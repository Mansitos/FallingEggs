using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    void Start()
    {  
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Egg")
        {
            other.gameObject.transform.GetComponent<Egg>().destroy(false);
        }
    }
}
