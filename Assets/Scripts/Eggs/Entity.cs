using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public int hitToDestroy = 1;
    [SerializeField] public int score = 1;
    [SerializeField] public int penalty = 1;
    [SerializeField] bool playerHasToDestroy = true;
    protected Manager manager;

    [SerializeField] GameObject scorePopUpPrefab;

    protected int remainingHits;

    protected virtual void Start()
    {
        remainingHits = hitToDestroy;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update()
    {
    }

    public virtual void registerHit()
    {
        remainingHits--;

        if (remainingHits <= 0)
        {
            destroy(true);
        }
    }

    public virtual void destroy(bool destroyedByPlayer)
    {
        ScoreManager sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (destroyedByPlayer)
        {
            if (playerHasToDestroy)
            {
                GameObject popup = GameObject.Instantiate(scorePopUpPrefab);
                popup.transform.position = this.transform.position;
                popup.GetComponent<PointPopUp>().setValue(score);
                sm.addScore(score);
            }
            else
            {
                manager.removeLife(penalty);
            }
            
        }
        else
        {
            if (playerHasToDestroy)
            {
                manager.removeLife(penalty);
            }
            else
            {
                GameObject popup = GameObject.Instantiate(scorePopUpPrefab);
                popup.transform.position = this.transform.position;
                popup.GetComponent<PointPopUp>().setValue(score);
                sm.addScore(score);
            }
        }

        Destroy(this.gameObject);
    }

}
