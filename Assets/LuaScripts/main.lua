--HelpCs = go:AddComponent(typeof(CS.DoTest))
--HelpCs = CS.UnityEngine.GameObject("AssetUpdateHelper"):AddComponent(typeof(CS.AssetUpdateHelper))
-- 资源更新
--local asset_helper = CS.UnityEngine.GameObject.FindObjectOfType(typeof(CS.AssetUpdateHelper))
--asset_helper:UpdateAssets(GameInit)
--asset_helper:Test(main.xxtest)
--


require "Net.Common.NetDefines"


--加载首个界面
require "ui.LoginCtrl"
LoginCtrl.Init()