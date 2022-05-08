using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFeedback : MonoBehaviour
{
    [SerializeField] protected float fadeSpeed;

    private Color objectColor;

    void Start()
    {
        objectColor = this.GetComponent<Renderer>().material.color;      
    }

    void Update()
    {
        float fade = objectColor.a - (fadeSpeed * Time.deltaTime);

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fade);
        this.GetComponent<Renderer>().material.color = objectColor;

        if(objectColor.a < 0)
        {
            Destroy(gameObject);
        }
    }
}
