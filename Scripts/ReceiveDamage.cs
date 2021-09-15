using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ReceiveDamage : MonoBehaviour
{

    public float health = 50f;
    float totalHealth;
    Animator anim;

    public float flint = 1;

    bool isDead = false;

    NavMeshAgent agent_;



    private void Start()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        if(gameObject.GetComponent<NavMeshAgent>()!= null){
            agent_ = GetComponent<NavMeshAgent>();
        }

        totalHealth = health;
    }

    private void Update()
    {
        /*if (health < totalHealth)
        {
            health += Time.deltaTime * 2;
        }*/
    }
    public void TakeDamage (float amount)
    {
        
        
            health -= amount;
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("ORE NO RAIFU " + health);
        }
            if (gameObject.CompareTag("Enemy") && health > 0)
            {
                anim.SetTrigger("ZombieFlintBasic");
                anim.SetBool("ZombieRunningBasic", false);
                anim.SetBool("ZombieWalkingBasic", false);
                gameObject.GetComponent<EnemyController>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;

                StartCoroutine(ReEnabledEnemyController());
            }


            if (health <= 0f)
            {

                if (CompareTag("Target"))
                {
                    Disable();
                }

                else if (gameObject.CompareTag("Enemy") && !isDead)
                {
                    /*GetComponent<EnemyController>().AmmoDrop.SetActive(true);
                    GetComponent<EnemyController>().BatteryDrop.SetActive(true);
                    GetComponent<EnemyController>().AmmoDrop.transform.parent = null;
                    GetComponent<EnemyController>().BatteryDrop.transform.parent = null;*/
                    isDead = true;
                    anim.SetBool("ZombieRunningBasic", false);
                    anim.SetBool("ZombieWalkingBasic", false);
                    gameObject.GetComponent<EnemyController>().enabled = false;
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    gameObject.GetComponent<ReceiveDamage>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                    anim.SetTrigger("DeathAnimBasic");
                    StartCoroutine(Die());

                }

                else if(gameObject.CompareTag("Player"))
                    {
                    SceneManager.LoadScene("End Alpha Scene");
                    }
            }
            
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(10f);

        gameObject.SetActive(false);      


    }

    IEnumerator ReEnabledEnemyController()
    {
        yield return new WaitForSeconds(flint);

        if (!isDead)
        {
            if (agent_.speed < 3)
            {
                anim.SetBool("ZombieWalkingBasic", true);
                anim.SetBool("ZombieRunningBasic", false);
            }
            else if (agent_.speed > 2)
            {
                anim.SetBool("ZombieWalkingBasic", false);
                anim.SetBool("ZombieRunningBasic", true);
            }
            gameObject.GetComponent<EnemyController>().enabled = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
        
    }

    void Disable()
    {
        GetComponent<ReceiveDamage>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<TutorialTargetCount>().enabled = true;
    }
}
