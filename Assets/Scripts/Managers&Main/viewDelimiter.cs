using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewDelimiter : MonoBehaviour
{
    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = camera.orthographicSize * 0.5625f / camera.aspect;
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
