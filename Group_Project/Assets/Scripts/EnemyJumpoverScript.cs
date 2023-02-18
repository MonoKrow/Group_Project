using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpoverScript : MonoBehaviour
{
    public EnemyScript target;

    public GameObject mainHitBoxIgnore;
    public bool jumpChecker;


    [HideInInspector]
    public int checkerID = 0;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (jumpChecker)
        {
            target.jumpCheckT[checkerID] = true;
        }
        else
        {
            target.jumpCheckB = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>() || other.gameObject == mainHitBoxIgnore)
        {
            return;
        }

        if (other.GetComponent<BoxCollider>())
        {
            if (other.GetComponent<BoxCollider>().isTrigger == false)
            {
                collidePass();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>() || other.gameObject == mainHitBoxIgnore)
        {
            return;
        }

        if (other.GetComponent<BoxCollider>())
        {
            if (other.GetComponent<BoxCollider>().isTrigger == false)
            {
                collidePass();
            }
        }
    }

    private void collidePass()
    {
        if (jumpChecker)
        {
            target.jumpCheckT[checkerID] = false;
        }
        else
        {
            target.jumpCheckB = false;
        }
    }
}
