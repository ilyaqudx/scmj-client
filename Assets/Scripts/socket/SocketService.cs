using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using DefaultNamespace.message;
using LitJson;
using UnityEngine;

namespace DefaultNamespace.socket
{
    public class SocketService
    {
        private static SocketService INSTANCE = new SocketService();

        public static SocketService getInstance()
        {
            return INSTANCE;
        }

        private TcpClient tcpClient;
        private byte[] readBuffer = new byte[1024];
        private byte[] segment = null;

        public SocketService()
        {
            Debug.Log("socket service is instanced!");
        }

        public void connect()
        {
            try
            {
                tcpClient = new TcpClient(AddressFamily.InterNetwork);
                tcpClient.Connect(Api.HOST, Api.PORT);
                Debug.Log(string.Format("connect socket service status: {0} {1}:{2}", tcpClient.Connected, Api.HOST,
                    Api.PORT));
                tcpClient.GetStream().BeginRead(readBuffer, 0, readBuffer.Length, onReadCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void reconnect()
        {
            try
            {
                connect();
                Debug.Log("socket server reconnected!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void onReadCallback(IAsyncResult ar)
        {
            try
            {
                var readBytes = tcpClient.GetStream().EndRead(ar);
                var segmentLength = segment == null ? 0 : segment.Length;
                var dataLength = segmentLength + readBytes;
                var dataBuffer = new byte[dataLength];
                if (segment != null)
                {
                    Array.Copy(segment, 0, dataBuffer, 0, segment.Length);
                }

                Array.Copy(readBuffer, 0, dataBuffer, segmentLength, readBytes);


                var reader = new BinaryReader(new MemoryStream(dataBuffer));
                var currentReadBytes = 0;
                var totalReadBytes = 0;
                while ((currentReadBytes = decode(reader, totalReadBytes)) > 0)
                {
                    totalReadBytes += currentReadBytes;
                }

                if (totalReadBytes < reader.BaseStream.Length)
                {
                    segment = new byte[reader.BaseStream.Length - totalReadBytes];
                    Array.Copy(dataBuffer, totalReadBytes, segment, 0, segment.Length);
                }
                else
                {
                    segment = null;
                }

                // close reader
                closeReader(reader);

                Debug.Log(string.Format("current total read bytes: {0}", totalReadBytes));
                tcpClient.GetStream().BeginRead(readBuffer, 0, readBuffer.Length, onReadCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void closeReader(BinaryReader reader)
        {
            try
            {
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /**
         * response message header is 13 bytes
         *
         * header = flag(1) + packetLen(4) + cmd(4) + status(4)
         * packetLen = header(13) + body
         * body = packetLen - 13
         */
        private int decode(BinaryReader reader, int position)
        {
            var startPosition = reader.BaseStream.Position;

            if (reader.BaseStream.Length - startPosition < 8)
            {
                return 0;
            }


            var cmd = All.toInt(reader.ReadBytes(4));
            var packetLen = All.toInt(reader.ReadBytes(4));

            if (reader.BaseStream.Length - startPosition < packetLen + 8)
            {
                reader.BaseStream.Position = 0;
                return 0;
            }

            var messageResponse =
                JsonUtility.FromJson<MessageResponse>(Encoding.UTF8.GetString(reader.ReadBytes(packetLen)));
            Debug.Log(string.Format("decode server response message: {0}", messageResponse));

            CommandDispatcher.dispatch(messageResponse);

            return (int)(reader.BaseStream.Position - startPosition);
        }

        public void send(MessagePacket messagePacket)
        {
            if (tcpClient.Connected)
            {
                if (null != messagePacket)
                {
                    var packet = getBytes(messagePacket);
                    tcpClient.GetStream().BeginWrite(packet, 0, packet.Length, onWriteCallback, null);
                }
            }
            else
            {
                Debug.Log("socket server is disconnected,can not send message.reconnect socket server right now.");
                reconnect();
            }
        }

        private byte[] getBytes(MessagePacket messagePacket)
        {
            byte[] bytes; //自定义字节数组，用以装载消息协议
            using (var memoryStream = new MemoryStream()) //创建内存流
            {
                //以二进制写入器往这个流里写内容
                var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
                // 网络包长度

                var json = JsonMapper.ToJson(messagePacket);
                Debug.Log(string.Format("wait send message: {0} - {1}", json.Length, json));
                var packetBytes = Encoding.UTF8.GetBytes(json);
                var packetLength = packetBytes.Length;
                Debug.Log(string.Format("packetLength: {0}", packetLength));

                //写入协议，占4个字节
                binaryWriter.Write(All.getBytes(messagePacket.cmd));
                //写入包长度占4个字节
                binaryWriter.Write(All.getBytes(packetLength));
                //写入实际消息内容
                binaryWriter.Write(packetBytes);

                bytes = memoryStream.ToArray(); //将流内容写入自定义字节数组
                Debug.Log(string.Format("wait send message bytes length: {0}", bytes.Length));
                binaryWriter.Close(); //关闭写入器释放资源
            }

            return bytes; //返回填充好消息协议对象的自定义字节数组
        }

        public void onWriteCallback(IAsyncResult ar)
        {
            try
            {
                tcpClient.GetStream().EndWrite(ar);
                Debug.Log(string.Format("success send message to server"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}