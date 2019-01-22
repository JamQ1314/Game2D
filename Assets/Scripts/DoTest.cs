using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class DoTest : MonoBehaviour
{
    private LuaEnv luaenv = new LuaEnv();

	// Use this for initialization
    void Start()
    {
        //        luaenv.AddLoader((ref string fileFullName) =>
        //        {
        //            return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(fileFullName));
        //        });

        //        luaenv.AddLoader((ref string fileFullName) => { return null; });
        //
        //        string path = Path.Combine(Environment.CurrentDirectory, "AssetBundles/").Replace("\\", "/");
        //        string fullPath = path + "Windows/lua/main.lua";
        //        if (File.Exists(fullPath))
        //        {
        //            AssetBundle ab = AssetBundle.LoadFromFile(fullPath);
        //            TextAsset luaScript = ab.LoadAsset<TextAsset>("main.lua");
        //            luaenv.DoString(luaScript.text);
        //            luaenv.Dispose();
        //        }
        //        else
        //        {
        //            Debug.Log("file is not exit");
        //        }

        luaenv.DoString("require 'main'");
        luaenv.Dispose();


        var mgr = new AssetManager();
        mgr.Init();
        mgr.LoadAndInstantiate("LoginView");
    }
	
	// Update is called once per frame
	void Update () {
		
	}



}
