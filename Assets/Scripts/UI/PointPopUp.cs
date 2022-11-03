using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointPopUp : MonoBehaviour
{
    [SerializeField] protected float fadeSpeed;
    [SerializeField] protected float moveSpeed;

    private Color objectColor;

    void Start()
    {
        objectColor = this.GetComponent<TextMeshPro>().color;
    }

    void Update()
    {
        float fade = objectColor.a - (fadeSpeed * Time.smoothDeltaTime);

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fade);
        this.GetComponent<TextMeshPro>().color = objectColor;

        if (objectColor.a < 0)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(0, moveSpeed * Time.smoothDeltaTime, 0);
    }

    public void setValue(int value)
    {
        if(value > 0)
        {
            this.GetComponent<TextMeshPro>().text = "+" + value.ToString();
        }
        else if (value == 0)
        {
            this.GetComponent<TextMeshPro>().text = "";
        }
        else
        {
            this.GetComponent<TextMeshPro>().text = "-" + value.ToString();
        }
    }


}
