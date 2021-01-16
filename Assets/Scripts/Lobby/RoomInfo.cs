using UnityEngine;
using UnityEngine.UI;

public class RoomInfoDto
{
    public int id;
    public string roomName;
    public string ownerName;

    public int curPlayers;
    public int maxPlayers;

    public RoomInfoDto(int _id, string _roomName, string _ownerName, int _curPlayers, int _maxPlayers)
    {
        id = _id;
        roomName = _roomName;
        ownerName = _ownerName;
        curPlayers = _curPlayers;
        maxPlayers = _maxPlayers;
    }
}
public class RoomInfo : MonoBehaviour
{
    public int roomId;

    public Text roomIdText;
    public Text roomNameText;
    public Text roomPlayersText;

}
