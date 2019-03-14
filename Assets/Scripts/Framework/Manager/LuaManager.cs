using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaManager : MonoBehaviour
{
    public LuaEnv luaEnv;
    public void Init()
    {
        luaEnv = new LuaEnv();

    }

    public void DoString(string luaStr)
    {
        luaEnv.DoString(luaStr);
    }


    enum RunModel
    {
        Dev,
        Debug,
    }
    public void LoadLua(string luaName)
    {
        if (GApp.GMode == DevelopMode.Debug)
        {
            string relativePtah = Application.dataPath + "/ABGame/LuaScripts";
            luaEnv.DoString(string.Format("require '{0}'", luaName));
        }
        else
        {
            TextAsset luaScript = GApp.BundleMgr.Load<TextAsset>(luaName + ".lua");
            if (luaScript != null)
                luaEnv.DoString(luaScript.text);
        }

    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
