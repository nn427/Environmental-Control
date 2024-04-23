# 投影機控制：PJLink protocol

-文件. https://pjlink.jbmia.or.jp/english/data_cl2/PJLink_5-1.pdf

-範例. https://github.com/uow-dmurrell/ProjectorControl/tree/master/pjlink-sharp-read-only/Example

設定：

	a.省電模式關閉

	b.待機模式-連結待機

	c.LAN設置-DHCP關閉，需手動指派


# Unity3D

投影機控制實作: ProjectorUtility.cs

需指定投影機IP位置(區網)

		a.開機:void AwakeProj() 
		b.關機:void CloseProj()
  
		(port預設4352)

電腦WOL實作:WOLUtility.cs

	需指定Router IP, port, 目標電腦MAC位置
 
	方法一:
		a.初始化UDP連線: void Init()
		b.送出Magic Packet:void wakeOnLanWithConnectedClient()
	方法二:
	  	a.使用一次性連線:void SendAwake()
