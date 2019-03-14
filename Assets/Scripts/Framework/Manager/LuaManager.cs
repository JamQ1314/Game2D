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
        luaEnv.AddLoader(MyCustomLoader);
    }

    private static byte[] MyCustomLoader(ref string fileName)
    {
        if (GApp.GMode == DevelopMode.Debug)
        {
            fileName = fileName.Replace(".", "/") + ".lua";
            var fullName = Application.dataPath + "/LuaScipts/" + fileName;
            return File.ReadAllBytes(fullName);
        }
        else
        {
            return null;
        }
        

    }

    public void DoString(string luaStr)
    {
        luaEnv.DoString(luaStr);
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
