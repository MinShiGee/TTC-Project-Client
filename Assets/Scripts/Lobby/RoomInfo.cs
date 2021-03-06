﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomInfo : MonoBehaviour, IPointerDownHandler
{
    public int roomId = 0;

    public Text roomIdText;
    public Text roomNameText;
    public Text roomPlayersText;
    public Text roomOwnerText;

    public Text isPrivateText;
    public bool isPrivate;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.clickCount >= 2)
        {
            eventData.clickCount = 0;
            Debug.Log($"Joining RoomId {roomId}!");

            if (isPrivate)
            {
                LobbyUIManager.instance.ShowPrivateRoomJoinPanel(true, roomId);
                return;
            }

            ClientSend.JoinRoom(roomId, isPrivate, "Hello!");
            return;
        }
    }

}
