using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using UnityEngine;

public class SSDKManager : ManagerBase {

    private ShareSDK ssdk;
    private void Awake()
    {
       
    }
    public override void Init()
    {
        base.Init();
        ssdk = FindObjectOfType<ShareSDK>();
        if (ssdk == null)
        {
            //Debug.LogError("Please Add GameObject with script 'ShareSDK'");
            return;
        }
        DontDestroyOnLoad(ssdk.gameObject);

        ssdk.authHandler = OnAuthResultHandler;
        ssdk.showUserHandler = OnGetUserInfoResultHandler;
    }

    public void QQLogin()
    {
        print("SSDK QQLogin");
        ssdk.GetUserInfo(PlatformType.QQ);
    }

    void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            if (result != null && result.Count > 0)
            {
                print("authorize success !" + "Platform :" + type + "result:" + MiniJSON.jsonEncode(result));
            }
            else
            {
                print("authorize success !" + "Platform :" + type);
            }
            ssdk.GetUserInfo(PlatformType.QQ);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
            
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
            
        }
    }

    void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get user info result :");
            print(MiniJSON.jsonEncode(result));
            print("AuthInfo:" + MiniJSON.jsonEncode(ssdk.GetAuthInfo(PlatformType.QQ)));
            print("Get userInfo success !Platform :" + type);

            Hashtable info = ssdk.GetAuthInfo(PlatformType.QQ);
            guiStr = string.Format("nickname : {0}     userID: {1}     userIcon: {2}", (string)result["nickname"], (string)info["userID"], (string)info["userIcon"]);

        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
            
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    private string guiStr = "Ready!";
    private void OnGUI()
    {
        GUIStyle sty = new GUIStyle();
        sty.fontSize = 25;
        sty.normal.textColor = Color.red;
        GUILayout.Label(guiStr, sty);
    }

}
