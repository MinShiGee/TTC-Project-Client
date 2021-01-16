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

    public void UpdateRoomList(List<RoomInfoDto> _roomList)
    {
        Debug.Log("CreateRoom");

        foreach(RoomInfoDto item in _roomList)
        {

        }

    }


}
