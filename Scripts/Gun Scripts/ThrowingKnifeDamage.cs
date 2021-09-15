using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeDamage : MonoBehaviour
{
    public int dealDamage = 10;
    
    // Start is called before the first frame update

    public void OnCollisionEnter(Collision collision)
    {
        ReceiveDamage victim = collision.gameObject.GetComponent<ReceiveDamage>();
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Enemy"))
        {
            victim.TakeDamage(dealDamage);
        }
    }
}
