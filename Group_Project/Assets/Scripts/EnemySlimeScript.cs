using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteAnimatorScript>().ChangeAnimation(0, 6, true);
    }
}
