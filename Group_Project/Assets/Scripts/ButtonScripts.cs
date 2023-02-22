using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public GameMannager gameMannager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueClick()
    {
        gameMannager.changeGameState(GameMannager.gameStateList.gamePlay);
    }

    public void BackClick()
    {
        gameMannager.changeGameState(GameMannager.gameStateList.pause);
    }

    public void InstructionsClick()
    {
        gameMannager.changeGameState(GameMannager.gameStateList.instructions);
    }

}
