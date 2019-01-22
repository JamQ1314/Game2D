using System.IO;
using UnityEditor;
using UnityEngine;

public class LuaTool : Editor {
    
    [MenuItem("Tools/xlua copy")]
    static void Copy()
    {
        string srcPath = Application.dataPath + "/Scripts/Resources";
        string dstPath = Application.dataPath + "/ABGame/lua";
        if (!Directory.Exists(dstPath))
            Directory.CreateDirectory(dstPath);
        else
        {
            DirectoryInfo dstInfo = new DirectoryInfo(dstPath);
            for (int i = 0; i < dstInfo.GetFiles().Length; i++)
            {
                File.Delete(dstInfo.GetFiles()[i].FullName);
            }
        }

        DirectoryInfo srcInfo = new DirectoryInfo(srcPath);
        for (int i = 0; i < srcInfo.GetFiles().Length; i++)
        {
            var file = srcInfo.GetFiles()[i];
            File.Copy(file.FullName, dstPath + "/" + file.Name);
        }

        AssetDatabase.Refresh();
    }
}
