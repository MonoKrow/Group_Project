using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheckerScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public bool groundedCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (groundedCheck)
        {
            groundedCheck = false;
        }
        else
        {
            playerScript.onGround = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != playerScript.gameObject)
        {
            if (!groundedCheck)
            {
                groundedCheck = true;
                playerScript.onGround = true;
            }
        }
    }
}
