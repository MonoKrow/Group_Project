﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMannager : MonoBehaviour
{
    public static GameMannager instance;
    public GameObject playerObject;
    public GameObject goalObject;

    [Space]
    [Space]
    [Space]

    public List<AudioSource> audioSource;

    [SerializeField]
    public enum audioSourcesName
    {
        BGAudio,
        gameplay
    }


    public List<AudioClip> audioClip;

    [SerializeField]
    public enum audioClipsName
    {
        BGM,
        CollectPoint,
        PlayerJump,
        PlayerGotHit,
        PlayerDeath,
        Win
    }

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
    public GameObject instructionsMenu;

    private bool firstStartUp = true;
    private bool keyESCDown = false;

    [SerializeField]
    public enum gameStateList
    {
        gamePlay,
        gameWin,
        gameLose,
        pause,
        instructions
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }

        AudioListener.volume = 10;
        changeGameState(gameStateList.instructions);
        playAudio(audioSourcesName.BGAudio, audioClipsName.BGM, 0.01f, true);
    }

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            healthChange(-1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscKeyDown();
        }
    }

    public void itemCountChange(float amount)
    {
        itemLeft += amount;

        if (itemLeft <= 0)
        {
            goalObject.SetActive(true);
        }
    }

    public void healthChange(float amount)
    {
        health += amount;

        if (health <= 0)
        {
            playerObject.GetComponent<PlayerScript>().onDeath();
        }
    }

    public void playAudio(audioSourcesName _audioSource, audioClipsName _audioClip, float volume, bool loop)
    {
        audioSource[(int)_audioSource].volume = volume;
        audioSource[(int)_audioSource].loop = loop;
        audioSource[(int)_audioSource].clip = audioClip[(int)_audioClip];
        audioSource[(int)_audioSource].Play();
    }

    public void playAudioOneshot(audioSourcesName _audioSource, audioClipsName _audioClip, float volume)
    {
        audioSource[(int)_audioSource].PlayOneShot(audioClip[(int)_audioClip], volume);
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
                    instructionsMenu.SetActive(false);
                    Time.timeScale = 1;
                    break;
                }

            case gameStateList.pause:
                {

                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(true);
                    gamewinMenu.SetActive(false);
                    gameoverMenu.SetActive(false);
                    instructionsMenu.SetActive(false);
                    Time.timeScale = 0;
                    break;
                }

            case gameStateList.gameWin:
                {
                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(false);
                    gamewinMenu.SetActive(true);
                    gameoverMenu.SetActive(false);
                    instructionsMenu.SetActive(false);
                    Time.timeScale = 0;
                    break;
                }

            case gameStateList.gameLose:
                {
                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(false);
                    gamewinMenu.SetActive(false);
                    gameoverMenu.SetActive(true);
                    instructionsMenu.SetActive(false);
                    Time.timeScale = 0;
                    break;
                }

            case gameStateList.instructions:
                {
                    backgroundMenu.SetActive(true);
                    pauseMenu.SetActive(false);
                    gamewinMenu.SetActive(false);
                    gameoverMenu.SetActive(false);
                    instructionsMenu.SetActive(true);
                    Time.timeScale = 0;
                    break;
                }
        }
    }

    public void OnEscKeyDown()
    {
        if (pauseMenu.activeInHierarchy)
        {
            changeGameState(gameStateList.gamePlay);
        }
        else if (instructionsMenu.activeInHierarchy)
        {
            BackToPauseClick();
        }
        else if (gamewinMenu.activeInHierarchy || gamewinMenu.activeInHierarchy)
        {
            OnRestartClick();
        }
        else
        {
            changeGameState(gameStateList.pause);
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        changeGameState(gameStateList.gamePlay);
    }

    public void ContinueClick()
    {
        changeGameState(gameStateList.gamePlay);
    }

    public void InstructionsClick()
    {
        changeGameState(gameStateList.instructions);
    }

    public void BackToPauseClick()
    {
        if (firstStartUp)
        {
            changeGameState(gameStateList.gamePlay);
            firstStartUp = false;
        }
        else
        {
            changeGameState(gameStateList.pause);
        }
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
