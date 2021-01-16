using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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