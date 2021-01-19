using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomDto : MonoBehaviour
{
    public string roomName = "Null";
    public bool isPrivate = false;
    public string password = string.Empty;

    public CreateRoomDto(string _roomName, bool _isPrivate, string _password)
    {
        roomName = _roomName;
        isPrivate = _isPrivate;
        password = _password;
    }
}
