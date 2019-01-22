﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


[LuaCallCSharp]
public class GApp
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public static AssetManager AssetMgr = null;
    /// <summary>
    /// UI界面管理器
    /// </summary>
    public static UIManager UIMgr = null;
    /// <summary>
    /// 声音管理器
    /// </summary>
    public static AudioManager AudioMgr = null;
    /// <summary>
    /// 热更新管理器
    /// </summary>
    public static LuaManager LuaMgr = null;

    #region mono 方法
    public static void Init()
    {
        AssetMgr = UnitySingleton<AssetManager>.Ins;
        UIMgr = UnitySingleton<UIManager>.Ins;
        AudioMgr = UnitySingleton<AudioManager>.Ins;
        LuaMgr = UnitySingleton<LuaManager>.Ins;
    }


    public static void Reset()
    {
        AssetMgr.Init();
        UIMgr.Init();
        AudioMgr.Init();
        LuaMgr.Init();     
    }
#endregion
}