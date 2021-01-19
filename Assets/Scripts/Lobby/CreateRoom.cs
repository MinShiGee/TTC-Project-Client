using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    public InputField roomName = default;
    public Toggle isPrivate = default;
    public InputField password = default;


    public void isPrivateClicked()
    {
        if (isPrivate.isOn)
        {
            password.interactable = true;
        }
        else
        {
            password.interactable = false;
        }
    }

    public void SendCreateRoomDto()
    {
        CreateRoomDto _dto = new CreateRoomDto(roomName.text, isPrivate.isOn, password.text);
        if (roomName.text.Length < 1)
            return;
        ClientSend.CreateRoom(_dto);
        HideCreateRoomPanel();
    }

    public void HideCreateRoomPanel()
    {
        this.gameObject.SetActive(false);
    }
}
