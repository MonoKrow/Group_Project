using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject groundCheckerObject;

    public int speed;

    [HideInInspector]
    public SpriteAnimatorScript spriteAnimatorScript;

    [HideInInspector]
    public float knockbackCD = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimatorScript = GetComponent<SpriteAnimatorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackCD > 0)
        {
            knockbackCD -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().velocity -= new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;

        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().velocity += new Vector3(speed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround && GetComponent<Rigidbody>().velocity.y <= 0)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 5, GetComponent<Rigidbody>().velocity.z);
                groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = false;
                spriteAnimatorScript.ChangeAnimation(3);
            }
        }

        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround && GetComponent<Rigidbody>().velocity.y <= 0)
        {
            if (GetComponent<Rigidbody>().velocity.x > 0.2f || GetComponent<Rigidbody>().velocity.x < 0.2f && GetComponent<Rigidbody>().velocity.x != 0f)
            {
                spriteAnimatorScript.ChangeAnimation(1);
            }
            else
            {
                spriteAnimatorScript.ChangeAnimation(0);
            }
        }


    }

    public void onKnockback(Vector3 fromPosition, Vector3 forceMutiplyer, Vector3 addedForce)
    {
        if (knockbackCD <= 0)
        {
            Vector3 temp = ((transform.position + new Vector3(0, 0.175f, 0)) - fromPosition).normalized;
            Vector3 temp2 = (new Vector3(temp.x * forceMutiplyer.x, temp.y * forceMutiplyer.y, temp.z * forceMutiplyer.z) + addedForce);

            if (GetComponent<Rigidbody>().velocity.y < 0)
            {
                GetComponent<Rigidbody>().velocity += temp2;
            }
            else if (GetComponent<Rigidbody>().velocity.y > temp2.y)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(temp2.x, 0, temp2.z);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x + temp2.x, temp2.y, GetComponent<Rigidbody>().velocity.z + temp2.z);
            }

            knockbackCD = 0.5f;
        }
    }
}
