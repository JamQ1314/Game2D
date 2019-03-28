LoginCtrl = {}

local viewName = "LoginView"

function LoginCtrl.Init()
	--初始化
	require "ui.LoginView"
	CS.GApp.UIMgr:Create("prefabs.ui.LoginView",LoginCtrl.OnCreate);
end	


function LoginCtrl.OnCreate()
	--创建UI后回调
	CS.GApp.UIMgr:GetGameObject("wxloginbtn"):GetComponent("UnityEngine.UI.Button").onClick:AddListener(LoginCtrl.wxlogin)

	if CS.GApp.Ins:GetMode() == 0 then
		LoginView:AccLoginPanel()
		CS.GApp.UIMgr:GetGameObject("accloginbtn"):GetComponent("UnityEngine.UI.Button").onClick:AddListener(LoginCtrl.accLogin)
		CS.GApp.UIMgr:GetGameObject("accregisterbtn"):GetComponent("UnityEngine.UI.Button").onClick:AddListener(LoginCtrl.accRegister)
	end
	--连接服务器
	LoginCtrl.netConn()
end

function LoginCtrl.wxlogin()
end

function LoginCtrl.accRegister()
	print("this is accRegister");
	accpb = require "Net.Protobuf.acc_pb"
	acc = accpb.Account()
	acc.acc = "11111"
	acc.pwd = "22222"
	acc_bytes = acc:SerializeToString()
	
	CS.GApp.NetMgr:Send(0,Main_ID.Lobby,Lobby_ID.AccRegister,acc_bytes)
end

function LoginCtrl.accLogin()
	print("this	 is accLogin");
	accpb = require "Net.Protobuf.acc_pb"
	acc = accpb.Account()
	acc.acc = "acc"
	acc.pwd = "pwd"
	acc_bytes = acc:SerializeToString()
	CS.GApp.NetMgr:Send(0,Main_ID.Lobby,Lobby_ID.AccLogin,acc_bytes)
end

function LoginCtrl.netConn()
	CS.GApp.NetMgr:Connect(0,"127.0.0.1",5555)
end