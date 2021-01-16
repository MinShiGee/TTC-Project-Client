using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Dictionary<int, RoomInfoDto> roomList = new Dictionary<int, RoomInfoDto>();

    [SerializeField]
    private Transform roomListTransform = default;
    [SerializeField]
    private GameObject roomInfoPrefeb = default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void UpdateRoomList(int _maxRooms, List<RoomInfoDto> _roomList)
    {
        if(roomList.Count != _maxRooms)
        {
            InitRoomList(_maxRooms);
        }

        int _roomId = 0;

        for(int i = 1; i <= _maxRooms; i++)
        {
            if (_roomId < _roomList.Count && i == _roomList[_roomId].id)
            {
                Debug.Log($"RoomId: {i} is Updated");
                _roomId++;
            }
            else if (_roomId < _roomList.Count && _roomList[_roomId].curPlayers != 0)
            {
                Debug.Log($"RoomId: {i} is removed.");
                /* Remove Room*/
            }

        }

    }

    private void InitRoomList(int _size)
    {
        roomList = new Dictionary<int, RoomInfoDto>();

        for(int i = 1; i <= _size; i++)
        {
            roomList.Add(i, new RoomInfoDto(i, "NullPointExcepton", "Server", 0, 6));
        }
    }


}
