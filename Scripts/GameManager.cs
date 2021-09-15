using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager GameManagerInstance;



    //Day And Night Variables
    //------------------------------------------------------------------------------------


    public Light DirectionalLight;


    public DANPreset Preset;


    [Range(0, 1440)] public float TimeOfDay = 900;

    
    //------------------------------------------------------------------------------------


    //Rain Variables
    //------------------------------------------------------------------------------------
    float _rainXPosition;
    float _rainYPosition;

    [HideInInspector]
    public static int RainChance;

    [SerializeField]
    GameObject _rainParticle;

    public static bool _isRaining = false;

    public GameObject[] _rainDropArray;
    int i = 0;
    //------------------------------------------------------------------------------------

    //Players Manager Variables
    //------------------------------------------------------------------------------------

    
    //------------------------------------------------------------------------------------


    #region Awake

    private void Awake()
    {
        GameManagerInstance = this;
    }

    #endregion


    #region Start

    void Start()
    {
        StartCoroutine(Raining());
    }

    #endregion


    #region Update
    // Update is called once per frame
    void Update()
    {
        DayAndNightBehaviour();
       // Debug.Log(NumbOfPlayers);       
    }
    #endregion


    #region DayAndNightBehaviour
    void DayAndNightBehaviour()
    {
        if (Preset == null)
            return;
        //Adjust to 1440 = quase 30 minutos
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 1440; //Clamp between 0 - 24
            UpdateLightning(TimeOfDay / 1440f);



        }
        else
        {


            UpdateLightning(TimeOfDay / 1440f);
        }
    }

    private void UpdateLightning(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.fogColor.Evaluate(timePercent);
        //RenderSettings.ambientLight = Preset.SunIntensity.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
        //if (Moon != null)
        {

            // Moon.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) + 90f, 170f, 0));
        }
    }
    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        //Search for lightning tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (Directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
    #endregion


    #region RainBehaviour

    IEnumerator RainDropsStart()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        if (_isRaining)
        {

            if (i < _rainDropArray.Length)
            {
                /*float yDrop = _rainDropArray[i].transform.position.y;
                yDrop -= Time.deltaTime;
                _rainDropArray[i].transform.position = new Vector3(_rainDropArray[i].transform.position.x, yDrop, _rainDropArray[i].transform.position.z);*/
                _rainDropArray[i].SetActive(true);
                _rainDropArray[i].GetComponent<Animator>().speed = Random.Range(0.1f, 1f);

                //StartCoroutine(RainDropsDisable());
                i += 1;
                
                StartCoroutine(RainDropsStart());
                yield break;

            }
            else if (i >= _rainDropArray.Length)
            {
                i = 0;
            }

        }
    }
    IEnumerator RainDropsDisable()
    {
        yield return new WaitForSeconds(1f);
        for (int x = 0; x < _rainDropArray.Length;)
        {
            _rainDropArray[x].SetActive(false);

            x++;            

        }
    }

    IEnumerator Raining()
    {
        yield return new WaitForSeconds(200f);
        RainChance = Random.Range(1, 101);

        GetComponent<PhotonView>().RPC("RPC_Rain", RpcTarget.All);

        StartCoroutine(Raining());
    }

    [PunRPC]
    void RPC_Rain()
    {
        if (RainChance <= 20)
        {
            _isRaining = true;
            _rainParticle.SetActive(true);
            StartCoroutine(RainDropsStart());
        }
        else
        {
            _isRaining = false;
            _rainParticle.SetActive(false);
            StartCoroutine(RainDropsDisable());
        }
    }


    #endregion
}
