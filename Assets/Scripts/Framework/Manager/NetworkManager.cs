using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.LuaCallCSharp]
public class NetworkManager : ManagerBase
{
    private Dictionary<int, NSocket> ConnSockets;
    public override void Init()
    {
        base.Init();
        ConnSockets = new Dictionary<int, NSocket>();
    }
    
    public void RigsterNetEvent(int socketID,int main_id,Action<int,byte[]> call)
    {
        NSocket nSocket = null;
        if (ConnSockets.ContainsKey(socketID))
            nSocket = ConnSockets[socketID];
        else
            nSocket = new NSocket(socketID);
        nSocket.AddCall(main_id, call);
    }
    
    public void Connect(int socketID, string host, int port)
    {
        NSocket nSocket = null;
        if (ConnSockets.ContainsKey(socketID))
            nSocket = ConnSockets[socketID];
        else
            nSocket = new NSocket(socketID);
        nSocket.ConnAsync(host, port);
    }

    public void Send(ushort main_id, ushort sub_id, byte[] data, Action<Hashtable> callback = null, Hashtable hashtable = null)
    {
        if (!ConnSockets.ContainsKey(main_id))
            return;
        NSocket nSocket =  ConnSockets[main_id];
        nSocket.SendAsync(main_id, sub_id, data, callback, hashtable);
    }

    public void Close(int socketID)
    {
        if (ConnSockets.ContainsKey(socketID))
        {
            NSocket nSocket = ConnSockets[socketID];
            nSocket.Close();
        }
    }


    private void OnDestroy()
    {
        foreach (var conn in ConnSockets.Values)
        {
            conn.Close();
        }
    }
}
