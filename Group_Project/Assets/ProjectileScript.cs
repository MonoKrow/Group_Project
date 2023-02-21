using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.fixedDeltaTime;

            transform.position += transform.right * speed * Time.fixedDeltaTime;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
