using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int speed;
    public bool onGround;
    public SpriteAnimatorScript spriteAnimatorScript;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimatorScript = GetComponent<SpriteAnimatorScript>();
    }

    // Update is called once per frame
    void Update()
    {

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
            if (onGround && GetComponent<Rigidbody>().velocity.y <= 0)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, 5, 0);
                onGround = false;
                spriteAnimatorScript.ChangeAnimation(3);
            }
        }

        if (onGround && GetComponent<Rigidbody>().velocity.y <= 0)
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
}
