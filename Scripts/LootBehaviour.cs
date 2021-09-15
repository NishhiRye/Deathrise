using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehaviour : MonoBehaviour
{
    public GameObject lootSpawn1;
    public GameObject lootSpawn2;
    public GameObject lootSpawn3;
    public GameObject lootSpawn4;
    public GameObject lootSpawn5;
    public GameObject lootSpawn6;
    public GameObject loot1;
    public GameObject loot2;
    public GameObject loot3;
    public GameObject loot4;
    public GameObject loot5;
    public GameObject loot6;
    // Start is called before the first frame update
    void Awake()
    {
        loot1.transform.position = lootSpawn1.transform.position;
        loot2.transform.position = lootSpawn2.transform.position;
        loot3.transform.position = lootSpawn3.transform.position;
        loot4.transform.position = lootSpawn4.transform.position;
        loot5.transform.position = lootSpawn5.transform.position;
        loot6.transform.position = lootSpawn6.transform.position;
    }

} 
