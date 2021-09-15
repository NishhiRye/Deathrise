using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviourPunCallbacks
{

    CharacterController controller;

    bool _enablePlayer;

    float _baseSpeed;
    public float playerSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float RunSpeed = 8f;
    bool _isRunning = false;
    float _playerWheight;

    Animation _animation;
    bool _left = true;
    bool _right;
    bool _isMoving;
    bool _leftFoot = true;
    bool _rightFoot = false;

    [SerializeField]
    float _footstepRate = 1.5f;

    [SerializeField]
    AudioSource _rightFootstep;

    [SerializeField]
    AudioSource _leftFootstep;

    public Transform groundCheck;

    [HideInInspector]
    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    [HideInInspector]
    public PhotonView PV;



    [SerializeField]
    Camera _playerCam;
    float _interactionRange = 3.5f;

    [SerializeField]
    Transform _rightHandTrans;

    [SerializeField]
    Transform _leftHandTrans;

    [HideInInspector]
    public GameObject RightGun;
    [HideInInspector]
    public GameObject LeftGun;

    [HideInInspector]
    public bool LeftHand = false;
    [HideInInspector]
    public bool RightHand = false;

    [SerializeField]
    float _throwForce = 4000f;


    public Slider StaminaSlider;
    public Slider SecundaryStaminaSlider;
    [HideInInspector]
    public float currentPrimaryStamina = 1f;
    [HideInInspector]
    public float currentSecundaryStamina = 0f;
    [HideInInspector]
    public bool SecundaryStaminaIsActive = false;
    [SerializeField]
    GameObject _secundaryStaminaFillArea;



    Vector3[] _spawnPositions = new[] { new Vector3(11f, 18f, -118f), new Vector3(60f, 18f, -116f), new Vector3(60f, 18f, 26f), new Vector3(38f, 19f, 6f), new Vector3(-225f, 28f, -74f), new Vector3(-149f, 18f, -144f), new Vector3(37f, 18f, 4f) };
    /*/_spawnPositions[0] = new Vector3(11f, 18f, -118f);
    _spawnPositions[1] = new Vector3(100f, 18f, -107f);
    _spawnPositions[2] = new Vector3(146f, 18f, 29f);
    _spawnPositions[3] = new Vector3(51f, 26f, 66f);
    _spawnPositions[4] = new Vector3(-225f, 28f, -74f);
    _spawnPositions[5] = new Vector3(-149f, 18f, -144f);
    _spawnPositions[6] = new Vector3(37f, 18f, -144f);*/

    //MouseLook Variables
    //-------------------------------------------------------------

    public float mouseSensitivity = 250f;

    float _xRotation;
    //--------------------------------------------------------------

    //AmmoStorage Variables
    //--------------------------------------------------------------

    [HideInInspector]
    public int SniperAmmoStorage = 10;

    [HideInInspector]
    public int AssaultRifleAmmoStorage = 50;

    [HideInInspector]
    public int SubmachineGunAmmoStorage = 40;

    [HideInInspector]
    public int PistolAmmoStorage = 30;

    [HideInInspector]
    public int ShotGunAmmoStorage = 20;


    [HideInInspector]
    public int SniperAmmoMaxStorage = 40; //*************************************************************************************************

    [HideInInspector]
    public int AssaultRifleAmmoMaxStorage = 210; //******************************************************************************************

    [HideInInspector]
    public int SubmachineGunAmmoMaxStorage = 150; //*****************************************************************************************

    [HideInInspector]
    public int PistolAmmoMaxStorage = 100; //*************************************************************************************************

    [HideInInspector]
    public int ShotGunAmmoMaxStorage = 80; //************************************************************************************************

    //--------------------------------------------------------------

    //Helmet Variables
    //--------------------------------------------------------------
    [SerializeField]
    GameObject _helmetLight;

    float _helmetBattery = 1f;

    [SerializeField]
    float _helmetBatteryDecay = 2f; //******************************************************************************************************

    bool _helmetIsOn = false;
    //--------------------------------------------------------------

    //Text OnScreen Variables
    //--------------------------------------------------------------
    [SerializeField]
    Text _moneyText;
    [HideInInspector]
    public int Points;
    [SerializeField]
    Text _gunAmmoText;

    [SerializeField]
    Text _gunBatteryText;

    /* [SerializeField]
     Text _pointsOnScreen;*/

    [SerializeField]
    GameObject _pointsOnScreenGameObject;

    [SerializeField]
    Transform _pointsOnScreenSpawner;


    [SerializeField]
    Slider _lifeSlider;

    [SerializeField]
    Slider _primaryShieldSlider;

    [SerializeField]
    Slider _secundaryShieldSlider;

    [HideInInspector]
    public bool SecundaryShieldIsActive = false;
    [SerializeField]
    GameObject _secundaryShieldFillArea;


    [SerializeField]
    Text _timerText;

    [SerializeField]
    Text _dayText;


    public int _dayInt = 0;


    public int _hourInt = 10;


    public int _minuteInt = 0;


    [SerializeField]
    Text _playerNickOnScreen;

    [SerializeField]
    Text _aRAmmoText;

    [SerializeField]
    Text _sMGAmmoText;

    [SerializeField]
    Text _sniperAmmoText;

    [SerializeField]
    Text _shotgunAmmoText;

    [SerializeField]
    Text _pistolAmmoText;

    [SerializeField]
    GameObject _ammoVariations;

    [SerializeField]
    Text _maxAmmoOnScreen;

    [SerializeField]
    Text _noAmmoText;

    [SerializeField]
    Text _helmetText;

    [SerializeField]
    Slider _helmetSlider;




    [SerializeField]
    GameObject _pistolCrosshair;    

    [SerializeField]
    GameObject _submachinegunCrosshair;

    [SerializeField]
    GameObject _assaultRifleCrosshair;

    [SerializeField]
    GameObject _shotgunCrosshair;




    [SerializeField]
    GameObject _rightGlockSil;

    [SerializeField]
    GameObject _leftGlockSil;

    [SerializeField]
    GameObject _right357Sil;

    [SerializeField]
    GameObject _left357Sil;

    [SerializeField]
    GameObject _rightKatanaSil;

    [SerializeField]
    GameObject _leftKatanaSil;

    [SerializeField]
    GameObject _rightSwordSil;

    [SerializeField]
    GameObject _leftSwordSil;

    [SerializeField]
    GameObject _aWPSil;

    [SerializeField]
    GameObject _famasSil;

    [SerializeField]
    GameObject _aA12Sil;

    [SerializeField]
    GameObject _vectorSil;

    [SerializeField]
    GameObject _p90Sil;

    [SerializeField]
    GameObject _mAR14Sil;

    [SerializeField]
    GameObject _lesPaulSil;


    public Text _reloadingText;
    //--------------------------------------------------------------

    //Recoil Variables
    //--------------------------------------------------------------
    [HideInInspector]
    public float _horizontalRecoil;
    [HideInInspector]
    public float _verticalRecoil;
    //--------------------------------------------------------------

    //Animation Rigging Variables
    //--------------------------------------------------------------
    [SerializeField]
    Transform _initialRightHandRigTarget;

    [SerializeField]
    TwoBoneIKConstraint _rightHandIK;

    [SerializeField]
    Transform _initialLeftHandRigTarget;

    [SerializeField]
    TwoBoneIKConstraint _leftHandIK;

    //--------------------------------------------------------------

    //Life And Death Behaviour Variables
    //--------------------------------------------------------------
    [HideInInspector]
    public float PlayerLife = 1f;
    [HideInInspector]
    public float RPC_PlayerLife;
    [HideInInspector]
    public float PlayerPrimaryShield = 1f;
    [HideInInspector]
    public float PlayerSecundaryShield = 1f;
    [HideInInspector]
    bool _playerIsDead = false;

    [SerializeField]
    GameObject _playerCanvas;

    [SerializeField]
    GameObject _deathCanvas;

    [HideInInspector]
    public string PlayerThatShotMe;

    //drop all items...
    //--------------------------------------------------------------

    //Multipliers Variables
    //--------------------------------------------------------------
    [SerializeField]
    float _playerSpeedMultiplier = 1f;//****************************************************************************************************

    [SerializeField, Range(0f, 1f)]
    float _staminaMultiplier = 1f;//********************************************************************************************************

    [SerializeField, Range(0f, 1f)]
    float _damageMultiplier = 1f;//*********************************************************************************************************

    [SerializeField, Range(0f, 1f)]//Mais dinheiro, mais muniçao, itens mais baratos;
    float _luckMultiplier = 1f;//***********************************************************************************************************

    [SerializeField, Range(0f, 1f)]
    float _recoilMultiplier = 1f;//***********************************************************************************************************

    [SerializeField, Range(0f, 1f)]
    float _mouseSensitivityMultiplier = 1f;//*************************************************************************************************

    [SerializeField, Range(0f, 1f)]
    float _resistanceMultiplier = 1f;//*************************************************************************************************

    //tracker mechanics maybe...
    //claymore...
    //monkey bomb...
    //--------------------------------------------------------------

    //Bullet Pooling Variables
    //--------------------------------------------------------------
    public List<GameObject> BulletsToRecycle = new List<GameObject>();
    //--------------------------------------------------------------

    //Pause Behaviour
    //--------------------------------------------------------------
    [SerializeField]
    GameObject _pauseCanvas;

    [SerializeField]
    GameObject _inGameMenuCanvas;

    [SerializeField]
    GameObject _optionsCanvas;

    [HideInInspector]
    public bool IsPaused = false;
    //--------------------------------------------------------------

    //Options Variables
    //------------------------------------------------------------------------------------
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    Resolution[] _resolutions;
    public Dropdown ResolutionDropdown;
    public Slider MouseSensSlider;
    //------------------------------------------------------------------------------------

    //LeaderBoard Variables
    //------------------------------------------------------------------------------------
    [HideInInspector]
    public bool PlayerIsOn = false;

    [HideInInspector]
    public float PlayerPoints;

    [HideInInspector]
    public float RCP_PlayerPoints;

    [HideInInspector]
    public int PlayerKills;

    [HideInInspector]
    public int RCP_PlayerKills;

    [HideInInspector]
    public int PlayerDeaths;

    [HideInInspector]
    public int RCP_PlayerDeaths;

    [HideInInspector]
    public string PlayerName;

    [HideInInspector]
    public string RCP_PlayerName;


    [SerializeField]
    Text[] _playersNames = new Text[7];

    [SerializeField]
    Text[] _playersKills = new Text[7];

    [SerializeField]
    Text[] _playersDeaths = new Text[7];

    [SerializeField]
    Text[] _playersPoints = new Text[7];

    bool _player1InGame;
    bool _player2InGame;
    bool _player3InGame;
    bool _player4InGame;
    bool _player5InGame;
    bool _player6InGame;
    bool _player7InGame;
    bool _player8InGame;

    [SerializeField]
    GameObject[] _playersInGame;
    //------------------------------------------------------------------------------------

    //LeaderBoard Variables
    //------------------------------------------------------------------------------------
    [SerializeField]
    GameObject _charSpawn;

    [SerializeField]
    GameObject _charPrefab;

    GameObject _characterMain;

    bool _charIsOn = false;
    //------------------------------------------------------------------------------------

    //Buying Variables
    //------------------------------------------------------------------------------------
    [SerializeField]
    GameObject BuyStationCanvas;

    [SerializeField]
    GameObject EquipmentsToBuyCanvas;

    [SerializeField]
    GameObject PatchesToBuyCanvas;

    [SerializeField]
    GameObject WeaponsToBuyCanvas;

    int _activePatchCanvas = 0;

    [HideInInspector]
    public bool IsBuying = false;

    bool _isBuyingEquipments = false;
    bool _isBuyingPatches = false;
    bool _isBuyingWeapons = false;

    [SerializeField]
    GameObject[] _PatchesLayers = new GameObject[5];

    [SerializeField]
    GameObject _warlordPatch;

    [SerializeField]
    GameObject _intelligencePatch;

    [SerializeField]
    GameObject _resistancePatch;

    [SerializeField]
    GameObject _agilityPatch;

    [SerializeField]
    GameObject _precisionPatch;

    //------------------------------------------------------------------------------------

    //Patches Variables
    //------------------------------------------------------------------------------------
    [HideInInspector]
    public bool WarlordisOn;

    [HideInInspector]
    public bool PrecisionisOn;

    [HideInInspector]
    public bool AgilityisOn;

    [HideInInspector]
    public bool ResistanceisOn;

    [HideInInspector]
    public bool IntelligenceisOn;
    //------------------------------------------------------------------------------------

    #region Awake
    private void Awake()
    {
        _enablePlayer = false;
        _baseSpeed = playerSpeed;
        PV = GetComponent<PhotonView>();

        StartCoroutine(Timer());

        PlayerIsOn = true;

        //string nick = PlayerName;
        //PV.RPC("RPC_PlayerVariables", RpcTarget.All, nick);
        PistolAmmoStorage = 50;
        SniperAmmoStorage = 20;
        StartCoroutine(EnablePlayerGameplay());

    }

    IEnumerator EnablePlayerGameplay()
    {
        yield return new WaitForSeconds(5f);

        _enablePlayer = true;
    }
    #endregion


    #region Start
    void Start()
    {

        _animation = GetComponent<Animation>();
        // StaminaSlider.value = currentStamina;
        _helmetSlider.value = _helmetBattery;
        currentPrimaryStamina = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();

        //_rightHandIK.weight = 0;
        //_leftHandIK.weight = 0;

        SettingsStart();


    }
    #endregion


    #region Update

    void Update()
    {       


        if (Input.GetKeyDown(KeyCode.B))
        {
            Points += 5000;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

            RefilShield(25f);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //TakeDamage(40);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //Restore life
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SecundaryShieldIsActive = true;
            _secundaryShieldFillArea.SetActive(true);
        }

        PlayersManager();

        if (!PV.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            return;
        }

        if (!_enablePlayer)
        {
            return;
        }

        if (_playerIsDead)
        {
            return;
        }

        _playerCanvas.SetActive(true);
        // _moneyText.text = Points.ToString();


        LifeBehaviour();

        SettingsUdate();

        AmmoStats();

        AmmoTextBehaviour();

        if (IsBuying)
        {
            return;
        }

        PauseBehaviour();

        if (IsPaused)
        {
            return;
        }

        MouseLook();

        BaseMovement();

        RunBehaviour();

        WeaponThrow();

        HelmetLight();

        HeadBob();


        PV.RPC("VariablesUpdate", RpcTarget.All, PlayerName, PlayerPoints, PlayerKills, PlayerDeaths);
        PV.RPC("RPC_LifeUpdate", RpcTarget.All, PlayerLife);

    }

    #endregion


    #region LateUpdate

    void LateUpdate()
    {

        RunBehaviourLateUpdate();

        InteractionBehaviour();

        DeathBehaviour();

        //PV.RPC("LeaderBoardBehaviour", RpcTarget.All);
        LeaderBoardBehaviour();

        Buying();        

    }

    #endregion


    #region BaseMovement
    void BaseMovement()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        if (x != 0 || z != 0)
        {
            _isMoving = true;
        }
        else if (x == 0 && z == 0)
        {
            _isMoving = false;
        }
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * playerSpeed * Time.deltaTime * _playerSpeedMultiplier * (1 - (_playerWheight / 100)));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    #endregion


    #region RunBehaviour
    void RunBehaviour()
    {
        if (SecundaryStaminaIsActive)
        {
            _secundaryStaminaFillArea.SetActive(true);
        }
        else
        {
            _secundaryStaminaFillArea.SetActive(false);
        }
        if (_isRunning)
        {
            if (currentSecundaryStamina > 0.0018f && SecundaryStaminaIsActive)
            {
                currentSecundaryStamina -= 0.12f * _staminaMultiplier * Time.deltaTime;
                SecundaryStaminaSlider.value = currentSecundaryStamina;
            }
            else
            {
                currentPrimaryStamina -= 0.12f * _staminaMultiplier * Time.deltaTime;
                StaminaSlider.value = currentPrimaryStamina;

            }

        }
        else
        {
            if (!_isRunning && currentPrimaryStamina >= 0.0018)
            {
                if (currentPrimaryStamina >= 0.9982 && SecundaryStaminaIsActive)
                {
                    currentSecundaryStamina += 0.2f * Time.deltaTime;
                    SecundaryStaminaSlider.value = currentSecundaryStamina;
                }
                else
                {
                    currentPrimaryStamina += 0.2f * Time.deltaTime;
                    StaminaSlider.value = currentPrimaryStamina;
                }
            }
            else
            {
                StartCoroutine(RunningCancel());
                StartCoroutine(StaminaUp());
            }
        }
        currentSecundaryStamina = Mathf.Clamp(currentSecundaryStamina, 0f, 1f);
        currentPrimaryStamina = Mathf.Clamp(currentPrimaryStamina, 0f, 1f);



    }
    IEnumerator StaminaUp()
    {
        yield return new WaitForSeconds(3);

        if (currentPrimaryStamina < 0.0018)
        {
            currentPrimaryStamina += 0.2f * Time.deltaTime;
            StaminaSlider.value = currentPrimaryStamina;
        }
        yield break;

    }


    IEnumerator RunningCancel()
    {
        yield return new WaitForSeconds(0.3f);


        if (!_isRunning)
        {
            playerSpeed = _baseSpeed;
        }

    }

    #endregion


    #region RunBehaviourLateUpdate
    void RunBehaviourLateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentPrimaryStamina > 0.05)
        {
            //Debug.Log("inicio " + _doubleTap);

            playerSpeed = RunSpeed;
            _isRunning = true;

        }

        else if (Input.GetKey(KeyCode.LeftShift) && currentPrimaryStamina <= 0.002)
        {

            _isRunning = false;
            StartCoroutine(RunningCancel());

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isRunning = false;
            StartCoroutine(RunningCancel());
        }

    }
    #endregion


    #region HeadBob
    void HeadBob()
    {

        //Debug.Log("headed");
        if (controller.isGrounded)
        {
            //Debug.Log("is grounded");
            if (_isMoving)
            {
                //Debug.Log("is moving");
                if (_left)
                {
                    new Quaternion { eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z) };
                    _left = false;
                    _right = true;
                    /*
                    if (!_animation.isPlaying)
                    {
                        _xRotate += 1f;
                        _zRotate -= 1f;
                        //_animation.Play("Walk Left");
                        _left = false;
                        _right = true;
                        Debug.Log("played");
                    }*/
                }
                if (_right)
                {
                    new Quaternion { eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f) };
                    _right = false;
                    _left = true;
                    /*if (!_animation.isPlaying)
                    {
                        _zRotate += 1f;
                        _xRotate -= 1f;
                        //_animation.Play("Walk Right");
                        _right = false;
                        _left = true;
                    }*/
                }
            }
        }
    }
    #endregion    


    #region InteractionBehaviour

    void InteractionBehaviour()
    {

        RaycastHit cleitin;
        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out cleitin, _interactionRange))
        {

            switch (cleitin.collider.tag)
            {

                case "SingleWeaponToPick":

                    if (Input.GetKeyDown(KeyCode.E) && !RightHand && !LeftHand)
                    {
                        RightGun = cleitin.collider.gameObject;
                        RightGun.GetComponent<PhotonView>().RequestOwnership();
                        GunToRightHand(cleitin);
                    }

                    break;


                case "AkimboWeaponToPick":

                    if (Input.GetKeyDown(KeyCode.E) && !RightHand)
                    {
                        RightGun = cleitin.collider.gameObject;
                        RightGun.GetComponent<PhotonView>().RequestOwnership();
                        GunToRightHand(cleitin);
                    }

                    if (Input.GetKeyDown(KeyCode.Q) && !LeftHand)
                    {
                        LeftGun = cleitin.collider.gameObject;
                        LeftGun.GetComponent<PhotonView>().RequestOwnership();
                        GunToLeftHand(cleitin);
                    }

                    break;


                case "AmmoToPick":

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (SniperAmmoStorage == SniperAmmoMaxStorage && AssaultRifleAmmoStorage == AssaultRifleAmmoMaxStorage && SubmachineGunAmmoStorage == SubmachineGunAmmoMaxStorage && PistolAmmoStorage == PistolAmmoMaxStorage && ShotGunAmmoStorage == ShotGunAmmoMaxStorage)
                        {
                            return;
                        }

                        if (SniperAmmoStorage < SniperAmmoMaxStorage)
                        {

                            SniperAmmoStorage += Random.Range(1, 11);

                        }


                        if (AssaultRifleAmmoStorage < AssaultRifleAmmoMaxStorage)
                        {

                            AssaultRifleAmmoStorage += Random.Range(10, 61);

                        }


                        if (SubmachineGunAmmoStorage < SubmachineGunAmmoMaxStorage)
                        {

                            SubmachineGunAmmoStorage += Random.Range(20, 51);

                        }


                        if (PistolAmmoStorage < PistolAmmoMaxStorage)
                        {

                            PistolAmmoStorage += Random.Range(5, 21);

                        }


                        if (ShotGunAmmoStorage < ShotGunAmmoMaxStorage)
                        {

                            ShotGunAmmoStorage += Random.Range(10, 21);

                        }



                        cleitin.collider.gameObject.SetActive(false);


                    }

                    break;


                case "LootCrate":

                    if (Input.GetKeyDown(KeyCode.F))
                    {

                    }

                    break;


                case "LifePlate":

                    if (Input.GetKeyDown(KeyCode.F))
                    {

                    }

                    break;


                case "GamePoints":

                    if (Input.GetKeyDown(KeyCode.F))
                    {

                    }

                    break;

                case "PatchesBuyStation":

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        IsBuying = true;
                        _isBuyingPatches = true;
                    }

                    break;

                case "WeaponsBuyStation":

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        IsBuying = true;
                        _isBuyingWeapons = true;
                    }

                    break;

                case "EquipmentsBuyStation":

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        IsBuying = true;
                        _isBuyingEquipments = true;
                    }

                    break;
            }
        }

    }

    #endregion


    #region GunToRightHand
    void GunToRightHand(RaycastHit cleitin)
    {
        if (RightGun.CompareTag("SingleWeaponToPick"))
        {

            LeftHand = true;

        }




        _playerWheight += RightGun.GetComponent<WeaponController>().GunWheight;

        RightHand = true;

        RightGun.GetComponent<Rigidbody>().isKinematic = true;

        //if(Katana va para um spawn especifico)
        //if(pistola va para outro spawn especifico)
        //if(guitarra va para outro spawn especifico)
        //if(sniper va para
        RightGun.transform.position = _rightHandTrans.position;
        RightGun.transform.rotation = _rightHandTrans.rotation;
        RightGun.transform.parent = _playerCam.transform;

        RightGun.GetComponent<WeaponController>().enabled = true;

        RightGun.GetComponent<WeaponController>().IsRightGun = true;


        /*_rightHandIK.weight = 1;

        _initialRightHandRigTarget.transform.position = _rightGun.GetComponent<WeaponController>().RightHandRigTarget.transform.TransformPoint(Vector3.zero);
        _initialRightHandRigTarget.transform.rotation = _rightGun.GetComponent<WeaponController>().RightHandRigTarget.transform.localRotation;        
        _initialRightHandRigTarget.transform.parent = _rightGun.GetComponent<WeaponController>().RightHandRigTarget.transform;*/

        switch (RightGun.GetComponent<WeaponController>().GunName)
        {
            case "Glock":

                _rightGlockSil.SetActive(true);
                _pistolCrosshair.SetActive(true);

                break;

            case "357":

                _right357Sil.SetActive(true);
                _pistolCrosshair.SetActive(true);

                break;

            case "AWP":

                _aWPSil.SetActive(true);

                break;

            case "Famas":

                _famasSil.SetActive(true);
                _assaultRifleCrosshair.SetActive(true);

                break;

            case "AA12":

                _aA12Sil.SetActive(true);
                _shotgunCrosshair.SetActive(true);

                break;

            case "P90":

                _p90Sil.SetActive(true);
                _submachinegunCrosshair.SetActive(true);

                break;


            case "MAR-14":

                _mAR14Sil.SetActive(true);
                _assaultRifleCrosshair.SetActive(true);

                break;

            case "Vector":

                _vectorSil.SetActive(true);
                _submachinegunCrosshair.SetActive(true);

                break;

            case "Katana":

                _rightKatanaSil.SetActive(true);

                break;

            case "Les Paul":

                _lesPaulSil.SetActive(true);

                break;

            case "Sword":

                _rightSwordSil.SetActive(true);

                break;
        }
    }
    #endregion


    #region GunToLeftHand
    void GunToLeftHand(RaycastHit cleitin)
    {

        LeftHand = true;

        _playerWheight += LeftGun.GetComponent<WeaponController>().GunWheight;

        LeftGun.GetComponent<Rigidbody>().isKinematic = true;

        LeftGun.transform.position = _leftHandTrans.position;
        LeftGun.transform.rotation = _leftHandTrans.rotation;
        LeftGun.transform.parent = _playerCam.transform;

        LeftGun.GetComponent<WeaponController>().enabled = true;

        LeftGun.GetComponent<WeaponController>().IsLeftGun = true;


        /*_leftHandIK.weight = 1;

        _initialLeftHandRigTarget.transform.position = _leftGun.GetComponent<WeaponController>().LeftHandRigTarget.transform.TransformPoint(Vector3.zero);
        _initialLeftHandRigTarget.transform.rotation = _leftGun.GetComponent<WeaponController>().LeftHandRigTarget.transform.rotation;
        _initialLeftHandRigTarget.SetParent(_leftGun.transform);*/
        switch (LeftGun.GetComponent<WeaponController>().GunName)
        {
            case "Glock":

                _leftGlockSil.SetActive(true);
                _pistolCrosshair.SetActive(true);

                break;

            case "357":

                _left357Sil.SetActive(true);
                _pistolCrosshair.SetActive(true);

                break;

            case "Katana":

                _leftKatanaSil.SetActive(true);

                break;

            case "Sword":

                _leftSwordSil.SetActive(true);

                break;

        }
    }

    #endregion


    #region WeaponThrow
    void WeaponThrow()
    {
        if (Input.GetKeyDown(KeyCode.E) && RightHand && !RightGun.GetComponent<WeaponController>()._isReloading)
        {
            PrimaryHandThrow();
        }
        if (RightGun != null && RightGun.CompareTag("SingleWeaponToPick"))
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && LeftHand && !LeftGun.GetComponent<WeaponController>()._isReloading)
        {
            SecondaryHandThrow();
        }
    }

    void PrimaryHandThrow()
    {
        if (RightGun.CompareTag("SingleWeaponToPick"))
        {

            LeftHand = false;

        }

        if(LeftGun == null)
        {
            _pistolCrosshair.SetActive(false);
            _submachinegunCrosshair.SetActive(false);
            _assaultRifleCrosshair.SetActive(false);
            _shotgunCrosshair.SetActive(false);
        }


        _playerWheight -= RightGun.GetComponent<WeaponController>().GunWheight;

        RightHand = false;

        RightGun.GetComponent<WeaponController>().IsRightGun = false;

        RightGun.GetComponent<WeaponController>().enabled = false;

        RightGun.GetComponent<Rigidbody>().isKinematic = false;
        RightGun.GetComponent<Rigidbody>().AddForce(-RightGun.transform.right * (_throwForce * 1 / RightGun.GetComponent<WeaponController>().GunWheight));
        RightGun.transform.parent = null;

        RightGun = null;

        //_rightHandIK.weight = 0;

        //_initialRightHandRigTarget.transform.parent = this.transform;        

        _rightGlockSil.SetActive(false);

        _right357Sil.SetActive(false);

        _aWPSil.SetActive(false);

        _famasSil.SetActive(false);

        _aA12Sil.SetActive(false);

        _p90Sil.SetActive(false);

        _mAR14Sil.SetActive(false);

        _vectorSil.SetActive(false);

        _rightKatanaSil.SetActive(false);

        _lesPaulSil.SetActive(false);

        _rightSwordSil.SetActive(false);


    }

    void SecondaryHandThrow()
    {


        _playerWheight -= LeftGun.GetComponent<WeaponController>().GunWheight;
        LeftHand = false;
        LeftGun.GetComponent<WeaponController>().IsLeftGun = false;

        LeftGun.GetComponent<WeaponController>().enabled = false;

        LeftGun.GetComponent<Rigidbody>().isKinematic = false;
        LeftGun.GetComponent<Rigidbody>().AddForce(-LeftGun.transform.right * (_throwForce * 1 / LeftGun.GetComponent<WeaponController>().GunWheight));
        LeftGun.transform.parent = null;

        LeftGun = null;

        //_leftHandIK.weight = 0;

        //_initialLeftHandRigTarget.transform.parent = this.transform;


        _leftGlockSil.SetActive(false);

        _left357Sil.SetActive(false);

        _leftKatanaSil.SetActive(false);

        _leftSwordSil.SetActive(false);

        if (RightGun == null)
        {
            _pistolCrosshair.SetActive(false);
            _submachinegunCrosshair.SetActive(false);
            _assaultRifleCrosshair.SetActive(false);
            _shotgunCrosshair.SetActive(false);
        }

    }

    #endregion


    #region HelmetLight         
    void HelmetLight()
    {
        _helmetSlider.value = _helmetBattery;

        if (_helmetBattery < (1 / 100))
        {
            _helmetLight.SetActive(false);
            _helmetIsOn = false;
        }

        if (!_helmetIsOn)
        {
            PV.RPC("HelmetIsOn", RpcTarget.All);
        }
        else if (_helmetIsOn)
        {
            PV.RPC("HelmetIsOff", RpcTarget.All);
        }

        _helmetBattery = Mathf.Clamp(_helmetBattery, -0.001f, 1f);

    }

    [PunRPC]
    void HelmetIsOn()
    {
        _helmetBattery += (Time.deltaTime * _helmetBatteryDecay) / 100;

        if (Input.GetKeyDown(KeyCode.H))
        {
            _helmetLight.SetActive(true);
            _helmetIsOn = true;
        }
    }

    [PunRPC]
    void HelmetIsOff()
    {
        _helmetBattery -= (Time.deltaTime * _helmetBatteryDecay) / 100;

        if (Input.GetKeyDown(KeyCode.H) && _helmetBattery > 0)
        {
            _helmetLight.SetActive(false);
            _helmetIsOn = false;
        }
    }
    #endregion


    #region AmmoStats

    void AmmoStats()
    {
        PistolAmmoStorage = Mathf.Clamp(PistolAmmoStorage, 0, PistolAmmoMaxStorage);
        SubmachineGunAmmoStorage = Mathf.Clamp(SubmachineGunAmmoStorage, 0, SubmachineGunAmmoMaxStorage);
        AssaultRifleAmmoStorage = Mathf.Clamp(AssaultRifleAmmoStorage, 0, AssaultRifleAmmoMaxStorage);
        SniperAmmoStorage = Mathf.Clamp(SniperAmmoStorage, 0, SniperAmmoMaxStorage);
        ShotGunAmmoStorage = Mathf.Clamp(ShotGunAmmoStorage, 0, ShotGunAmmoMaxStorage);
    }

    #endregion


    #region AmmoTextBehaviour

    void AmmoTextBehaviour()
    {
        _playerCanvas.SetActive(true);

        string AssaultRifleAmmoText = AssaultRifleAmmoStorage.ToString();
        string HelmetBatteryText = (_helmetBattery * 100).ToString("F0") + "%";
        string PistolAmmoText = PistolAmmoStorage.ToString();
        string ShotGunAmmoText = ShotGunAmmoStorage.ToString();
        string SniperAmmoText = SniperAmmoStorage.ToString();
        string SubmachineGunAmmoText = SubmachineGunAmmoStorage.ToString();



        _playerNickOnScreen.text = PlayerName;
        _moneyText.text = Points.ToString();
        _helmetText.text = HelmetBatteryText;


        if (Input.GetKey(KeyCode.Tab))
        {
            _ammoVariations.SetActive(true);
            //Debug.Log("TAINO");
        }
        else
        {
            _ammoVariations.SetActive(false);
        }
        //HelmetBatteryText
        _aRAmmoText.text = AssaultRifleAmmoText;
        _sMGAmmoText.text = SubmachineGunAmmoText;
        _sniperAmmoText.text = SniperAmmoText;
        _shotgunAmmoText.text = ShotGunAmmoText;
        _pistolAmmoText.text = PistolAmmoText;
        /*Debug.Log("right " + _rightGun);
        Debug.Log("left " + _leftGun);*/
        if (RightGun == null && LeftGun == null)
        {
            _gunAmmoText.GetComponent<Text>().enabled = false;
            _noAmmoText.enabled = false;
            _maxAmmoOnScreen.text = null;
        }

        else if (RightGun != null && LeftGun == null)
        {
            if (RightGun.GetComponent<WeaponController>().CurrentAmmo == 0)
            {
                _noAmmoText.enabled = true;
            }
            else
            {
                _noAmmoText.enabled = false;
            }
            _gunAmmoText.GetComponent<Text>().enabled = true;
            _gunAmmoText.text = RightGun.GetComponentInChildren<WeaponController>().CurrentAmmo.ToString();


        }

        else if (LeftGun != null && RightGun == null)
        {
            if (LeftGun.GetComponent<WeaponController>().CurrentAmmo == 0)
            {
                _noAmmoText.enabled = true;
            }
            else
            {
                _noAmmoText.enabled = false;
            }
            _gunAmmoText.GetComponent<Text>().enabled = true;
            _gunAmmoText.text = LeftGun.GetComponentInChildren<WeaponController>().CurrentAmmo.ToString();
        }

        else if (RightGun != null && LeftGun != null)
        {
            if (RightGun.GetComponent<WeaponController>().CurrentAmmo == 0 && LeftGun.GetComponent<WeaponController>().CurrentAmmo == 0)
            {
                _noAmmoText.enabled = true;
            }
            else
            {
                _noAmmoText.enabled = false;
            }
            _gunAmmoText.GetComponent<Text>().enabled = true;
            _gunAmmoText.text = LeftGun.GetComponentInChildren<WeaponController>().CurrentAmmo.ToString() + " / " + RightGun.GetComponentInChildren<WeaponController>().CurrentAmmo.ToString(); ;
        }

        if (RightGun == null)
        {
            return;
        }
        switch (RightGun.GetComponent<WeaponController>().GunName)
        {
            case "Glock":

                _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();

                break;

            case "357":

                _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();

                break;

            case "AWP":

                _maxAmmoOnScreen.text = SniperAmmoStorage.ToString();

                break;

            case "Famas":

                _maxAmmoOnScreen.text = AssaultRifleAmmoStorage.ToString();

                break;

            case "AA12":

                _maxAmmoOnScreen.text = ShotGunAmmoStorage.ToString();

                break;

            case "P90":

                _maxAmmoOnScreen.text = SubmachineGunAmmoStorage.ToString();

                break;


            case "MAR-14":

                _maxAmmoOnScreen.text = AssaultRifleAmmoStorage.ToString();

                break;

            case "Vector":

                _maxAmmoOnScreen.text = SubmachineGunAmmoStorage.ToString();

                break;

            case "Katana":

                if (LeftGun != null)
                {
                    if (LeftGun.GetComponent<WeaponController>().GunName == "Glock" || LeftGun.GetComponent<WeaponController>().GunName == "357")
                    {
                        _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();
                    }
                }
                else
                {

                    _maxAmmoOnScreen.text = "∞";
                }

                break;

            case "Les Paul":

                _maxAmmoOnScreen.text = "∞";

                break;

            case "Sword":

                if (LeftGun != null)
                {
                    if (LeftGun.GetComponent<WeaponController>().GunName == "Glock" || LeftGun.GetComponent<WeaponController>().GunName == "357")
                    {
                        _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();
                    }
                }
                else
                {

                    _maxAmmoOnScreen.text = "∞";
                }

                break;
        }

        if (LeftGun == null)
        {
            return;
        }
        switch (LeftGun.GetComponent<WeaponController>().GunName)
        {
            case "Glock":

                _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();

                break;

            case "357":

                _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();

                break;


            case "Katana":

                if (RightGun != null)
                {
                    if (RightGun.GetComponent<WeaponController>().GunName == "Glock" || RightGun.GetComponent<WeaponController>().GunName == "357")
                    {
                        _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();
                    }
                }
                else
                {

                    _maxAmmoOnScreen.text = "∞";
                }

                break;

            case "Les Paul":

                _maxAmmoOnScreen.text = "∞";

                break;

            case "Sword":

                if (RightGun != null)
                {
                    if (RightGun.GetComponent<WeaponController>().GunName == "Glock" || RightGun.GetComponent<WeaponController>().GunName == "357")
                    {
                        _maxAmmoOnScreen.text = PistolAmmoStorage.ToString();
                    }
                }
                else
                {

                    _maxAmmoOnScreen.text = "∞";
                }

                break;
        }

    }

    #endregion


    #region MouseLook

    void MouseLook()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime + _horizontalRecoil;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime + _verticalRecoil;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _playerCam.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        /*_horizontalRecoil = Mathf.Clamp(0f, 0f, 2.5f);
        _verticalRecoil = Mathf.Clamp(0f, 0f, 2.5f);
        _verticalRecoil -= Time.deltaTime;
        _verticalRecoil = Mathf.Clamp(_verticalRecoil, 0f, 10f);*/
    }

    #endregion


    #region AddRecoil

    public void AddRecoil(float _v, float _h)
    {
        _verticalRecoil = _v;
        _horizontalRecoil = _h;
    }

    #endregion


    #region Timer

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        _minuteInt++;

        if (_minuteInt > 59)
        {
            _hourInt++;
            _minuteInt = 0;
        }
        if (_hourInt > 23)
        {
            _dayInt++;
            _hourInt = 0;
        }

        _timerText.text = _hourInt.ToString("D2") + ":" + _minuteInt.ToString("D2");
        _dayText.text = "DAY " + _dayInt.ToString();
        StartCoroutine(Timer());

    }

    #endregion


    #region LifeBehaviour    

    [PunRPC]
    void PlayerCharacterRagdoll()
    {
        _characterMain = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Character Mesh"), _charSpawn.transform.position, _charSpawn.transform.rotation);
        _characterMain.transform.position = _charSpawn.transform.position;
        _characterMain.transform.parent = this.transform;
        _charIsOn = true;
    }
    void LifeBehaviour()
    {

        if (!_charIsOn)
        {
            PV.RPC("PlayerCharacterRagdoll", RpcTarget.All);
        }
        _lifeSlider.value = PlayerLife;
        PlayerLife += Time.deltaTime */* * _lifeRestoreMultiplier */0.1f;
        PlayerLife = Mathf.Clamp(PlayerLife, 0f, 1f);
        //PlayerPrimaryShield = Mathf.Clamp(PlayerLife, 0f, 1f);
        //PlayerSecundaryShield = Mathf.Clamp(PlayerLife, 0f, 1f);
        _primaryShieldSlider.value = PlayerPrimaryShield;
        _secundaryShieldSlider.value = PlayerSecundaryShield;


    }

    #endregion


    #region DeathBehaviour

    [PunRPC]
    public void PlayerKillsCount(int kills)
    {

    }
    public void TakeDamage(float DamageTaken)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, DamageTaken);
        /*if (!PV.IsMine)
        {
            return;
        }
        DamageTaken /= 100;
        float _finalDamage = DamageTaken * (1 + (1 - _damageMultiplier));
        Debug.Log(_finalDamage);
        if (SecundaryShieldIsActive)
        {
            PlayerSecundaryShield -= _finalDamage * 2;
            if (PlayerSecundaryShield < 0)
            {
                PlayerPrimaryShield += PlayerSecundaryShield;
                PlayerSecundaryShield = 0;
            }
        }
        else if (!SecundaryShieldIsActive)
        {
            PlayerPrimaryShield -= _finalDamage * 2;
        }

        //PlayerShield - DamageTaken

        if (PlayerPrimaryShield < 0)
        {
            PlayerLife += PlayerPrimaryShield / 2;
            PlayerPrimaryShield = 0;
        }*/
    }

    [PunRPC]
    void RPC_TakeDamage(float DamageTaken)
    {
        if (!PV.IsMine)
        {
            return;
        }
        DamageTaken /= 100;
        float _finalDamage = DamageTaken * (1 + (1 - _damageMultiplier));
        Debug.Log(_finalDamage);
        if (SecundaryShieldIsActive)
        {
            PlayerSecundaryShield -= _finalDamage * 2;
            if (PlayerSecundaryShield < 0)
            {
                PlayerPrimaryShield += PlayerSecundaryShield;
                PlayerSecundaryShield = 0;
            }
        }
        else if (!SecundaryShieldIsActive)
        {
            PlayerPrimaryShield -= _finalDamage * 2;
        }

        //PlayerShield - DamageTaken

        if (PlayerPrimaryShield < 0)
        {
            PlayerLife += PlayerPrimaryShield / 2;
            PlayerPrimaryShield = 0;

        }
    }

    public void RefilShield(float ShieldRestore)
    {
        Debug.Log("foi eim");
        PV.RPC("RPC_RefilShield", RpcTarget.All, 25f);
    }
    [PunRPC]
    void RPC_RefilShield(float ShieldRestore)
    {
        if (!PV.IsMine)
        {
            return;
        }
        Debug.Log("aaaaaaaaaaaaa");
        ShieldRestore /= 100;

        if (PlayerPrimaryShield >= 1 && SecundaryShieldIsActive)
        {
            PlayerSecundaryShield += ShieldRestore;
        }
        else
        {

            PlayerPrimaryShield += ShieldRestore;
            if(PlayerPrimaryShield >= 1)
            {
                PlayerSecundaryShield += PlayerPrimaryShield - 1;
                PlayerPrimaryShield = 1;
            }
        }


    }
    public void EnemyKillsUpdate(string EnemyPlayerName)
    {
        PlayerThatShotMe = EnemyPlayerName;
        Debug.Log(PlayerThatShotMe);
        Debug.Log(PlayerLife);
        /*if (PlayerLife <= 0)
        {
            Debug.Log(EnemyPlayerName);

            Debug.Log("vagabundoooo");
            PV.RPC("RPC_EnemyKillsUpdate", RpcTarget.All, PlayerThatShotMe);
        }*/

    }

    [PunRPC]
    void RPC_EnemyKillsUpdate(string PlayerThatShotMe)
    {
                
            Debug.Log("coe manoooo");
        GameObject.Find(PlayerThatShotMe).GetComponent<PlayerController>().PlayerKills += 1;
        
    }
    void DeathBehaviour()
    {
        if (!_playerIsDead && PlayerLife <= 0)
        {
            PlayerDeaths++;
            _playerIsDead = true;
            _playerCanvas.SetActive(false);
            _deathCanvas.SetActive(true);
            if (RightGun != null)
            {
                PrimaryHandThrow();
            }
            if (LeftGun != null)
            {
                SecondaryHandThrow();
            }
            PV.RPC("DeathBehaviourFinal", RpcTarget.All);
        }
    }
    [PunRPC]
    void DeathBehaviourFinal()
    {
        StartCoroutine(Resurrection());
        GameObject.Find(PlayerThatShotMe).GetComponent<PlayerController>().PlayerKills += 1;
        GameObject.Find(PlayerThatShotMe).GetComponent<PlayerController>().PointsBehaviour(2000);
        GetComponent<CharacterController>().enabled = false;
        GetComponentInChildren<RagdollBehaviour>().RagdollToActivate = true;
        GetComponent<PlayerController>().enabled = false;


        /*respawnWave = waveInt + 1;
        _playerIsDead = true;
         
         if(waveInt = respawnWave)
        {
        //Player Renasce
        _playerIsDead = false;
        }
        
        if(_playerIsDead)
        {
         //Desabilitar este script e dropar tudo mas manter a camera funcionando.
        
        }

        
        */
    }

    IEnumerator Resurrection()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.Destroy(_characterMain);
        GetComponent<CharacterController>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        _charIsOn = false;
        int randomSpawn = Random.Range(0, 6);
        PlayerLife = 1f;
        PlayerPrimaryShield = 1f;
        Points /= 3;
        _playerIsDead = false;
        this.transform.position = _spawnPositions[randomSpawn];
        if (!PV.IsMine)
        {
            yield break; ;
        }
        _deathCanvas.SetActive(false);
        _playerCanvas.SetActive(true);

    }

    #endregion


    #region PointsBehaviour


    public void PointsBehaviour(int PointsToGet)
    {
        PV.RPC("RPC_Points", RpcTarget.All, PointsToGet);
        //Points += PointsToGet;


        GameObject _pointVisualFeedBack = Instantiate(_pointsOnScreenGameObject, _pointsOnScreenSpawner.position, _pointsOnScreenSpawner.rotation);
        _pointVisualFeedBack.transform.parent = _pointsOnScreenSpawner.transform.parent;
        _pointVisualFeedBack.GetComponent<Text>().text = "+ " + PointsToGet.ToString();


        float yPoints = _pointVisualFeedBack.transform.position.y;
        float animationTime = 20f;
        bool canDestroyAnimatedPoints = false;
        Color _pointsColor = _pointVisualFeedBack.GetComponent<Text>().color;

        if (canDestroyAnimatedPoints)
        {
            Destroy(_pointVisualFeedBack);
        }

        StartCoroutine(PointsAnimation(yPoints, animationTime, canDestroyAnimatedPoints, _pointVisualFeedBack, _pointsColor));

    }

    [PunRPC]
    void RPC_Points(int PointsToGet)
    {
        Points += PointsToGet;
    }

    IEnumerator PointsAnimation(float yPoints, float animationTime, bool canDestroyAnimatedPoints, GameObject _pointVisualFeedBack, Color _pointsColor)
    {
        yield return new WaitForSeconds(0.01f);
        if (animationTime >= 0)
        {
            _pointsColor.a -= Time.deltaTime * 6;
            _pointVisualFeedBack.transform.position = new Vector3(_pointVisualFeedBack.transform.position.x, yPoints, _pointVisualFeedBack.transform.position.z);
            animationTime--;
            yPoints -= Time.deltaTime * 200; ;
            StartCoroutine(PointsAnimation(yPoints, animationTime, canDestroyAnimatedPoints, _pointVisualFeedBack, _pointsColor));
            yield break;
        }

        Destroy(_pointVisualFeedBack);




    }

    #endregion


    #region PauseBehaviour   


    // Update is called once per frame
    void PauseBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                PauseGame();
            }
            else if (IsPaused)
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (!MainManager.IsMultiplayer)
        {
            Time.timeScale = 0F;
        }
        IsPaused = true;
        _pauseCanvas.SetActive(true);
        _inGameMenuCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1F;
        IsPaused = false;
        _pauseCanvas.SetActive(false);
        _inGameMenuCanvas.SetActive(false);
        _optionsCanvas.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Scene");

        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();

    }

    public void ExitPause()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();

        Application.Quit();
    }

    public void OptionsPause()
    {
        _optionsCanvas.SetActive(true);
        _inGameMenuCanvas.SetActive(false);

    }

    public void OptionsReturn()
    {
        _optionsCanvas.SetActive(false);
        _inGameMenuCanvas.SetActive(true);
    }

    #endregion


    #region BuyingBehaviour

    void Buying()
    {

        //------------------------------------------------------------------------------


        if (IsBuying && _isBuyingEquipments)
        {
            BuyStationCanvas.SetActive(true);
            EquipmentsToBuyCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (IsBuying && _isBuyingPatches)
        {
            BuyStationCanvas.SetActive(true);
            PatchesToBuyCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (IsBuying && _isBuyingWeapons)
        {
            BuyStationCanvas.SetActive(true);
            WeaponsToBuyCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        //------------------------------------------------------------------------------


        if (IsBuying && Input.GetKeyDown(KeyCode.Escape))
        {
            BuyStationCanvas.SetActive(false);

            EquipmentsToBuyCanvas.SetActive(false);
            PatchesToBuyCanvas.SetActive(false);
            WeaponsToBuyCanvas.SetActive(false);

            IsBuying = false;
            _isBuyingEquipments = false;
            _isBuyingPatches = false;
            _isBuyingWeapons = false;
            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }


        //------------------------------------------------------------------------------


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas++;
            if (_activePatchCanvas > 4)
            {
                _activePatchCanvas = 0;
            }
            _PatchesLayers[_activePatchCanvas].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas--;
            if (_activePatchCanvas < 0)
            {
                _activePatchCanvas = 4;
            }
            _PatchesLayers[_activePatchCanvas].SetActive(true);
        }

        //------------------------------------------------------------------------------
    }


    //------------------------------------------------------------------------------


    public void ExitBuy()
    {
        BuyStationCanvas.SetActive(false);

        EquipmentsToBuyCanvas.SetActive(false);
        PatchesToBuyCanvas.SetActive(false);
        WeaponsToBuyCanvas.SetActive(false);

        IsBuying = false;
        _isBuyingEquipments = false;
        _isBuyingPatches = false;
        _isBuyingWeapons = false;
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    //------------------------------------------------------------------------------


    public void ChooseWarlord()
    {
        for (_activePatchCanvas = 0; _activePatchCanvas < _PatchesLayers.Length;)
        {
            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas++;
        }
        _activePatchCanvas = 0;
        _PatchesLayers[_activePatchCanvas].SetActive(true);

        
    }

    public void ChoosePrecision()
    {
        for (_activePatchCanvas = 0; _activePatchCanvas < _PatchesLayers.Length;)
        {
            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas++;
        }
        _activePatchCanvas = 1;
        _PatchesLayers[_activePatchCanvas].SetActive(true);

        
    }

    public void ChooseAgility()
    {
        for (_activePatchCanvas = 0; _activePatchCanvas < _PatchesLayers.Length;)
        {
            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas++;
        }
        _activePatchCanvas = 2;
        _PatchesLayers[_activePatchCanvas].SetActive(true);

        
    }

    public void ChooseResistance()
    {
        for (_activePatchCanvas = 0; _activePatchCanvas < _PatchesLayers.Length;)
        {
            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas++;
        }
        _activePatchCanvas = 3;
        _PatchesLayers[_activePatchCanvas].SetActive(true);

        
    }

    public void ChooseIntelligence()
    {
        for (_activePatchCanvas = 0; _activePatchCanvas < _PatchesLayers.Length;)
        {
            _PatchesLayers[_activePatchCanvas].SetActive(false);
            _activePatchCanvas++;
        }
        _activePatchCanvas = 4;
        _PatchesLayers[_activePatchCanvas].SetActive(true);

        
    }

    //---------------------------------------------------------------------------------
    public void EnableWarlord()
    {
        _warlordPatch.SetActive(true);
    }

    public void EnablePrecision()
    {
        _precisionPatch.SetActive(true);
    }

    public void EnableAgility()
    {
        _agilityPatch.SetActive(true);

        SecundaryStaminaIsActive = true;
        _secundaryStaminaFillArea.SetActive(true);

    }

    public void EnableResistance()
    {
        _resistancePatch.SetActive(true);
        
        SecundaryShieldIsActive = true;
        _secundaryShieldFillArea.SetActive(true);
    }

    public void EnableIntelligence()
    {
        _intelligencePatch.SetActive(true);
    }





    void BuyWeapons()
    {

    }

    void BuyEquipment()
    {

    }
    #endregion


    #region SettingsBehaviour

    void SettingsStart()
    {
        _resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    void SettingsUdate()
    {
        mouseSensitivity = MouseSensSlider.value;
        //Debug.Log(MouseSensSlider.value);
    }

    public void SetMusicVolume(float MusicVolume)
    {
        MusicMixer.SetFloat("MusicVolume", MusicVolume);
    }

    public void SetSFXVolume(float SFXVolume)
    {
        SFXMixer.SetFloat("SFXVolume", SFXVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion


    #region LeaderBoardBehaviour

    //[PunRPC]
    void LeaderBoardBehaviour()
    {

        PlayerPoints = Points;

        if (_playersInGame[0] != null && _playersInGame[0].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player1InGame = true;
        }
        if (_player1InGame)
        {
            _player1InGame = true;
            _playersNames[0].enabled = true;
            _playersNames[0].text = _playersInGame[0].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[0].enabled = true;
            _playersKills[0].text = _playersInGame[0].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[0].enabled = true;
            _playersDeaths[0].text = _playersInGame[0].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[0].enabled = true;
            _playersPoints[0].text = _playersInGame[0].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player1InGame)
        {
            _playersPoints[0].enabled = false;
            _playersDeaths[0].enabled = false;
            _playersKills[0].enabled = false;
            _playersNames[0].enabled = false;
        }



        if (_playersInGame.Length == 2 && _playersInGame[1] != null && _playersInGame[1].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player2InGame = true;
        }
        if (_player2InGame)
        {
            _playersNames[1].enabled = true;
            _playersNames[1].text = _playersInGame[1].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[1].enabled = true;
            _playersKills[1].text = _playersInGame[1].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[1].enabled = true;
            _playersDeaths[1].text = _playersInGame[1].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[1].enabled = true;
            _playersPoints[1].text = _playersInGame[1].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player2InGame)
        {
            _playersPoints[1].enabled = false;
            _playersDeaths[1].enabled = false;
            _playersKills[1].enabled = false;
            _playersNames[1].enabled = false;
        }



        if (_playersInGame.Length == 3 && _playersInGame[2] != null && _playersInGame[2].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player3InGame = true;
        }
        if (_player3InGame)
        {
            _playersNames[2].enabled = true;
            _playersNames[2].text = _playersInGame[2].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[2].enabled = true;
            _playersKills[2].text = _playersInGame[2].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[2].enabled = true;
            _playersDeaths[2].text = _playersInGame[2].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[2].enabled = true;
            _playersPoints[2].text = _playersInGame[2].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player3InGame)
        {
            _playersPoints[2].enabled = false;
            _playersDeaths[2].enabled = false;
            _playersKills[2].enabled = false;
            _playersNames[2].enabled = false;
        }



        if (_playersInGame.Length == 4 && _playersInGame[3] != null && _playersInGame[3].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player4InGame = true;
        }
        if (_player4InGame)
        {
            _playersNames[3].enabled = true;
            _playersNames[3].text = _playersInGame[3].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[3].enabled = true;
            _playersKills[3].text = _playersInGame[3].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[3].enabled = true;
            _playersDeaths[3].text = _playersInGame[3].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[3].enabled = true;
            _playersPoints[3].text = _playersInGame[3].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player4InGame)
        {
            _playersPoints[3].enabled = false;
            _playersDeaths[3].enabled = false;
            _playersKills[3].enabled = false;
            _playersNames[3].enabled = false;
        }



        if (_playersInGame.Length == 5 && _playersInGame[4] != null && _playersInGame[4].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player5InGame = true;
        }
        if (_player5InGame)
        {
            _playersNames[4].enabled = true;
            _playersNames[4].text = _playersInGame[4].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[4].enabled = true;
            _playersKills[4].text = _playersInGame[4].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[4].enabled = true;
            _playersDeaths[4].text = _playersInGame[4].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[4].enabled = true;
            _playersPoints[4].text = _playersInGame[4].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player5InGame)
        {
            _playersPoints[4].enabled = false;
            _playersDeaths[4].enabled = false;
            _playersKills[4].enabled = false;
            _playersNames[4].enabled = false;
        }



        if (_playersInGame.Length == 6 && _playersInGame[5] != null && _playersInGame[5].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player6InGame = true;
        }
        if (_player6InGame)
        {
            _playersNames[5].enabled = true;
            _playersNames[5].text = _playersInGame[5].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[5].enabled = true;
            _playersKills[5].text = _playersInGame[5].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[5].enabled = true;
            _playersDeaths[5].text = _playersInGame[5].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[5].enabled = true;
            _playersPoints[5].text = _playersInGame[5].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player6InGame)
        {
            _playersPoints[5].enabled = false;
            _playersDeaths[5].enabled = false;
            _playersKills[5].enabled = false;
            _playersNames[5].enabled = false;
        }



        if (_playersInGame.Length == 7 && _playersInGame[6] != null && _playersInGame[6].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player7InGame = true;
        }
        if (_player7InGame)
        {
            _playersNames[6].enabled = true;
            _playersNames[6].text = _playersInGame[6].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[6].enabled = true;
            _playersKills[6].text = _playersInGame[6].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[6].enabled = true;
            _playersDeaths[6].text = _playersInGame[6].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[6].enabled = true;
            _playersPoints[6].text = _playersInGame[6].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player7InGame)
        {
            _playersPoints[6].enabled = false;
            _playersDeaths[6].enabled = false;
            _playersKills[6].enabled = false;
            _playersNames[6].enabled = false;
        }



        if (_playersInGame.Length == 8 && _playersInGame[7] != null && _playersInGame[7].GetComponent<PlayerController>().PlayerIsOn)
        {
            _player8InGame = true;
        }
        if (_player8InGame)
        {
            _playersNames[7].enabled = true;
            _playersNames[7].text = _playersInGame[7].GetComponent<PlayerController>().RCP_PlayerName.ToString();
            _playersKills[7].enabled = true;
            _playersKills[7].text = _playersInGame[7].GetComponent<PlayerController>().RCP_PlayerKills.ToString();
            _playersDeaths[7].enabled = true;
            _playersDeaths[7].text = _playersInGame[7].GetComponent<PlayerController>().RCP_PlayerDeaths.ToString();
            _playersPoints[7].enabled = true;
            _playersPoints[7].text = _playersInGame[7].GetComponent<PlayerController>().RCP_PlayerPoints.ToString();
        }
        if (!_player8InGame)
        {
            _playersPoints[7].enabled = false;
            _playersDeaths[7].enabled = false;
            _playersKills[7].enabled = false;
            _playersNames[7].enabled = false;
        }
    }

    #endregion


    #region PlayersManager

    void PlayersManager()
    {
        _playersInGame = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(_playersInGame.Length);
    }

    [PunRPC]
    void RPC_PlayerVariables(string nick)
    {
        PlayerName = nick;
    }

    [PunRPC]
    void VariablesUpdate(string PlayerName, float PlayerPoints, int PlayerKills, int PlayerDeaths)
    {
        RCP_PlayerName = PlayerName;
        RCP_PlayerPoints = PlayerPoints;
        RCP_PlayerKills = PlayerKills;
        RCP_PlayerDeaths = PlayerDeaths;
        gameObject.name = PlayerName;
        
    }

    [PunRPC]
    void RPC_LifeUpdate(float PlayerLife)
    {
        RPC_PlayerLife = PlayerLife;
    }
    #endregion



}
