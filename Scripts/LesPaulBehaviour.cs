using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesPaulBehaviour : MonoBehaviour
{
    public AudioSource[] LesPaulSongs;
    int _songIndex = 0;
    PlayerController PC;

    // Update is called once per frame
    void Update()
    {
        PC = GetComponentInParent<PlayerController>();
        

        if (PC != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _songIndex++;
                if (_songIndex > LesPaulSongs.Length - 1)
                {
                    _songIndex = 0;
                }
                LesPaulSongs[_songIndex].Play();
                if (_songIndex > 0)
                {
                    LesPaulSongs[_songIndex - 1].Stop();
                }
                else if(_songIndex == 0)
                {
                    LesPaulSongs[LesPaulSongs.Length - 1].Stop();
                }
                
            }
        }
        else
        {
            LesPaulSongs[_songIndex].Stop(); 
        }
    }
}
