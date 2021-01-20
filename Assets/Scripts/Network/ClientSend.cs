using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("HongGilDong");

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    public static void CreateRoom(CreateRoomDto _dto)
    {
        using (Packet _packet = new Packet((int)ClientPackets.roomCreate))
        {
            _packet.Write(_dto.roomName);
            _packet.Write(_dto.isPrivate);
            _packet.Write(_dto.password);
            SendTCPData(_packet);
        }

        return;
    }

    public static void LobbyChatMessage(string _msg)
    {
        using (Packet _packet = new Packet((int)ClientPackets.lobbyChatMessage))
        {
            _packet.Write(_msg);
            SendTCPData(_packet);
        }

        return;
    }

    public static void RoomChatMessage(string _msg)
    {
        using (Packet _packet = new Packet((int)ClientPackets.roomChatMessage))
        {
            _packet.Write(_msg);
            SendTCPData(_packet);
        }
    }
    #endregion
}
