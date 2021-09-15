using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RagdollBehaviour : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public bool RagdollToActivate = false;

    [SerializeField]
    GameObject _charRag;
    
    void Start()
    {
        RagdollToActivate = false;
        _charRag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<PhotonView>().RPC("RagdollFX", RpcTarget.All);
    }

    [PunRPC]
    void RagdollFX()
    {
        if (RagdollToActivate) _charRag.SetActive(true);

    }
}
