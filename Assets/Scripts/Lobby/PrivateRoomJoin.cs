using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivateRoomJoin : MonoBehaviour
{
    public int roomid = 0;

    [SerializeField]
    private InputField passwordInputField = default;

    public void JoinPrivateRoom()
    {
        string _password = passwordInputField.text;
        ClientSend.JoinRoom(roomid, true, _password);
        CancelJoin();
    }

    public void CancelJoin()
    {
        passwordInputField.text = "";
        gameObject.SetActive(false);
    }
}
