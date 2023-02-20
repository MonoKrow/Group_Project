using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatelogScript : MonoBehaviour
{
    public float distance = 0;
    public bool moveup = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveup)
        {
            float travel = 0.1f * Time.fixedDeltaTime;
            distance += travel;
            transform.position += transform.up * travel;

            if (distance > 0.1f)
            {
                moveup = !moveup;
            }
        }
        else
        {
            float travel = 0.1f * Time.fixedDeltaTime;
            distance -= travel;
            transform.position -= transform.up * travel;

            if (distance < -0.1f)
            {
                moveup = !moveup;
            }
        }

        GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.05f * Time.fixedDeltaTime);

        if (GetComponent<SpriteRenderer>().color.a <= 0)
        {
            Destroy(gameObject);
        }
    }


    /*
        public int movedown = 0;
    public int moveup = 0;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveup = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        if (moveup == 1)
        {
            if (count < 120)
                transform.position += transform.up * 0.033f * Time.deltaTime;
            count++;
            if (count > 120)
            {
                moveup = 0;
                count = 0;
                movedown = 1;
            }
        }
        if (movedown == 1)
        {
            if (count < 120)
                transform.position -= transform.up * 0.033f * Time.deltaTime;
            count++;
            if (count > 120)
            {
                moveup = 1;
                count = 0;
                movedown = 0;
            }
        }

    }
    */


}
