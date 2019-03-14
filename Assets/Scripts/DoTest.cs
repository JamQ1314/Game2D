using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

public class DoTest : MonoBehaviour
{
    private LuaEnv luaenv = new LuaEnv();

	// Use this for initialization
    void Start()
    {
        var go = Instantiate(
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ABGame/prefabs/ui/login/LoginView.prefab"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}



}
