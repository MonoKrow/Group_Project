using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public List<GameObject> areaLimitGroup;

    public GameObject groundCheckerObject;
    public GameObject jumpCheckGroup;

    public bool movingRight = true;
    public float speed;
    public float airSpeed;
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

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            GetComponent<Rigidbody>().velocity *= Mathf.Pow(0.1f, Time.fixedDeltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().velocity *= Mathf.Pow(0.75f, Time.fixedDeltaTime);
        }

        if (!jumpCheckB)
        {
            onJump();
        }

        if (movingRight)
        {
            if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                GetComponent<Rigidbody>().velocity += new Vector3(airSpeed * Time.fixedDeltaTime, 0, 0);
            }

            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(speed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(airSpeed * Time.fixedDeltaTime, 0, 0);
            }

            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void onJump()
    {
        if (groundCheckerObject.GetComponent<GroundCheckerScript>().onGround)
        {
            if (jumpCheckT.Contains(true))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.z);
            }
            else
            {
                onTurn();
            }
        }
    }

    public void onTurn()
    {
        if (GetComponent<Rigidbody>().velocity.x > 0)
        {
            movingRight = false;
        }
        else if (GetComponent<Rigidbody>().velocity.x < 0)
        {
            movingRight = true;
        }
        else
        {
            movingRight = !movingRight;
        }
    }
}
