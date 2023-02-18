using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerBoxScript : MonoBehaviour
{
    public PlayerScript target;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectableItemScript>())
        {
            other.GetComponent<CollectableItemScript>().onRun();
        }
    }
}
