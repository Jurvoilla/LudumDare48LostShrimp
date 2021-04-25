using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    internal float speed;
    private float maxSpeed = 5f;
    public Light2D light;
    public float sppedlight = 0.002f;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, -speed * Time.fixedDeltaTime, 0f));
        if(light.intensity >= 0.2f)
        {
            light.intensity -= sppedlight * Time.deltaTime;
        } 
    }
}
