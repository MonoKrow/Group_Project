using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpin : MonoBehaviour
{
    public float move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, move);
    }
}
