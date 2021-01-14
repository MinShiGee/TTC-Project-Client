using System.Collections;
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

        ClientSend.CreateRoom();
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
        int _length = _packet.ReadInt();
        for(int i = 1; i <= _length; i++)
        {
            int _id = _packet.ReadInt();
            string _roomName = _packet.ReadString();
            string _ownerName = _packet.ReadString();
            int _curPlayers = _packet.ReadInt();
            int _maxPlayers = _packet.ReadInt();
        }
    }
}
