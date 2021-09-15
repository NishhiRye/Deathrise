using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WeaponController : MonoBehaviourPunCallbacks
{

    //Game Variables
    //-------------------------------------------------------------
    PhotonView PV;
    PlayerController _playerController;
    public float GunWheight;
    //-------------------------------------------------------------

    //WeaponSway Variables
    //-------------------------------------------------------------
    public float amount = 0.04f;
    public float maxAmount = 0.4f;
    public float smoothAmount = 6f;

    Vector3 _initialPosition;
    //-------------------------------------------------------------

    //WeaponShot Variables
    //-------------------------------------------------------------
    [SerializeField]
    GameObject _bullet;


    public GameObject BulletSpawn;

    [SerializeField]
    ParticleSystem _muzzleFlash;

    public float GunDamage;

    public float BulletVelocity = 600f;

    public float cadence;

    [HideInInspector]
    public bool IsRightGun = false;

    [HideInInspector]
    public bool IsLeftGun = false;

    [HideInInspector]
    public bool CanShoot = true;

    [SerializeField]
    bool _isAutomaticGun;

    [SerializeField]
    Transform _bulletCapsuleSpawn;

    [SerializeField]
    GameObject _bulletCapsule;
    //-------------------------------------------------------------

    //Ammo & Reload Variables
    //-------------------------------------------------------------
    public enum SelectWeaponClass
    {
        Sniper,
        AssaultRifle,
        SubmachineGun,
        Pistol,
        ShotGun,
        WhiteGun
    };

    public SelectWeaponClass SelectGunClass;

    int _totalAmmo;

    public int MagazineMaxAmmo;

    [HideInInspector]
    public int CurrentAmmo;

    int _sniperAmmo;

    int _assaultRifleAmmo;

    int _submachineGunAmmo;

    int _pistolAmmo;

    int _shotGunAmmo;
    public float ReloadTime = 2f;

    public string GunName;

    [HideInInspector]
    public bool _isReloading = false;
    //--------------------------------------------------------------

    //FlashLight Variables
    //-------------------------------------------------------------
    [SerializeField]
    GameObject _flashLight;

    [HideInInspector]
    public bool LightIsEnabled = false;

    [HideInInspector]
    public float FlashLightBattery = 100f;

    float _flashLightBatteryDecay;

    [HideInInspector]
    public float ReloadMultiplier = 1;//***************************************************************************************************
    //-------------------------------------------------------------

    //WeaponRecoil Variables
    //------------------------------------------------------------- 
    [SerializeField]
    float _horizontalRecoil = 1f;
    float _h;

    [SerializeField]
    float _verticalRecoil = 1f;
    float _v;
    //-------------------------------------------------------------

    //Bullet Pooling Variables
    //-------------------------------------------------------------    
    List<GameObject> _bulletToPool;
    int _bulletAmount;
    int _bulletToShoot = 1;
    //-------------------------------------------------------------

    //Animations Rigging Variables
    //-------------------------------------------------------------  
    public GameObject LeftHandRigTarget;
    public GameObject RightHandRigTarget;

    //-------------------------------------------------------------

    //Audio Variables
    //-------------------------------------------------------------
    [SerializeField]
    AudioSource _gunShotSFX;
    [SerializeField]
    AudioSource _gunNoAmmoSFX;
    [SerializeField]
    AudioSource _gunReloadingSFX;
    //-------------------------------------------------------------


    #region OnEnable

    void OnEnable()
    {

        _initialPosition = transform.localPosition;
    }

    #endregion


    #region Start

    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        AmmoBehaviour();

        /*_bulletToPool = new List<GameObject>();
        for (int i = 0; i < _bulletAmount; i++)
        {
            GameObject obj = Instantiate(_bullet);
            obj.SetActive(false);
            _bulletToPool.Add(obj);
        }*/

    }

    #endregion


    #region Update

    void Update()
    {

        /*if (!PV.IsMine)
        {
            return;
        }*/
        if (_playerController != null)
        {
            if (_playerController.IsPaused)
            {
                return;
            }
            if (_playerController.IsBuying)
            {
                return;
            }
        }

        WeaponSway();

        FlashLight();

        WeaponShotBehaviour();

        ReloadBehaviour();

        BulletPoolingBehaviour();

        WeaponRecoilCorrection();

    }

    #endregion


    #region LateUpdate

    void LateUpdate()
    {


    }


    #endregion


    #region WeaponSway

    void WeaponSway()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosiiton = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosiiton + _initialPosition, Time.deltaTime * smoothAmount);
    }

    #endregion


    #region FlashLight

    void FlashLight()
    {
        FlashLightDecay();

        if (_flashLight != null && FlashLightBattery > 0)
        {
            //if (WeaponVariable.batteryReload > 0)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (LightIsEnabled)
                    {
                        FlashLightOff();
                    }
                    else
                    {
                        FlashLightOn();
                    }

                }
            }
            /*else
            {
                _flashLight.SetActive(false);
                lightIsEnabled = false;
            }*/
        }

        else
        {
            return;
        }
    }

    void FlashLightDecay()
    {

        if (LightIsEnabled)
        {
            FlashLightBattery -= (Time.deltaTime * _flashLightBatteryDecay);

        }

        if (!LightIsEnabled)
        {

        }

        FlashLightBattery = Mathf.Clamp(FlashLightBattery, 0, 100);

        return;

    }

    void FlashLightOn()
    {
        _flashLight.SetActive(true);
        LightIsEnabled = true;
    }


    void FlashLightOff()
    {
        _flashLight.SetActive(false);
        LightIsEnabled = false;
    }

    #endregion


    #region WeaponShotBehaviour

    void WeaponShotBehaviour()
    {

        if (CurrentAmmo > 0 || SelectGunClass == SelectWeaponClass.WhiteGun)
        {

            if (CanShoot)
            {
                if (_isAutomaticGun)
                {

                    if (Input.GetKey(KeyCode.Mouse0) && IsRightGun && !_isReloading)
                    {
                        if (SelectGunClass != SelectWeaponClass.WhiteGun)
                        {
                            //PV.RPC("Shot", RpcTarget.All);
                            Shot();


                        }
                        else
                        {
                            return;
                        }

                    }
                    if (Input.GetKey(KeyCode.Mouse1) && IsLeftGun && !_isReloading)
                    {

                        if (SelectGunClass != SelectWeaponClass.WhiteGun)
                        {
                            //PV.RPC("Shot", RpcTarget.All);
                            Shot();

                        }

                        else
                        {
                            return;
                        }

                    }
                }
                else if (!_isAutomaticGun)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && IsRightGun)
                    {
                        //PV.RPC("Shot", RpcTarget.All);
                        Shot();

                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && IsLeftGun)
                    {
                        //PV.RPC("Shot", RpcTarget.All);
                        Shot();

                    }
                }
            }
        }
        else
        {
            CanShoot = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && IsRightGun && CurrentAmmo == 0)
        {
            _gunNoAmmoSFX.Play();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && IsLeftGun && CurrentAmmo == 0)
        {
            _gunNoAmmoSFX.Play();
        }

    }

    [PunRPC]
    void Shot()
    {
        if (_isReloading)
        {
            StartCoroutine(ShotCadence());
            return;
        }

        _gunShotSFX.PlayOneShot(_gunShotSFX.clip, 1);

        CanShoot = false;

        CurrentAmmo--;


        //Play shot animation
        //Play shot Sound FX        

        //_muzzleFlash.Play();
        if (_bulletCapsule != null)
        {
            GameObject BulletCapsule = Instantiate(_bulletCapsule, _bulletCapsuleSpawn.transform.position, _bulletCapsuleSpawn.transform.rotation);
            BulletCapsule.GetComponent<Rigidbody>().AddForce(BulletSpawn.transform.forward * 200);
            StartCoroutine(BulletCapsulePooling(BulletCapsule));
        }

        WeaponRecoil();


        if (_playerController.BulletsToRecycle.Count > 0)
        {
            _playerController.BulletsToRecycle[_bulletToShoot].SetActive(true);
            _playerController.BulletsToRecycle[_bulletToShoot].GetComponent<BulletBehaviour>().BulletDamage = GunDamage;
            _playerController.BulletsToRecycle[_bulletToShoot].transform.position = BulletSpawn.transform.position;
            _playerController.BulletsToRecycle[_bulletToShoot].transform.rotation = BulletSpawn.transform.rotation;
            _playerController.BulletsToRecycle[_bulletToShoot].GetComponent<Rigidbody>().isKinematic = false;
            _playerController.BulletsToRecycle[_bulletToShoot].GetComponent<Rigidbody>().AddForce(-BulletSpawn.transform.right * BulletVelocity * 100);
            _playerController.BulletsToRecycle[_bulletToShoot].transform.parent = null;

        }

        _bulletToShoot += 1;

        if (_bulletToShoot > 70)
        {
            _bulletToShoot = 1;
        }
        //Debug.Log(_bulletToShoot);
        StartCoroutine(ShotCadence());
    }
    IEnumerator BulletCapsulePooling(GameObject BulletCapsule)
    {

        yield return new WaitForSeconds(5f);
        BulletCapsule.SetActive(false);
    }

    [PunRPC]
    void Attack()
    {
        CanShoot = false;

        StartCoroutine(ShotCadence());
    }

    IEnumerator ShotCadence()
    {
        yield return new WaitForSeconds(1 / cadence);

        if (!CanShoot)
        {
            CanShoot = true;
        }
    }

    #endregion


    #region WeaponRecoil

    void WeaponRecoil()
    {
        _h = Random.Range(-_horizontalRecoil, _horizontalRecoil);
        _v = _verticalRecoil;

        _playerController.AddRecoil(_v, _h);

        StartCoroutine(WeaponRecoilCorrection());

    }

    IEnumerator WeaponRecoilCorrection()
    {
        yield return new WaitForSeconds(Time.deltaTime); //||

        _v -= Time.deltaTime * 3;
        _h -= Time.deltaTime * 3;

        _v = Mathf.Clamp(_v, 0f, 10f);
        _h = Mathf.Clamp(_h, 0f, 10f);

        _playerController.AddRecoil(_v, _h);

        if (_v != 0 || _h != 0)
        {
            StartCoroutine(WeaponRecoilCorrection());
        }
    }

    #endregion


    #region AmmoBehaviour

    void AmmoBehaviour()
    {
        StartCoroutine(AmmoCheck());
    }

    IEnumerator AmmoCheck()
    {
        yield return new WaitForSeconds(0.04f);

        _pistolAmmo = _playerController.PistolAmmoStorage;
        _sniperAmmo = _playerController.SniperAmmoStorage;
        _assaultRifleAmmo = _playerController.AssaultRifleAmmoStorage;
        _submachineGunAmmo = _playerController.SubmachineGunAmmoStorage;
        _shotGunAmmo = _playerController.ShotGunAmmoStorage;


        //Debug.Log("Pistol ammo " + _pistolAmmo);
    }
    #endregion


    #region  ReloadBehaviour

    void ReloadBehaviour()
    {
        switch (SelectGunClass)
        {
            case SelectWeaponClass.AssaultRifle:
                _totalAmmo = _playerController.AssaultRifleAmmoStorage;
                break;


            case SelectWeaponClass.SubmachineGun:
                _totalAmmo = _playerController.SubmachineGunAmmoStorage;
                break;


            case SelectWeaponClass.ShotGun:
                _totalAmmo = _playerController.ShotGunAmmoStorage;
                break;


            case SelectWeaponClass.Sniper:
                _totalAmmo = _playerController.SniperAmmoStorage;
                break;


            case SelectWeaponClass.Pistol:
                _totalAmmo = _playerController.PistolAmmoStorage;
                break;
        }


        if (_totalAmmo == 0 || CurrentAmmo == MagazineMaxAmmo)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.R) && SelectGunClass != SelectWeaponClass.WhiteGun && !_isReloading)
        {
            _isReloading = true;
            CanShoot = false;
            StartCoroutine(Reloading());
            _gunReloadingSFX.Play();
            _playerController._reloadingText.enabled = true;
        }

    }

    IEnumerator Reloading()
    {

        yield return new WaitForSeconds(ReloadTime * ReloadMultiplier);

        _isReloading = false;
        _playerController._reloadingText.enabled = false;

        if (SelectGunClass == SelectWeaponClass.Sniper)
        {
            while (CurrentAmmo < MagazineMaxAmmo && _playerController.SniperAmmoStorage > 0)
            {
                CanShoot = true;
                CurrentAmmo++;
                _playerController.SniperAmmoStorage -= 1;
            }


        }

        if (SelectGunClass == SelectWeaponClass.AssaultRifle)
        {

            while (CurrentAmmo < MagazineMaxAmmo && _playerController.AssaultRifleAmmoStorage > 0)
            {
                CanShoot = true;
                CurrentAmmo++;
                _playerController.AssaultRifleAmmoStorage -= 1;
            }

        }

        if (SelectGunClass == SelectWeaponClass.SubmachineGun)
        {

            while (CurrentAmmo < MagazineMaxAmmo && _playerController.SubmachineGunAmmoStorage > 0)
            {
                CanShoot = true;
                CurrentAmmo++;
                _playerController.SubmachineGunAmmoStorage -= 1;
            }

        }

        if (SelectGunClass == SelectWeaponClass.Pistol)
        {

            while (CurrentAmmo < MagazineMaxAmmo && _playerController.PistolAmmoStorage > 0)
            {
                CanShoot = true;
                CurrentAmmo++;
                _playerController.PistolAmmoStorage -= 1;
            }

        }

        if (SelectGunClass == SelectWeaponClass.ShotGun)
        {

            while (CurrentAmmo < MagazineMaxAmmo && _playerController.ShotGunAmmoStorage > 0)
            {
                CanShoot = true;
                CurrentAmmo++;
                _playerController.ShotGunAmmoStorage -= 1;
            }

        }



        CurrentAmmo = Mathf.Clamp(CurrentAmmo, 0, MagazineMaxAmmo);
    }

    #endregion


    #region BulletPoolingBehaviour

    void BulletPoolingBehaviour()
    {

    }

    #endregion
}


