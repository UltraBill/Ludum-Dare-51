using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Rendering.Universal.Light2D))]
public class lightingScript : MonoBehaviour
{
    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;
    
    float random;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0.0f, 65535.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
