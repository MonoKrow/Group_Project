using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerBoxScript : MonoBehaviour
{
    public EnemyScript target;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>())
        {
            GameMannager.instance.healthChange(-1, transform.position + target.knockbackForceOffset, target.knockbackForceMutiplyer, target.knockbackForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>())
        {
            GameMannager.instance.healthChange(-1, transform.position + target.knockbackForceOffset, target.knockbackForceMutiplyer, target.knockbackForce);
        }
    }
}
