using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : WeaponVariable
{
    
    public float damage = 10f;
    public float range = 100f;
    public float ImpactForce = 30f;
    public float FireRate = 15f;
    public float reloadDelay;

    public static int totalBullets;
    public int bullets;
    public static int bulletsInCanvasGun1;
    public static int bulletsInCanvasGun2;

    GameObject player;
    GameObject PlayerCam;

    public GameObject ImpactParticles;
    public GameObject BulletImpact;

    public AudioSource ShotSound;
    
    public AudioSource noAmmoSFX;

    //public AudioSource reloadSFX;

    public float BulletDropSFX = 0.4f;

    public float nextTimeToFire = 0f;

    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PlayerCam = GameObject.FindWithTag("MainCamera");
        totalBullets = bullets;
        
    }

    
    void Update()
    {
        if (gameObject == gun1)
        {
            bulletsInCanvasGun1 = bullets;
        }
        if (gameObject == gun2)
        {
            bulletsInCanvasGun2 = bullets;
        }
        

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && bullets > 0 && gameObject == gun1)
            {
                nextTimeToFire = Time.time + 1f / FireRate;
                Shoot();
                ShotSound.Play();
            bullets -= 1;
            
            
            }
        
        if (Input.GetButtonDown("Fire1") && bullets == 0 && gameObject == gun1)
        {
            noAmmoSFX.Play();
        }
        if (Input.GetButtonDown("Fire2") && Time.time >= nextTimeToFire && bullets > 0 && gameObject == gun2 )
        {
            nextTimeToFire = Time.time + 1f / FireRate;
            Shoot();
            ShotSound.Play();
            bullets -= 1;
            
        }
        
        if (Input.GetButtonDown("Fire2") && bullets == 0 && gameObject == gun2)
        {
            noAmmoSFX.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //reloadSFX.Play();
           StartCoroutine(Reload());
        }
            
            
        
                      
            
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadDelay);
        
        while(bullets < totalBullets && reloadBullets > 0)
        {
            bullets++;
            reloadBullets--;
        }
        
    }

    // Update is called once per frame
    public void Shoot()
    {
        RaycastHit cleitin;
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out cleitin, range))
        {
            
            if (cleitin.collider.gameObject.name == "Headshot Collider")
            {
                cleitin.collider.gameObject.GetComponent<HeadKillZombie>().zombieToKill.GetComponent<ReceiveDamage>().health = 0;
                //cleitin.collider.gameObject.GetComponent<Collider>().enabled = false;
                player.GetComponent<InteractionCanvas>().zombieMoneyPoints += 125;

                //Debug.Log("AXCCERTEI ????");



            }

            ReceiveDamage target = cleitin.transform.GetComponent<ReceiveDamage>();
            

            if(target != null)
            {
                
                
                    target.TakeDamage(damage);
                player.GetComponent<InteractionCanvas>().zombieMoneyPoints += 20;
                
            }
            

            

            if(cleitin.rigidbody != null)
            {
                cleitin.rigidbody.AddForce(-cleitin.normal * ImpactForce);
            }

            GameObject impactGameObject = Instantiate(ImpactParticles, cleitin.point, Quaternion.LookRotation(cleitin.normal));
            Destroy(impactGameObject, 2f);

            GameObject bulletImpactGameObject = Instantiate(BulletImpact, cleitin.point, Quaternion.LookRotation(cleitin.normal));
            Destroy(bulletImpactGameObject, 10f);

        }
    }
}
