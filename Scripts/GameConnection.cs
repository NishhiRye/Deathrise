using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System.Linq;

public class GameConnection : MonoBehaviourPunCallbacks
{

    [PunRPC]
    public static GameConnection Instance;


    [SerializeField]
    InputField _roomName;

    [SerializeField]
    InputField _playerName;




    [SerializeField]
    Text _roomNameShowing;

    [SerializeField]
    Text _messages;

    [HideInInspector]
    public bool PlayerConnected;



    [SerializeField] Transform _playerListContent;
    [SerializeField] GameObject _playerListItemPrefab;

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;


    //-------------------------------------------------------------------------------------------

    //Game Starting
    //------------------------------------------------------------------------------------
    public static bool GameIsStarting = false;
    /*public Text StartingCountDown;
    public GameObject StartingFadeImage;
    public GameObject StartingReturn;
    public GameObject StartingStart;*/
    //------------------------------------------------------------------------------------

    void Awake()
    {
        Instance = this;
        GameIsStarting = false;

    }


    public void Update()
    {
        PhotonNetwork.NickName = _playerName.text;

        /*if (!GameIsStarting)
        {
            return;
        }
            StartingStart.SetActive(false);
            StartingReturn.SetActive(false);
            Image FadeImage = StartingFadeImage.GetComponent<Image>();
            Color fadeAlpha = FadeImage.color;
            fadeAlpha.a += Time.deltaTime;

            float fadeCountDown = 5f;
            Debug.Log(fadeCountDown);
            fadeCountDown -= Time.deltaTime;
            StartingCountDown.text = "GAME IS STARTING!\n\n" + fadeCountDown.ToString("F0");*/
    }

    //-------------------------------------------------------------------------------------------

    public void ConnectingToServer()
    {
        _messages.text = "Conectando-se ao servidor!";
        PhotonNetwork.ConnectUsingSettings();
        OnConnected();
    }

    //-------------------------------------------------------------------------------------------

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        _messages.text = "Conectado ao Master!";
        Debug.Log("master");

        PhotonNetwork.AutomaticallySyncScene = true;
    }
    //-------------------------------------------------------------------------------------------


    public override void OnConnected()
    {
        PlayerConnected = true;
        _messages.text = "Conectado com sucesso!";
        base.OnConnected();
    }

    //-------------------------------------------------------------------------------------------
    public override void OnJoinedLobby()
    {
        Debug.Log("Joinei no lobby");
        //PhotonNetwork.NickName = _playerName.text;
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomName.text))
        {
            return;
        }
        /*RoomOptions _roomOptions = new RoomOptions();
        _roomOptions.MaxPlayers = 2;*/
        PhotonNetwork.CreateRoom(_roomName.text/*, _roomOptions*/);
        Debug.Log("Created a Room");

    }

    //-------------------------------------------------------------------------------------------

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

    }

    //-------------------------------------------------------------------------------------------

    public override void OnJoinedRoom()
    {

        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("joined aqui " + _roomName.text + "as " + _playerName.text);
        _roomNameShowing.text = "SALA:\n" + _roomName.text;

        foreach (Transform child in _playerListContent)
        {
            Destroy(child.gameObject);
        }

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

    }

    //-------------------------------------------------------------------------------------------

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //-------------------------------------------------------------------------------------------        

    public override void OnLeftRoom()
    {
        _messages.text = "Voce saiu da sala";

    }

    //-------------------------------------------------------------------------------------------

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }

    //-------------------------------------------------------------------------------------------

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    //-------------------------------------------------------------------------------------------

    public bool RoomOwner()
    {
        return PhotonNetwork.IsMasterClient;
    }

    //-------------------------------------------------------------------------------------------

    public void StartGame()

    {
        /*StartingFadeImage.SetActive(true);*/
        GameIsStarting = true;        
        StartCoroutine(GameIsAboutToStart());
        

    }

    IEnumerator GameIsAboutToStart()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.LoadLevel("Main");
        
    }
    //----------------------------------------------------------------------------------------

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    //-------------------------------------------------------------------------------------------

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    //-------------------------------------------------------------------------------------------

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        MainMenu.MenuInstance.GameStartButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    //-------------------------------------------------------------------------------------------
}
