﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine;

public class NSocket
{

    private int sid;

    private Socket socket;

    private Dictionary<int, Action<int, byte[]>> nCalls;

    private byte[] RecvBuffer;

    private int nRecvSize;//已接接收数据长度

    private int nHeadSize;



    public NSocket(int id)
    {
        sid = id;

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        nCalls = new Dictionary<int, Action<int, byte[]>>();

        RecvBuffer = new byte[NetDefine.TCP_BUFFER];

        nRecvSize = 0;

        nHeadSize = Marshal.SizeOf(typeof(TCP_Head));
        
    }

    public void AddCall(int id, Action<int, byte[]> call)
    {
        if (nCalls.ContainsKey(id))
        {
            var ac = nCalls[id];
            ac += call;
        }
        else
        {
            nCalls.Add(id, call);
        }
    }

    public void ConnAsync(string host, int port)
    {
        socket.BeginConnect(host, port, ConnCB, this);
    }

    private void ConnCB(IAsyncResult ar)
    {
        try
        {
            socket.EndConnect(ar);
            RecvAsync();
        }
        catch (SocketException se)
        {
            ReConn(se.ErrorCode);
        }
    }

    public void RecvAsync()
    {
        socket.BeginReceive(RecvBuffer, nRecvSize, NetDefine.TCP_BUFFER - nRecvSize, SocketFlags.None, RecvCB, this);
    }

    void RecvCB(IAsyncResult ar)
    {
        try
        {
            int nSize = socket.EndReceive(ar);
            if (nSize <= 0)
                return;
            nRecvSize += nSize;

            int nAllDataSiz = RecvBuffer[0] + (RecvBuffer[1] << 8); //16位表示 TCP_Head中Buffer_Size

            if (nAllDataSiz > nRecvSize)//数据未接受完
                return;

            if (nAllDataSiz == nRecvSize)
            {
                ProcessNetEvent(RecvBuffer);

                nRecvSize = 0;
            }
            else
            {
                Debug.Log("数据接受异常！！！");
            }

            RecvAsync();
        }
        catch (SocketException se)
        {
            ReConn(se.ErrorCode);
        }
    }

    private void ProcessNetEvent(byte[] bytes)
    {
        byte[] tempHeadBuff = new byte[nHeadSize];
        Array.Copy(bytes, 0, tempHeadBuff, 0, nHeadSize);
//        TCP_Head tcpHead = NetDefine.BytesToStru<TCP_Head>(tempHeadBuff);
    }


    public void SendAsync(ushort main_id,ushort sub_id,byte[] data = null,Action<Hashtable> callback = null,Hashtable hashtable = null)
    {
        TCP_Head tcpHead = new TCP_Head();
        tcpHead.Cmd.Main_ID = main_id;
        tcpHead.Cmd.Sub_ID = sub_id;
        int nHeadSize = Marshal.SizeOf(tcpHead);

        int nSendSize = nHeadSize;
        if (data != null)
            nSendSize += data.Length;

        byte[] SendBuffer = new byte[nSendSize];
        var Headbuffer = NetDefine.StruToBytes(tcpHead);
        Array.Copy(Headbuffer, SendBuffer, nHeadSize);
        if (data != null)
            Array.Copy(data, 0, SendBuffer, nHeadSize, data.Length);

        socket.BeginSend(SendBuffer,0, nSendSize,SocketFlags.None, (ar) =>
        {
            try
            {
                socket.EndSend(ar);
                if (callback != null)
                    callback(hashtable);
            }
            catch (SocketException se)
            {
                ReConn(se.ErrorCode);
            }
        },socket);
    }

    void SendCB(IAsyncResult ar)
    {

    }
    public void ReConn(int eCode)
    {
        
    }

    public void Close()
    {
        try
        {
            socket.Shutdown(SocketShutdown.Both);
        }
        catch
        {

        }
        finally
        {
            socket.Close();
        }
    }
}
