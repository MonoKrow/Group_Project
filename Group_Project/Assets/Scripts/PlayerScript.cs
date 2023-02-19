using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject groundCheckerObject;

    public float speed;
    public float airSpeed;
    public float maxSpeed;
    public float jumpPower;

    public float frictionGround;
    public float frictionAir;

    [HideInInspector]
    public SpriteAnimatorScript spriteAnimatorScript;

    [HideInInspector]
    public float knockbackCD = 0;

    private int keyDownMoveJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimatorScript = GetComponent<SpriteAnimatorScript>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (knockbackCD > 0)
        {
            knockbackCD -= Time.fixedDeltaTime;
        }

        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            GetComponent<Rigidbody>().velocity *= Mathf.Pow(frictionGround,Time.fixedDeltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().velocity *= Mathf.Pow(frictionAir, Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (keyDownMoveJump == 0)
            {
                keyDownMoveJump = 1;
            }
        }
        else
        {
            keyDownMoveJump = 0;
        }



        if (keyDownMoveJump == 1)
        {
            onJump();
            keyDownMoveJump = 2;
        }

        landCheck();
    }

    private void moveLeft()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            if (GetComponent<Rigidbody>().velocity.x > -maxSpeed)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(speed * Time.fixedDeltaTime, 0, 0);

                if (GetComponent<Rigidbody>().velocity.x < -maxSpeed)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
                }
            }
        }
        else
        {
            if (GetComponent<Rigidbody>().velocity.x > -maxSpeed)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(airSpeed * Time.fixedDeltaTime, 0, 0);

                if (GetComponent<Rigidbody>().velocity.x < -maxSpeed)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
                }
            }
        }
        GetComponent<SpriteRenderer>().flipX = true;
    }

    private void moveRight()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            if (GetComponent<Rigidbody>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(speed * Time.fixedDeltaTime, 0, 0);

                if (GetComponent<Rigidbody>().velocity.x > maxSpeed)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
                }
            }
        }
        else
        {
            if (GetComponent<Rigidbody>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(airSpeed * Time.fixedDeltaTime, 0, 0);

                if (GetComponent<Rigidbody>().velocity.x > maxSpeed)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
                }
            }
        }
        GetComponent<SpriteRenderer>().flipX = false;
    }

    private void landCheck()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            if (GetComponent<Rigidbody>().velocity.y <= 0)
            {
                if (GetComponent<Rigidbody>().velocity.magnitude > 0.2f)
                {
                    spriteAnimatorScript.ChangeAnimation(1);
                }
                else
                {
                    spriteAnimatorScript.ChangeAnimation(0);
                }
            }

            //GetComponent<Rigidbody>().drag = frictionGround;
        }
        else
        {
            if (spriteAnimatorScript.currentAnimation == 0)
            {
                spriteAnimatorScript.ChangeAnimation(1);
            }

            //GetComponent<Rigidbody>().drag = frictionAir;
        }
    }

    private void onJump()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround && GetComponent<Rigidbody>().velocity.y <= 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpPower, GetComponent<Rigidbody>().velocity.z);
            groundCheckerObject.GetComponent<GroundCheckerScript>().onGround = false;
            spriteAnimatorScript.ChangeAnimation(3);
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
