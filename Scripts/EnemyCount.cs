using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : UnlockBehaviour
{
    private void Awake()
    {
        WaveBehaviour.enemiesInScene++;
    }
    private void OnDisable()
    {
        enemyCount += 1;
        WaveBehaviour.enemiesInScene--;

    }

}
