using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject groundCheckerObject;
    public GameObject jumpCheckGroup;

    public bool movingRight = true;
    public float speed;
    public float jumpHeight;
    public Vector3 knockbackForce;
    public Vector3 knockbackForceMutiplyer;
    public Vector3 knockbackForceOffset;

    [Space]

    public int jumpHeightScan;
    public float jumpHeightScanOffset;

    [Space]

    [HideInInspector]
    public bool jumpCheckB = true;
    [HideInInspector]
    public List<bool> jumpCheckT = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        jumpCheckT.Add(true);
        for (int loop = 1; loop < jumpHeightScan; loop++)
        {
            GameObject tempEdit = Instantiate(jumpCheckGroup.transform.GetChild(0).gameObject, jumpCheckGroup.transform);
            tempEdit.transform.localPosition += new Vector3(0, jumpHeightScanOffset * loop, 0);
            tempEdit.GetComponent<EnemyJumpoverScript>().checkerID = loop;
            jumpCheckT.Add(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumpCheckB && GetComponent<Rigidbody>().velocity.y <= 0)
        {
            onJump();
        }

        if (movingRight)
        {
            GetComponent<Rigidbody>().velocity += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            GetComponent<Rigidbody>().velocity -= new Vector3(speed * Time.deltaTime, 0, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void onJump()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround && jumpCheckT.Contains(true))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.z);
        }
        else
        {
            onTurn();
        }
    }

    public void onTurn()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            if (GetComponent<Rigidbody>().velocity.x > 0)
            {
                movingRight = false;
            }
            else if (GetComponent<Rigidbody>().velocity.x < 0)
            {
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            other.gameObject.GetComponent<PlayerScript>().onKnockback(transform.position + knockbackForceOffset, knockbackForceMutiplyer, knockbackForce);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            other.gameObject.GetComponent<PlayerScript>().onKnockback(transform.position + knockbackForceOffset, knockbackForceMutiplyer, knockbackForce);
        }
    }
}
