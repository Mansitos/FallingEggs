using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] GameObject inputFeedbackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Egg" || hit.transform.tag == "Eagle" || hit.transform.tag == "AlienShip")
                {
                    hit.transform.GetComponent<Entity>().registerHit();
                }
            }

            GameObject feedbackProp = GameObject.Instantiate(inputFeedbackPrefab);
            feedbackProp.transform.position = ray.origin;
        }

        else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Egg" || hit.transform.tag == "Eagle" || hit.transform.tag == "AlienShip")
                {
                    hit.transform.GetComponent<Entity>().registerHit();
                }
            }

            GameObject feedbackProp = GameObject.Instantiate(inputFeedbackPrefab);
            feedbackProp.transform.position = ray.origin;
        }
    }
}
