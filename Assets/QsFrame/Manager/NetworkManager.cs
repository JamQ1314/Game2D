using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkManager : MonoBehaviour
{

    private Dictionary<int, NSocket> ConnSockets = new Dictionary<int, NSocket>();
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

    public void Close(int socketID)
    {
        if (ConnSockets.ContainsKey(socketID))
        {
            NSocket nSocket = ConnSockets[socketID];
            nSocket.Close();
        }
    }
}
