using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class MainManager : WeaponVariable  
{
    public static bool IsMultiplayer = false;

    public GameObject AWPInstantiator;     
    public GameObject LeftHandInstantiator;
    public GameObject RightHandInstantiator;
    public GameObject LeftLanternInstantiator;

    public GameObject PlayerCam;

    public GameObject Lantern;
    public GameObject Magnum;
    public GameObject AWP;
    public GameObject Glock;

    //public GameObject tutorialCard;
    public GameObject FirstKillCard;
    public GameObject tenKillCard;
    public GameObject FirstDeathCard;
    public GameObject FirstKnifeKillCard;
    public GameObject TripleKnifeKillCard;
    public GameObject TenWaveUnlockCard;

    PhotonView PV;    

    
    bool _playerIsControled = false;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    /*private void Awake()
    {
        if (!UnlockBehaviour.tutorialSkip) 
        {
            UnlockBehaviour.tutorialSkip = true;
            StartCoroutine(UnlockCard());
                }
    }

    IEnumerator UnlockCard()
    {
        yield return new WaitForSeconds (2f);

        tutorialCard.SetActive(true);        
        StartCoroutine(DisableCard());
    }

    IEnumerator DisableCard()
    {
        yield return new WaitForSeconds(5f);
        tutorialCard.SetActive(false);
    }
    // Update is called once per frame
    void Start()
    {
        
        if(WorldVariableCount.WorldVariable == -1)
        {
            GameObject InstanceGun = Instantiate(Glock, RightHandInstantiator.transform.position, RightHandInstantiator.transform.rotation);
            gun = InstanceGun; 
            //Debug.Log("uai");
            InstanceGun.GetComponent<Rigidbody>().isKinematic = true;

            InstanceGun.transform.position = RightHandInstantiator.transform.position;
            InstanceGun.transform.rotation = RightHandInstantiator.transform.rotation;
            InstanceGun.transform.parent = PlayerCam.transform;

            InstanceGun.GetComponent<FlashLight>().enabled = true;
            InstanceGun.GetComponent<WeaponShoot>().enabled = true;

            InstanceGun.GetComponent<WeaponThrow>().enabled = true;
            InstanceGun.GetComponent<WeaponSway>().enabled = true;

            gun1 = gun;
            gun = null;
            RightHandEquiped = true;
        }
        if (WorldVariableCount.WorldVariable == -2)
        {
            GameObject InstanceGun = Instantiate(Magnum, RightHandInstantiator.transform.position, RightHandInstantiator.transform.rotation);
            GameObject InstanceLantern = Instantiate(Lantern, LeftLanternInstantiator.transform.position, LeftLanternInstantiator.transform.rotation);
            gun = InstanceGun;
            InstanceGun.GetComponent<Rigidbody>().isKinematic = true;

            InstanceGun.transform.position = RightHandInstantiator.transform.position;
            InstanceGun.transform.rotation = RightHandInstantiator.transform.rotation;
            InstanceGun.transform.parent = PlayerCam.transform;

            
            InstanceGun.GetComponent<WeaponShoot>().enabled = true;

            InstanceGun.GetComponent<WeaponThrow>().enabled = true;
            InstanceGun.GetComponent<WeaponSway>().enabled = true;

            gun1 = gun;
            gun = null;
            RightHandEquiped = true;


            gun = InstanceLantern;
            InstanceLantern.GetComponent<Rigidbody>().isKinematic = true;

            
            InstanceLantern.transform.parent = PlayerCam.transform;


            InstanceLantern.GetComponent<FlashLight>().enabled = true;

            InstanceLantern.GetComponent<WeaponThrow>().enabled = true;
            InstanceLantern.GetComponent<WeaponSway>().enabled = true;

            gun2 = gun;
            gun = null;
            LeftHandEquiped = true;
        }
        if (WorldVariableCount.WorldVariable == -3)
        {
            GameObject InstanceGun = Instantiate(AWP, AWPInstantiator.transform.position, AWPInstantiator.transform.rotation);
            GameObject InstanceLantern = Instantiate(Lantern, LeftLanternInstantiator.transform.position, LeftLanternInstantiator.transform.rotation);
            gun = InstanceGun;
            InstanceGun.GetComponent<Rigidbody>().isKinematic = true;

            InstanceGun.transform.position = AWPInstantiator.transform.position;
            InstanceGun.transform.rotation = AWPInstantiator.transform.rotation;
            InstanceGun.transform.parent = PlayerCam.transform;


            InstanceGun.GetComponent<WeaponShoot>().enabled = true;

            InstanceGun.GetComponent<WeaponThrow>().enabled = true;
            InstanceGun.GetComponent<WeaponSway>().enabled = true;

            gun1 = gun;
            gun = null;
            RightHandEquiped = true;


            gun = InstanceLantern;
            InstanceLantern.GetComponent<Rigidbody>().isKinematic = true;

            
            InstanceLantern.transform.parent = PlayerCam.transform;


            InstanceLantern.GetComponent<FlashLight>().enabled = true;

            InstanceLantern.GetComponent<WeaponThrow>().enabled = true;
            InstanceLantern.GetComponent<WeaponSway>().enabled = true;

            gun2 = gun;
            gun = null;
            LeftHandEquiped = true;

        }

    }*/
    void CreatePlayer()
    {
        
        
    }
    private void Update()
    {
        /*if (PV.IsMine && _players ==0 && !_playerIsControled)
        {
            Player1.SetActive(true);
            _players++;
            _playerIsControled = true;
        }
        else if (PV.IsMine && _players == 1 && !_playerIsControled)
        {
            Player2.SetActive(true);
            _players++;
            _playerIsControled = true;
        }
        else if (PV.IsMine && _players == 2 && !_playerIsControled)
        {
            Player3.SetActive(true);
            _players++;
            _playerIsControled = true;
        }
        else if (PV.IsMine && _players == 3 && !_playerIsControled)
        {
            Player4.SetActive(true);
            _players++;
            _playerIsControled = true;
        }*/

        //Debug.Log(UnlockBehaviour.enemyCount);
        if (UnlockBehaviour.enemyCount == 1.0000 && !UnlockBehaviour.FirstKillCard)
            {
                StartCoroutine(UnlockCardFirstKill());
            }

        if (UnlockBehaviour.enemyCount == 10.00000 && !UnlockBehaviour.tenKillCard)
        {
            StartCoroutine(UnlockCardTenKill());
        }

        if(WaveBehaviour.wave == 10 && !UnlockBehaviour.TenWaveCard)
        {
            StartCoroutine(UnlockCardTenWave());
        }
    }

    //Ten Kills
    IEnumerator UnlockCardTenKill()
    {
        yield return new WaitForSeconds(2f);

        tenKillCard.SetActive(true);
        UnlockBehaviour.MagnumUnlock = true;
        UnlockBehaviour.tenKillCard = true;
        StartCoroutine(DisableCardTenKill());
    }

    IEnumerator DisableCardTenKill()
    {
        yield return new WaitForSeconds(5f);
        tenKillCard.SetActive(false);
    }

    //First Kill
    IEnumerator UnlockCardFirstKill()
        {
            yield return new WaitForSeconds(2f);

        FirstKillCard.SetActive(true);
        UnlockBehaviour.FirstKillCard = true;
        StartCoroutine(DisableCardFirstKill());
        }

    IEnumerator DisableCardFirstKill()
        {
            yield return new WaitForSeconds(5f);
        FirstKillCard.SetActive(false);
        }

    //Ten wave

    IEnumerator UnlockCardTenWave()
    {
        yield return new WaitForSeconds(2f);

        TenWaveUnlockCard.SetActive(true);
        UnlockBehaviour.TenWaveCard = true;
        UnlockBehaviour.AWPVar = true;
        StartCoroutine(DisableCardTenWave());
    }

    IEnumerator DisableCardTenWave()
    {
        yield return new WaitForSeconds(5f);
        TenWaveUnlockCard.SetActive(false);
    }



}
