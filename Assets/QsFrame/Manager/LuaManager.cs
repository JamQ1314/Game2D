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

        if (true)
        {
            luaEnv.DoString(string.Format("require '{0}'", luaName));
            return;
        }
        else
        {
            TextAsset luaScript = GApp.AssetMgr.Load<TextAsset>(luaName + ".lua");
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
