using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMannager : MonoBehaviour
{
    public static GameMannager instance;
    public GameObject playerObject;
    public GameObject goalObject;
    public Slider screenScaleSlider;
    public Text screenScaleText;
    [Space]
    public GameObject healthBar;
    public Sprite filledHeart;
    public Sprite empthyHeart;

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
    private int damageTakenCD = 0;
    private bool scaleingScreen = false;

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

        if (!PlayerPrefs.HasKey("ScreenScale"))
        {
            PlayerPrefs.SetFloat("ScreenScale", 0.5f);
        }

        if (Display.main.systemWidth / 16f > Display.main.systemHeight / 9f)
        {
            Screen.SetResolution((int)(Display.main.systemHeight * PlayerPrefs.GetFloat("ScreenScale") * (16f / 9f)), (int)(Display.main.systemHeight * PlayerPrefs.GetFloat("ScreenScale")), FullScreenMode.Windowed);
        }
        else
        {
            Screen.SetResolution((int)(Display.main.systemWidth * PlayerPrefs.GetFloat("ScreenScale")), (int)(Display.main.systemWidth * PlayerPrefs.GetFloat("ScreenScale") * (9f / 16f)), FullScreenMode.Windowed);
        }

        AudioListener.volume = 10;
        changeGameState(gameStateList.instructions);
        playAudio(audioSourcesName.BGAudio, audioClipsName.BGM, 0.01f, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscKeyDown();
        }

        if (scaleingScreen && !Input.GetMouseButton(0))
        {
            scaleingScreen = false;

            if (!PlayerPrefs.HasKey("ScreenScale"))
            {
                PlayerPrefs.SetFloat("ScreenScale", 0.5f);
            }

            PlayerPrefs.SetFloat("ScreenScale", screenScaleSlider.value);
            screenScaleText.text = "Screen Scale: " + (Mathf.RoundToInt(screenScaleSlider.value * 100f) / 100f) + "x";

            if (Display.main.systemWidth / 16f > Display.main.systemHeight / 9f)
            {
                Screen.SetResolution((int)(Display.main.systemHeight * PlayerPrefs.GetFloat("ScreenScale") * (16f / 9f)), (int)(Display.main.systemHeight * PlayerPrefs.GetFloat("ScreenScale")), FullScreenMode.Windowed);
            }
            else
            {
                Screen.SetResolution((int)(Display.main.systemWidth * PlayerPrefs.GetFloat("ScreenScale")), (int)(Display.main.systemWidth * PlayerPrefs.GetFloat("ScreenScale") * (9f / 16f)), FullScreenMode.Windowed);
            }
        }
    }

    private void FixedUpdate()
    {
        if (damageTakenCD > 0 && health > 0)
        {
            damageTakenCD--;
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

    public void healthChange(float amount, Vector3 fromPosition, Vector3 forceMutiplyer, Vector3 addedForce)
    {
        if (damageTakenCD <= 0)
        {
            health += amount;

            for (int loop = 0; loop < health; loop++)
            {
                if (loop >= healthBar.transform.childCount)
                {
                    break;
                }

                healthBar.transform.GetChild(loop).GetComponent<Image>().sprite = filledHeart;
            }

            if (health > 0)
            {
                for (int loop = (int)health; loop < healthBar.transform.childCount; loop++)
                {
                    healthBar.transform.GetChild(loop).GetComponent<Image>().sprite = empthyHeart;
                }
            }
            else
            {
                for (int loop = 0; loop < healthBar.transform.childCount; loop++)
                {
                    healthBar.transform.GetChild(loop).GetComponent<Image>().sprite = empthyHeart;
                }
            }

            playerObject.GetComponent<PlayerScript>().onKnockback(fromPosition, forceMutiplyer, addedForce);
            playAudioOneshot(audioSourcesName.gameplay, audioClipsName.PlayerGotHit, 0.01f);

            if (health <= 0)
            {
                playerObject.GetComponent<PlayerScript>().onDeath();
            }

            damageTakenCD = 120;
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
                    screenScaleSlider.value = PlayerPrefs.GetFloat("ScreenScale");
                    screenScaleText.text = "Screen Scale: " + (Mathf.RoundToInt(screenScaleSlider.value * 100f) / 100f) + "x";
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
                    playAudioOneshot(audioSourcesName.gameplay, audioClipsName.Win, 0.015f);
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

    public void onScreenSlider()
    {
        if (Input.GetMouseButton(0))
        {
            scaleingScreen = true;
            screenScaleText.text = "Screen Scale: " + (Mathf.RoundToInt(screenScaleSlider.value * 100f) / 100f) + "x";
        }
    }
}
