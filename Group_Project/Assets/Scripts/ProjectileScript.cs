using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float lifetime;
    [HideInInspector]
    public Vector3 muitplyKnockback;
    [HideInInspector]
    public Vector3 addKnockback;
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
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerBoxScript>())
        {
            other.GetComponent<PlayerTriggerBoxScript>().target.onKnockback(transform.position, muitplyKnockback, addKnockback);
        }
    }
}
