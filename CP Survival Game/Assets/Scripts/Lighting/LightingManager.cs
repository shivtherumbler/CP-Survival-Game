using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset preset;

    [SerializeField, Range(0, 360)] private float TimeOfDay;
    public GameObject Sun;
    public DigitalRuby.RainMaker.RainScript RainScript;

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RainScript.RainIntensity = Random.Range(0,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 360f;
            //UpdateLighting(TimeOfDay / 1020);
            Sun.transform.rotation = Quaternion.Euler(TimeOfDay, 0.0f, 0.0f);
        }
        else
        {
            //UpdateLighting(TimeOfDay / 1020);
            Sun.transform.rotation = Quaternion.Euler(TimeOfDay, 0.0f, 0.0f);
        }
    }
}
