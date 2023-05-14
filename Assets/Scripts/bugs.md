# 2022-8-7 23:03分

## [bug]

1、 已解决
[2022-8-7 22:55:05:222] MissingReferenceException: The object of type 'Button' has been destroyed but you are still
trying to
access it.
进入房间后退出,然后再进入点击准备出现上述异常信息
readyBtn.gameObject.SetActive(false);

[2022-8-9 23:18]
解决方案：将注册的回调事件在OnDestory方法里面全部要移除掉,否则再次进入原来的场景就会有问题，回调方法还是会调用但是回调方法里面涉及的UI等就会报NULL
参考网上链接：https://blog.csdn.net/qq_37704442/article/details/122885276

2、 已解决
全局GameObject: 设置LoadNoDestory()

3、 已解决
全局Toast：进入每一个SCENE在AWARE方法里面切换transform

4、已解决
unity5.6 使用的.NET3.0 json框架解析byte[]类型字段时需要先转换为base64 string,再转为byte[],高版本则直接就支持

5、部分解决
1、toggle group解决控制只能单选
2、重置toggle radio状态未生效(待解决)

6、已解决
开发时界面失去焦点暂停运行: 在player中有一个配置 run background勾选上即可

# 2022-8-9 23:21

1、解决[bug][1]

# 2022-8-16 23:19

7.1、使用CardFactory创建了HandCard,也通过代码动态给card添加点击事件,成功收到点击回调
7.2、当牌被点击时添加transform.DOBlendableLocalMoveBy动画,该方法相当于是BY.该动画花了30分钟才弄好因为之前使用的是另外一个方法DOMoveY，坐标系感觉不对
7.3、现在点击回调里面的EVENT可以拿到GAMEOBJECT对象,但是如何拿到CARD对象呢？或者还是换一种逻辑,把回调方法写在ROOMSCRIT中,也在ROOMSCRIPT中保存CARD列表，来进行操作

# 2022-8-23 22:52
7.3已找到解决方案 

方案：之前的想法是创建一个普通的对象CARD，里面持有GameObject（显示的手牌对象)，然后在点击后通过gameObject再找到对应的Card,但一直卡在如何在点击后通过gameObject获取
到Card对象，一直没想通（因为gameObject不能存储自定义数据，最多存储一个string类型的tag值，不是很满足需求）。但最后通过看之前别人写的项目里面的做法是和我的想法是相反的。
是将Card对象作为gameObject对象的组件来使用,然后点击gameObject之后则通过gameObject.getComponent<Card>()方法就可以获取到Card了，也就拿到了相关人value信息。
在prefab里直接添加card脚本为component这样才能在代码中获取card组件然后使用.这个完全是原有思路相反的确实想不到

给prefab实例gameobject添加点击事件 -> 非常简单：直接使Card去实现IpointerClickHandler接口就完成了，会实现OnPointerClick方法即可.相当简单(另外还有一种方法就是获取
gameobject组件EventTrigger，然后在代码中获取这个组件，然后添加Entry。包含eventId和callback.Listener)
var eventTrigger = gameObject.GetComponent<EventTrigger>();
var entry = new EventTrigger.Entry();
entry.eventID = EventTriggerType.PointerClick;
entry.callback.AddListener(clickHandCard);
eventTrigger.triggers.Add(entry);

然后还有一个问题就是：当点击后如何触发后续的动作，因为很多显示是需要在RoomScript里面去处理的（特别是UI相关的），那CARD接收到点击事件后如何通知到ROOMSCRIPT呢？此时定义
一个EventHandler(GameObject gameObject)代理方法，然后在CARD里面定义EventHandler方法的变量，实现方法是在RoomScript里面，然后通过 += 来进行注册.
另外RoomScript方法现在写的是STATIC的，而回调回来后需要使用一些实例方法，所以在ROOMSCRIPT构造函数里面来一个INSTANCE接收this对象

重要：组件对象中的gameObject就是该组件所附属的GameObject对象，所以在手牌prefab上的组件card添加IPointerClickHandler接口注册事件时其实就是注册到了HandCard上面




