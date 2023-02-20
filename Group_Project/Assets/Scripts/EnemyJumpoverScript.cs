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

    private bool delayCheck = false;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (delayCheck)
        {
            delayCheck = false;
        }
        else
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
            else if (target.areaLimitGroup.Contains(other.gameObject))
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
            else if (target.areaLimitGroup.Contains(other.gameObject))
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
        delayCheck = true;
    }
}
