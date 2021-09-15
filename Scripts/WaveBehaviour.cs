using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class WaveBehaviour : MonoBehaviourPunCallbacks
{
    public static WaveBehaviour Instance;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;

    public static int enemiesInScene; 
    public GameObject zombie;

    public static Text waveCounter;

    public static int wave = 1;

    public static int totalRoundZombie = 10;
    public static int actualRoundZombie;


    //---------------------------------------------------------------------------
    private void Awake()
    {
        waveCounter = GameObject.FindWithTag("Wave Counter").GetComponent<Text>();
        /*if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;*/
    }

    //---------------------------------------------------------------------------

    void Start()
    {        
        actualRoundZombie = totalRoundZombie;
        //StartCoroutine(WaveSpawn());
        
    }

    // Update is called once per frame
    void Update()
    {
        waveCounter.text = wave.ToString("F0");
        //Debug.Log(totalRoundZombie);
        /*Debug.Log(actualRoundZombie);
        Debug.Log(enemiesInScene);*/
        
    }

    IEnumerator WaveSpawn()
    {
        yield return new WaitForSeconds(3f);

        int spawner = Random.Range(1, 4);
        if(actualRoundZombie > 0 && enemiesInScene < totalRoundZombie)
        {
            if(spawner == 1) 
            {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Zombie"), spawn1.transform.position, spawn1.transform.rotation);
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Zombie"), spawn5.transform.position, spawn5.transform.rotation);                
                actualRoundZombie -= 2;
                StartCoroutine(WaveSpawn());
            }
            else if (spawner == 2)
            {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Zombie"), spawn2.transform.position, spawn2.transform.rotation);
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Zombie"), spawn6.transform.position, spawn6.transform.rotation);
                actualRoundZombie -= 2;
                StartCoroutine(WaveSpawn());
            }
            else if(spawner == 3)
            {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Zombie"), spawn3.transform.position, spawn3.transform.rotation);
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Zombie"), spawn4.transform.position, spawn4.transform.rotation);
                actualRoundZombie -= 2;
                StartCoroutine(WaveSpawn());
            }
        }
        else if (enemiesInScene == 0 && actualRoundZombie == 0)
        {
            //totalRoundZombie = Mathf.RoundToInt((totalRoundZombie + 1) * 1.2f);
            totalRoundZombie += 6;
            StartCoroutine(WaveSpawnDelay());
        }
        else
        {
            StartCoroutine(WaveSpawn());
        }
    }

    IEnumerator WaveSpawnDelay()
    {
        yield return new WaitForSeconds(2f);

        wave++;
        actualRoundZombie = totalRoundZombie;
        StartCoroutine(WaveSpawn());
    }
}
