using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerScript : MonoBehaviour
{
    public GameObject mainTarget;
    public GameObject hitboxTarget;

    public bool onGround = false;
    private bool groundedCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().center -= new Vector3(0, 0.005f, 0);
        GetComponent<BoxCollider>().size -= new Vector3(0.01f, 0, 0.01f);
    }

    private void FixedUpdate()
    {
        if (groundedCheck)
        {
            groundedCheck = false;
        }
        else
        {
            onGround = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (mainTarget.GetComponent<Rigidbody>().velocity.y > 0 || other.gameObject == hitboxTarget || other.GetComponent<PlayerScript>() || other.GetComponent<EnemyScript>())
        {
            return;
        }

        if (other.GetComponent<BoxCollider>())
        {
            if (other.GetComponent<BoxCollider>().isTrigger == false)
            {
                isGroundedCheck();
            }
        }
    }

    private void isGroundedCheck()
    {
        if (!groundedCheck)
        {
            groundedCheck = true;
            onGround = true;
        }
    }
}
