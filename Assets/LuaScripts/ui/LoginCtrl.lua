LoginCtrl = {}

local view
local viewName = "LoginView"

function LoginCtrl.Init()
	--≥ı ºªØ
	view = require "ui.LoginView"
	CS.GApp.UIMgr:Open()
end	