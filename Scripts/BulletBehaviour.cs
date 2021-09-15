using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletBehaviour : MonoBehaviourPunCallbacks
{
    Vector3 _previousPosition;

    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject _bulletPoolGroup;

    [HideInInspector]
    public float BulletDamage;

    void Start()
    {
        _previousPosition = transform.position;
    }
    void OnEnable()
    {
        StartCoroutine(BulletDisable());
    }
    void OnDisable()
    {

    }



    void Update()
    {
        _previousPosition = transform.position;
        RaycastHit[] hits = Physics.RaycastAll(new Ray(_previousPosition, (transform.position - _previousPosition).normalized), (transform.position - _previousPosition).magnitude);

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.gameObject.name);
        }

        Debug.DrawLine(transform.position, _previousPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //_player.GetComponent<PlayerController>().PointsBehaviour(100);
            if (other.GetComponent<PlayerController>() != null && other.GetComponent<PlayerController>().RPC_PlayerLife > 0)
            {                
                Debug.Log(other.GetComponent<PlayerController>().RPC_PlayerLife);
                _player.GetComponent<PlayerController>().PointsBehaviour(100);

                other.GetComponent<PlayerController>().TakeDamage(BulletDamage);
                other.GetComponent<PlayerController>().EnemyKillsUpdate(_player.GetComponent<PlayerController>().RCP_PlayerName);                
                //Debug.Log(_player.GetComponent<PlayerController>().RCP_PlayerName);
                if (other.GetComponent<PlayerController>().RPC_PlayerLife <= 0)
                {
                    _player.GetComponent<PlayerController>().PointsBehaviour(1000);
                    //_player.GetComponent<PlayerController>().PlayerKills += 1;
                }
            }
            //GetComponent<PhotonView>().RPC("BulletDamaged", RpcTarget.All, other);
        }
        /*if (other.CompareTag("Enemy"))
        {
            _player.GetComponent<PlayerController>().PointsBehaviour(10);
            
            if (other.GetComponent<EnemyController>().EnemyLife <= 0)
            {
                _player.GetComponent<PlayerController>().PointsBehaviour(100);
            }
        }*/
    }

    /*[PunRPC]
    public void BulletDamaged(Collider other)
    {
        //_player.GetComponent<PlayerController>().PointsBehaviour(100);
        if (other.GetComponent<PlayerController>() != null && other.GetComponent<PlayerController>().PlayerLife > 0)
        {
            Debug.Log(other.GetComponent<PlayerController>().PlayerLife);
            _player.GetComponent<PlayerController>().PointsBehaviour(100);

            other.GetComponent<PlayerController>().TakeDamage(BulletDamage);

            if (other.GetComponent<PlayerController>().PlayerLife <= 0)
            {
                _player.GetComponent<PlayerController>().PointsBehaviour(1000);
                BulletKill();
            }
        }
    }*/

    
    void BulletKill()
    {
        _player.GetComponent<PhotonView>().RPC("PlayerKillsCount", RpcTarget.All, 1);
        //_player.GetComponent<PlayerController>().PlayerKills++;
    }

    IEnumerator BulletDisable()
    {
        yield return new WaitForSeconds(1);
        transform.parent = _bulletPoolGroup.transform.parent;
        gameObject.SetActive(false);
    }
}
