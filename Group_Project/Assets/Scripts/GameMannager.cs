using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public static GameMannager instance;
    public GameObject playerObject;

    [Space]
    [Space]
    [Space]

    public float itemLeft = 0;
    public float health = 0;

    [Space]
    [Space]
    [Space]

    public GameObject backgroundMenu;
    public GameObject pauseMenu;
    public GameObject gamewinMenu;
    public GameObject gameoverMenu;

    [SerializeField]
    public enum gameStateList
    {
        gamePlay,
        gameWin,
        gameLose,
        pause
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            changeGameState(gameStateList.gamePlay);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            changeGameState(gameStateList.pause);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            changeGameState(gameStateList.gameWin);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            changeGameState(gameStateList.gameLose);
        }
    }


    public void itemCountChange(float amount)
    {
        itemLeft += amount;
    }

    public void changeGameState(gameStateList state)
    {
        switch (state)
        {
            case gameStateList.gamePlay:
                {
                    backgroundMenu.SetActive(false);
                    pauseMenu.SetActive(false);
                    gamewinMenu.SetActive(false);
                    gameoverMenu.SetActive(false);
                    Time.timeScale = 1;
                    break;
                }

            case gameStateList.pause:
                {
                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(true);
                    gamewinMenu.SetActive(false);
                    gameoverMenu.SetActive(false);
                    Time.timeScale = 0;
                    break;
                }

            case gameStateList.gameWin:
                {
                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(false);
                    gamewinMenu.SetActive(true);
                    gameoverMenu.SetActive(false);
                    Time.timeScale = 0;
                    break;
                }

            case gameStateList.gameLose:
                {
                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(false);
                    gamewinMenu.SetActive(false);
                    gameoverMenu.SetActive(true);
                    Time.timeScale = 0;
                    break;
                }
        }
    }
}
