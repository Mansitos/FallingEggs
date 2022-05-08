using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Entity
{
    [SerializeField] GameObject pickedUpEgg;
    private EagleAttackEvent eagleManager;

    protected override void Start()
    {
        base.Start();
        eagleManager = GetComponentInParent<EagleAttackEvent>();
    }


    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Egg" && !eagleManager.getHasAnEgg())
        {
            pickedUpEgg = other.gameObject;
            other.gameObject.transform.GetComponent<Egg>().enabled = false;
            other.gameObject.transform.position = this.transform.position - new Vector3(0, 1, 0);
            other.transform.parent = this.gameObject.transform;
            eagleManager.notifyCaptureEgg();

        }
    }

    public override void destroy(bool destroyedByPlayer)
    {
        if (pickedUpEgg != null)
        {
            pickedUpEgg.transform.parent = null;
            pickedUpEgg.GetComponent<Egg>().enabled = true;
        }
        Destroy(eagleManager);
        base.destroy(destroyedByPlayer);
    }

    public void notifyPenalty()
    {
        Debug.Log("Eagle stealed an egg!");
        pickedUpEgg.GetComponent<Egg>().destroy(false);
    }
}
