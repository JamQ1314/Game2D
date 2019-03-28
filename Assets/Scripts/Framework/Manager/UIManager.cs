using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class UIManager : ManagerBase
{
    private Dictionary<string, GameObject> dictOpenedUIs;

    public override void Init()
    {
        base.Init();
        dictOpenedUIs = new Dictionary<string, GameObject>();
    }


    public void Create(string uiName,Action onCreate = null)
    {
        if (dictGameObjects.ContainsKey(uiName))
            return;
        GameObject ui = GApp.AssetLoaderMgr.LoadAsset(uiName);
        if (onCreate != null)
            onCreate();

        var shortName = uiName.Substring(uiName.LastIndexOf('.')+1);
        dictOpenedUIs.Add(shortName, ui);
    }
    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param uiName="name"></param>
    public void Open(string  uiName)
    {
        Open(uiName, null);
    }

    /// <summary>
    /// 打开UI，附带Hashtable参数
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="hashtable"></param>
    public void Open(string uiName, LuaTable luaTable)
    {
        print("uimanager open");

        GameObject ui = GetGameObject(uiName);
        if (ui == null)
        {
            ui = GApp.AssetLoaderMgr.LoadAsset(uiName);
        }
        //ui.GetComponent<LuaViewBehaviour>().Open(luaTable);
        dictOpenedUIs.Add(uiName, ui);
    }

    public void Test()
    {
        print("uimanager test");
    }

    /// <summary>
    /// 关闭单个UI
    /// </summary>
    /// <param name="name"></param>
    public void Close(string name)
    {
        GameObject ui = dictOpenedUIs[name];
        if (ui == null) return;
        ui.GetComponent<LuaViewBehaviour>().Close();
        dictOpenedUIs.Remove(name);

    }
    /// <summary>
    /// 关闭所有UI
    /// </summary>
    public void CloseAll()
    {
        foreach (var v in dictOpenedUIs.Values)
        {
            v.GetComponent<LuaViewBehaviour>().Close();
        }
        dictOpenedUIs = new Dictionary<string, GameObject>();
    }
}