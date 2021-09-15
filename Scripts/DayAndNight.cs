using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayAndNight : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    //[SerializeField] private Light Moon;
    [SerializeField] private DANPreset Preset;
    //Variables
    [SerializeField, Range(0, 1440)] private float TimeOfDay;


    private void Update()
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




    //Try to find a directional light to use if we haven't set one
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
}

