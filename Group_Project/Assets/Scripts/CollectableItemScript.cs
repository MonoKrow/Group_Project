using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemScript : MonoBehaviour
{
    public float scoreIncrease;

    private bool onRunning = false;

    void Start()
    {

    }

    public void onRun()
    {
        if (!onRunning)
        {
            onRunning = true;
            GameMannager.instance.scoreIncrease(scoreIncrease);
            Destroy(gameObject);
        }
    }
}
