using UnityEngine;

public class LavaLightFlicker : MonoBehaviour
{
    private Light lavaLight;
    private float baseIntensity;

    void Start()
    {
        lavaLight = GetComponent<Light>();
        baseIntensity = lavaLight.intensity;
    }

    void Update()
    {
        lavaLight.intensity = baseIntensity + Mathf.PerlinNoise(Time.time * 2f, 0f) * 0.5f;
    }
}
