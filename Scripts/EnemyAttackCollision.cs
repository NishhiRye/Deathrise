using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject thisGameObject;
    private int enemyAttack; 
    private void Awake()
    {
        enemyAttack = Enemy.GetComponent<EnemyController>().attackDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<ReceiveDamage>() != null)
            {
                Debug.Log("Player hitado");
                other.GetComponent<ReceiveDamage>().TakeDamage(enemyAttack);                
            }
        }
    }

    
}
