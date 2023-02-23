using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderDamagePlayer : MonoBehaviour
{
    public int damage;

    public Vector3 knockbackForce;
    public Vector3 knockbackForceMutiplyer;
    public Vector3 knockbackForceOffset;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>())
        {
            GameMannager.instance.healthChange(-damage, transform.position + knockbackForceOffset, knockbackForceMutiplyer, knockbackForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>())
        {
            GameMannager.instance.healthChange(-damage, transform.position + knockbackForceOffset, knockbackForceMutiplyer, knockbackForce);
        }
    }
}
