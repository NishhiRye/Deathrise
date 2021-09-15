using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotaBehaviour : MonoBehaviour
{
    Animator anim;
    float animSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animSpeed = Random.Range(0.1f, 1.75f);
        anim.GetComponent<Animator>();
    }

    private void Update()
    {
        anim.speed = animSpeed;
        
    }
}
