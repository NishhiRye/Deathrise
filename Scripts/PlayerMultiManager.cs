using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;

public class PlayerMultiManager : MonoBehaviourPunCallbacks
{
    GameObject _playerSpawn;
    PhotonView PV;

    [SerializeField]
    GameObject Player;

    [HideInInspector]
    public string PlayerNickname;
    
    // Start is called before the first frame update
    void Awake()
    {
        //_playerSpawn = GameObject.FindWithTag("PlayerMultiSpawn");
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {        
        if (PV.IsMine)
        {            
            CreatePlayer();
        }
    }
    // Update is called once per frame
    void CreatePlayer()
    {        
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), _playerSpawn.transform.position, _playerSpawn.transform.rotation);
        Player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player Final"),new Vector3(59,24,-101) , Quaternion.identity);
        Player.GetComponent<PlayerController>().PlayerName = PlayerNickname;
    }
}
