using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager instance;

    private Dictionary<int, GameObject> roomList = new Dictionary<int, GameObject>();

    [SerializeField]
    private GameObject mainLobbyUI = default;
    [SerializeField]
    private GameObject gameLobbyUI = default;
    [SerializeField]
    private GameObject roomLobbyUI = default;

    [SerializeField]
    private GameObject ChatPrefeb = default;

    [Header("GameLobby")]
    [SerializeField]
    private Transform roomListTransform = default;
    [SerializeField]
    private GameObject roomInfoPrefeb = default;

    [SerializeField]
    private Transform lobbyChatListTransform = default;

    [SerializeField]
    private InputField lobbyChatInputField = default;

    [SerializeField]
    private Text lobbyServerText = default;

    [SerializeField]
    private GameObject createRoomPanel = default;

    [SerializeField]
    private PrivateRoomJoin privateRoomJoin = default;

    [Header("RoomLobby")]
    [SerializeField]
    private InputField roomChatInputField = default;
    [SerializeField]
    private Transform roomPlayerPanel = default;
    [SerializeField]
    private Transform roomChatListTransform = default;

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

    #region Connect Server

    public void ConnectToServer()
    {
        Client.instance.ConnectToServer();
    }

    public void ShowMainLobby()
    {
        mainLobbyUI.SetActive(true);
        gameLobbyUI.SetActive(false);
        roomLobbyUI.SetActive(false);
    }
    public void ShowGameLobby()
    {
        mainLobbyUI.SetActive(false);
        gameLobbyUI.SetActive(true);
        roomLobbyUI.SetActive(false);
    }
    public void ShowRoomLobby()
    {
        mainLobbyUI.SetActive(false);
        gameLobbyUI.SetActive(false);
        roomLobbyUI.SetActive(true);
    }
    public void ExitToGameLobby()
    {
        ClientSend.JoinRoom(-1, false, string.Empty);
        ShowGameLobby();
    }

    public void ExitToMainLobby()
    {
        Client.instance.Disconnect();
        ShowMainLobby();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    #region RoomList
    public void UpdateRoomList(int _maxRooms, List<RoomInfoDto> _roomList)
    {
        if(roomList.Count != _maxRooms)
        {
            InitRoomList(_maxRooms);
        }

        int _roomId = 0;

        for(int i = 1; i <= _maxRooms; i++)
        {

            if (_roomId >= _roomList.Count || (_roomId < _roomList.Count && i != _roomList[_roomId].id))
            {
                DeleteRoomInfo(i);
                continue;
            }
            else if (i == _roomList[_roomId].id)
            {
                UpdateRoomInfo(_roomList[_roomId], i);
                _roomId++;
            }

        }

    }

    private void InitRoomList(int _size)
    {
        roomList = new Dictionary<int, GameObject>();

        for(int i = 1; i <= _size; i++)
        {
            roomList.Add(i, null);
        }
    }

    private RoomInfo WriteRoomInfo(RoomInfo _info, RoomInfoDto _dto)
    {

        _info.roomId = _dto.id;
        _info.roomIdText.text = _dto.id.ToString();
        _info.roomNameText.text = _dto.roomName;
        _info.roomPlayersText.text = _dto.curPlayers + "/" + _dto.maxPlayers;
        _info.roomOwnerText.text = _dto.ownerName;
        _info.isPrivate = _dto.isPrivate;
        
        if (_info.isPrivate)
            _info.isPrivateText.text = "Private";
        else
            _info.isPrivateText.text = "";

        return _info;
    }

    private void CreateRoomInfo(RoomInfoDto _dto)
    {
        GameObject _infoPrefeb = Instantiate(roomInfoPrefeb, roomListTransform);
        RoomInfo _info = _infoPrefeb.GetComponent<RoomInfo>();
        WriteRoomInfo(_info, _dto);
        roomList[_info.roomId] = _infoPrefeb;

        return;
    }

    private void UpdateRoomInfo(RoomInfoDto _dto, int _id)
    {
        if (roomList[_id] == null)
        {
            CreateRoomInfo(_dto);
            return;
        }

        WriteRoomInfo(roomList[_id].GetComponent<RoomInfo>(), _dto);
        return;
    }

    private void DeleteRoomInfo(int _id)
    {
        if (roomList[_id] == null)
            return;

        Destroy(roomList[_id].gameObject);
        roomList[_id] = null;

        return;
    }

    #endregion

    #region LobbyChatManager

    public void SendLobbyMessage()
    {
        string _msg = lobbyChatInputField.text;
        lobbyChatInputField.text = "";

        ClientSend.LobbyChatMessage(_msg);
        return;
    }

    public void ReceiveLobbyMessage(string _msg)
    {
        Instantiate(ChatPrefeb, lobbyChatListTransform).GetComponent<ChatMessage>().message.text = _msg;
    }

    #endregion

    #region RoomChatManager

    public void SendRoomMessage()
    {
        string _msg = roomChatInputField.text;
        roomChatInputField.text = string.Empty;
        ClientSend.RoomChatMessage(_msg);
        return;
    }

    public void ReceiveRoomMessage(string _msg)
    {
        Instantiate(ChatPrefeb, roomChatListTransform).GetComponent<ChatMessage>().message.text = _msg;
    }

    #endregion

    #region ServerMessageManager
    public void LobbyServerMessage(string _msg)
    {
        lobbyServerText.text = _msg;
        return;
    }

    public void ShowCreateRoomPanel()
    {
        createRoomPanel.SetActive(true);
    }
    #endregion

    #region JoinRoom

    public void JoinFastRoom()
    {
        ClientSend.JoinRoom(0, false, "Hello");
        return;
    }

    public void ShowPrivateRoomJoinPanel(bool _data, int _roomId)
    {
        privateRoomJoin.gameObject.SetActive(_data);
        privateRoomJoin.roomid = _roomId;
    }

    #endregion

}
