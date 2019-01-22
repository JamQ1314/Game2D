using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class UIManager : ManagerBase
{
    private Dictionary<string, GameObject> dictOpenedUIs;

    public override void Init()
    {
        base.Init();
        dictOpenedUIs = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="name"></param>
    public void Open(string  name)
    {
        Open(name, null);
    }

    /// <summary>
    /// 打开UI，附带Hashtable参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="hashtable"></param>
    public void Open(string name, LuaTable luaTable)
    {
        GameObject ui = GetGameObject(name);
        if (ui == null)
        {
            //先加载Lua再加载UI
            GApp.LuaMgr.LoadLua(name);
            ui = GApp.AssetMgr.LoadAndInstantiate(name);
        }

        ui.GetComponent<LuaViewBehaviour>().Open(luaTable);
        dictOpenedUIs.Add(name, ui);
    }

    public void UIMgrHello()
    {
        Debug.Log("UIMgrHello");
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