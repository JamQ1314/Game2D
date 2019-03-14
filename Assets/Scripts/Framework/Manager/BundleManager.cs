using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BundleManager : ManagerBase
{
    /// <summary>
    /// 总依赖文件
    /// </summary>
    private AssetBundleManifest manifest;
    /// <summary>
    /// 记录资源名称-路径对应关系
    /// </summary>
    private Dictionary<string, string> dictAssets;
    /// <summary>
    /// 加载到缓存资源
    /// </summary>
    private List<AssetBundle> lCacheDenpendences;
    public override void Init()
    {
        base.Init();
        manifest = null;
        dictAssets = new Dictionary<string, string>();
        lCacheDenpendences = new List<AssetBundle>();

#if UNITY_EDITOR
        LoadLocalAseet();
#else
        LoadLocalManifest();
#endif
    }
    /// <summary>
    /// 加载总依赖文件
    /// </summary>
    private void LoadLocalManifest()
    {
        string manifestPath = ResPath.LocalPath + ResPath.ManifestName;
        AssetBundle manifestAssetBundle = AssetBundle.LoadFromFile(manifestPath);
        manifest = manifestAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] allAssetBundles = manifest.GetAllAssetBundles();
        for (int i = 0; i < allAssetBundles.Length; i++)
        {
            string tempAssetName = allAssetBundles[i];
            if (tempAssetName.Contains("/"))
            {
                var splits = tempAssetName.Split('/');
                tempAssetName = splits[splits.Length - 1];
            }
            dictAssets.Add(tempAssetName, allAssetBundles[i]);
        }
        //UnityTools.LogDictionary(dictAssets);
    }

    private void LoadLocalAseet()
    {
        string rootPtah = Application.dataPath + "/ABGame";
        print("------------------------ " +rootPtah);
        LoadLocalAseetBase(rootPtah);
        UnityTools.LogDictionary(dictAssets);
    }

    private void LoadLocalAseetBase(string path)
    {
        DirectoryInfo info = new DirectoryInfo(path);

        foreach (var file in info.GetFiles())
        {
            if (file.Extension != ".meta")
            {
                string rootPtah = Application.dataPath + "/ABGame";
                var fullName = file.FullName.Replace("\\","/");
                var relativeName = fullName.Replace(rootPtah,"");
                dictAssets.Add(file.Name, relativeName);
            }
        }

        foreach (var dir in info.GetDirectories())
        {
            LoadLocalAseetBase(dir.FullName);
        }
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Load<T>(string name) where T: UnityEngine.Object
    {
        if (!dictAssets.ContainsKey(name.ToLower()))
        {
            Debug.LogWarning("asset is not exist : " + name);
            return default(T);
        }
        string abName = dictAssets[name.ToLower()];
        //加载依赖
        string[] dps = manifest.GetAllDependencies(abName);
        if (dps.Length != 0)
        {
            foreach (string dp in dps)
            {
                Debug.Log(string.Format("正在加载资源{0}依赖：{1}", name, dp));
                LoadAssetBundle(dp);
            }
        }
        AssetBundle ab = LoadAssetBundle(abName);
        return ab.LoadAsset<T>(name.ToLower());
    }
    /// <summary>
    /// 加载并且实例化
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadAndInstantiate(string name)
    {
#if UNITY_EDITOR
        string abName = dictAssets[name.ToLower()];
        return (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath<Object>(""));
#else
        Object o = Load<Object>(name);
        GameObject go = (GameObject)Instantiate(o);
        Invoke("Release", 3.0f);
        return go;
#endif
    }

    /// <summary>
    /// 加载AssetBundle
    /// </summary>
    /// <param name="abName"></param>
    /// <returns></returns>
    private AssetBundle LoadAssetBundle(string abName)
    {
        AssetBundle ab = AssetBundle.LoadFromFile(ResPath.LocalPath + "/" + abName);
        lCacheDenpendences.Add(ab);
        return ab;
    }
    /// <summary>
    /// 释放缓存中的资源
    /// </summary>
    public void Release()
    {
        lock (lCacheDenpendences)
        {
            for (int i = 0; i < lCacheDenpendences.Count; i++)
            {
                lCacheDenpendences[i].Unload(false);
            }
            lCacheDenpendences.Clear();
        }
    }
}
