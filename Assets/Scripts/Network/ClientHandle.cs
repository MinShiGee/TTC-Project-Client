﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        LobbyUIManager.instance.ShowGameLobby();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void RoomList(Packet _packet)
    {
        int _maxRooms = _packet.ReadInt();
        int _length = _packet.ReadInt();
        List<RoomInfoDto> _tmoRoomList = new List<RoomInfoDto>();

        for(int i = 1; i <= _length; i++)
        {
            int _id = _packet.ReadInt();
            string _roomName = _packet.ReadString();
            string _ownerName = _packet.ReadString();
            int _curPlayers = _packet.ReadInt();
            int _maxPlayers = _packet.ReadInt();
            bool _isPrivate = _packet.ReadBool();

            _tmoRoomList.Add(new RoomInfoDto(_id, _roomName, _ownerName, _curPlayers, _maxPlayers, _isPrivate));
        }

        LobbyUIManager.instance.UpdateRoomList(_maxRooms , _tmoRoomList);
    }

    public static void LobbyChatMessage(Packet _packet)
    {
        string _msg = _packet.ReadString();
        LobbyUIManager.instance.ReceiveLobbyMessage(_msg);
        return;
    }

    public static void LobbyServerMessage(Packet _packet)
    {
        string _msg = _packet.ReadString();
        LobbyUIManager.instance.LobbyServerMessage(_msg);
        return;
    }

    public static void RoomChatMessage(Packet _packet)
    {
        string _msg = _packet.ReadString();
        LobbyUIManager.instance.ReceiveRoomMessage(_msg);
        return;
    }

    public static void RoomCreateStatus(Packet _packet)
    {
        bool _status = _packet.ReadBool();
        Debug.Log($"RoomCreateStatus = {_status}.");

        if (_status)
        {
            LobbyUIManager.instance.ShowRoomLobby();
        }

        return;
    }
}
