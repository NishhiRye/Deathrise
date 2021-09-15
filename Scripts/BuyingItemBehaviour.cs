using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


public class BuyingItemBehaviour : MonoBehaviourPunCallbacks
{

    PlayerController _player;
    BuyBehaviour _buyStationCanvas;

    [SerializeField]
    Image _itemBackground;

    [SerializeField]
    int _itemPrice;

    [SerializeField]
    Text _itemPriceText;

    [SerializeField]
    GameObject _itemBuyed;

    [SerializeField]
    string _itemName;

    [SerializeField]
    GameObject _myBuyButton;

    [SerializeField]
    GameObject _buyButton2;

    [SerializeField]
    GameObject _buyButton3;

    [SerializeField]
    GameObject _buyButton4;

    [SerializeField]
    GameObject _buyButton5;


    float _alphaColor = 0.1f;
    Color _backgroundColor;

    /*[SerializeField]
    GameObject _itemSpawn;*/
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInParent<PlayerController>();
        _buyStationCanvas = GetComponentInParent<BuyBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        _itemBackground.color = _backgroundColor;
        _backgroundColor.a = _alphaColor;

        _itemPriceText.text = "$" + _itemPrice.ToString();
        if (_player.Points < _itemPrice)
        {
            _backgroundColor = Color.red;
            _backgroundColor.a = _alphaColor;
        }
        else if (_player.Points >= _itemPrice)
        {
            _backgroundColor = Color.green;
            _backgroundColor.a = _alphaColor;

        }

    }

    public void BuyItem()
    {

        if (_player.Points >= _itemPrice)
        {
            Debug.Log("comprei " + _itemBuyed.name);
            _myBuyButton.SetActive(true);
            if(_buyButton2 != null)
            {
            _buyButton2.SetActive(false);
            }
            if (_buyButton3 != null)
            {
                _buyButton3.SetActive(false);
            }
            if (_buyButton4 != null)
            {
                _buyButton4.SetActive(false);
            }
            if (_buyButton5 != null)
            {
                _buyButton5.SetActive(false);
            }
                      
        }
        else
        {

            _myBuyButton.SetActive(false);
            if (_buyButton2 != null)
            {
                _buyButton2.SetActive(false);
            }
            if (_buyButton3 != null)
            {
                _buyButton3.SetActive(false);
            }
            if (_buyButton4 != null)
            {
                _buyButton4.SetActive(false);
            }
            if (_buyButton5 != null)
            {
                _buyButton5.SetActive(false);
            }
        }
    }

    public void BuyNow()
    {
        if (_player.Points >= _itemPrice)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", _itemBuyed.name), _player.transform.position, _player.transform.rotation);
            _player.Points -= _itemPrice;
            _myBuyButton.SetActive(false);
            if (_buyButton2 != null)
            {
                _buyButton2.SetActive(false);
            }
            if (_buyButton3 != null)
            {
                _buyButton3.SetActive(false);
            }
            if (_buyButton4 != null)
            {
                _buyButton4.SetActive(false);
            }
            if (_buyButton5 != null)
            {
                _buyButton5.SetActive(false);
            }
        }
        return;
    }

    public void BuyAmmoEquipmentNow()
    {
        if (_player.Points >= _itemPrice)
        {
            _player.PistolAmmoStorage += 30;
            _player.ShotGunAmmoStorage += 30;
            _player.AssaultRifleAmmoStorage += 50;
            _player.SniperAmmoStorage += 10;
            _player.SubmachineGunAmmoStorage += 50;
            _player.Points -= _itemPrice;
        }
    }

    public void BuyShieldEquipment()
    {
        if (_player.Points >= _itemPrice)
        {

            //_player.PV.RPC("RPC_RefilShield", RpcTarget.All, 25);
            _player.RefilShield(25);
            _player.Points -= _itemPrice;

        }
    }
}
