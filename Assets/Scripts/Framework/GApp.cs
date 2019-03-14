using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public enum DevelopMode
{
    Debug,//开发模式
    Release //发布模式
}
[LuaCallCSharp]
public class GApp
{
    public static DevelopMode GMode = DevelopMode.Debug;
    /// <summary>
    /// AssetBundle管理器
    /// </summary>
    public static BundleManager BundleMgr = null;
    /// <summary>
    /// 网络管理器
    /// </summary>
    public static NetworkManager NetMgr = null;
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
        BundleMgr = UnitySingleton<BundleManager>.Ins;
        NetMgr = UnitySingleton<NetworkManager>.Ins;
        UIMgr = UnitySingleton<UIManager>.Ins;
        AudioMgr = UnitySingleton<AudioManager>.Ins;
        LuaMgr = UnitySingleton<LuaManager>.Ins;
    }


    public static void Reset()
    {
        BundleMgr.Init();
        NetMgr.Init();
        UIMgr.Init();
        AudioMgr.Init();
        LuaMgr.Init();
    }
#endregion
}