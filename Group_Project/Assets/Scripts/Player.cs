using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public bool Isgrounded;
    public SpriteAnimatorScript Script;

    // Start is called before the first frame update
    void Start()
    {
        Script = GetComponent<SpriteAnimatorScript>();
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
            GetComponent<Rigidbody>().velocity += new Vector3(0, speed * Time.deltaTime, 0);
            Isgrounded = false;
            Script.ChangeAnimation(3);
        }

        if (GetComponent<Rigidbody>().velocity.x > 0.2f || GetComponent<Rigidbody>().velocity.x < 0.2f && GetComponent<Rigidbody>().velocity.x != 0f && Isgrounded == true)
        {
            Script.ChangeAnimation(1);
        }
        else
        {
            Script.ChangeAnimation(0);
        }

        if (GetComponent<Rigidbody>().velocity.y !> 0.2f)
        {
            Isgrounded = true;
        }

    }
}
