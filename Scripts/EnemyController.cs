using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;


    public int attackDamage = 10;

    public GameObject attackColliderGO;

    public float attackCooldown = 2.633f;
    private float cooldownTime;

    public static float zombieSpeed;
    Transform target_;

    NavMeshAgent agent_;    

    Animator anim;
    void Start()
    {
        agent_ = GetComponent<NavMeshAgent>();
        target_ = GameObject.FindWithTag("Player").transform;
        gameObject.GetComponent<NavMeshAgent>().speed = Random.Range(1, 8);
        anim = GetComponent<Animator>();
        // dropPercentage = Random.Range(1, 5);
        cooldownTime = attackCooldown;

        zombieSpeed = agent_.speed;
    }

    // Update is called once per frame
    void Update()
    {
                FaceTarget();


           float distance = Vector3.Distance(target_.position, transform.position); 


        attackCooldown -= Time.deltaTime;


        //Animations
        if (distance > agent_.stoppingDistance) {
            if (zombieSpeed < 3)
            {
                anim.SetBool("ZombieWalkingBasic", true);
                anim.SetBool("ZombieRunningBasic", false);
            }
            else if (zombieSpeed > 2)
            {
                anim.SetBool("ZombieWalkingBasic", false);
                anim.SetBool("ZombieRunningBasic", true);
            }
        }


        if(distance <= lookRadius)
        {
            agent_.SetDestination(target_.position);

            if(distance <= agent_.stoppingDistance && attackCooldown <= 0)
            {
                anim.SetBool("ZombieWalkingBasic", false);
                anim.SetBool("ZombieRunningBasic", false);
                anim.SetTrigger("ZombieAttackBasic");
                attackCooldown = 2.633f;                

                StartCoroutine(attackHitDelay());
            }
        }
    }



    IEnumerator attackHitDelay()
    {
        yield return new WaitForSeconds(1.3f);

        attackColliderGO.SetActive(true);

        StartCoroutine(ReEnabledHit());

    }



    IEnumerator ReEnabledHit()
    {
        yield return new WaitForSeconds(1);

        attackColliderGO.SetActive(false);
    }



    void FaceTarget()
    {
        Vector3 direction = (target_.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    
}
