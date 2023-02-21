using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazershoot : MonoBehaviour
{
    public GameObject lazerProjectilePrefab;
    public float rotationDirection;

    [Space]

    public float speed;
    public float lifetime;
    public float shotCD;

    private float spawnCD = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnCD > 0)
        {
            spawnCD -= Time.fixedDeltaTime;
        }
        else
        {
            GameObject tempedit = Instantiate(lazerProjectilePrefab, transform.position, Quaternion.identity);
            tempedit.transform.Rotate(new Vector3(0, 0, rotationDirection));
            tempedit.GetComponent<ProjectileScript>().speed = speed;
            tempedit.GetComponent<ProjectileScript>().lifetime = lifetime;

            spawnCD += shotCD;
        }
    }
}
