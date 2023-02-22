using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemScript : MonoBehaviour
{
    public float itemChangeAmount;

    private bool onRunning = false;

    void Start()
    {

    }

    public void onRun()
    {
        if (!onRunning)
        {
            onRunning = true;
            GameMannager.instance.itemCountChange(itemChangeAmount);
            GameMannager.instance.playAudioOneshot(GameMannager.audioSourcesName.gameplay, GameMannager.audioClipsName.CollectPoint, 0.01f);
            Destroy(gameObject);
        }
    }
}
