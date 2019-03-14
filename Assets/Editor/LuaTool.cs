using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LuaTool : Editor
{

    [MenuItem("Tools/Lua Copy")]
    static void Copy()
    {
        string srcPath = Application.dataPath + "/LuaScripts";
        string dstPath = Application.dataPath + "/ABGame/lua";

        if (!Directory.Exists(dstPath))
            Directory.CreateDirectory(dstPath);
        else
        {
            //清空文件夹
            ClearDir(dstPath);
        }
        
        //复制

    }

    static void ClearDir(string dstPath)
    {
        if (!Directory.Exists(dstPath))
            return;
        DirectoryInfo dir = new DirectoryInfo(dstPath);
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos(); //返回目录中所有文件和子目录
        foreach (FileSystemInfo i in fileinfo)
        {
            if (i is DirectoryInfo) //判断是否文件夹
            {
                DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                subdir.Delete(true); //删除子目录和文件
            }
            else
            {
                File.Delete(i.FullName); //删除指定文件
            }
        }
    }

    static void CopyLuaScirpts(string srcPtah,string dstPath)
    {
        DirectoryInfo info = new DirectoryInfo(srcPtah);
        
    }
}
