LoginCtrl = {}

local view
local viewName = "LoginView"

function LoginCtrl.Init()
	--��ʼ��
	view = require "ui.LoginView"
	CS.GApp.UIMgr:Open()
end	