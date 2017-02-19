using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class NavigationScript : MonoBehaviour
{
    public Canvas createCanvas;
    public Canvas creditCanvas;
    public Canvas mainCanvas;
    public Canvas optionsCanvas;
    public Canvas quitCanvas;

    public gameScript BoardManagerLink;

    void Awake()
    {
       optionsCanvas.enabled = false;
       createCanvas.enabled  = false;
       creditCanvas.enabled  = false;
       quitCanvas.enabled    = false;
    }

    public void optionsOn()
    {
       optionsCanvas.enabled = true;
       mainCanvas.enabled    = false;
       createCanvas.enabled  = false;
       creditCanvas.enabled  = false;
       quitCanvas.enabled    = false;
    }

    public void createOn()
    {
       optionsCanvas.enabled = false;
       mainCanvas.enabled    = false;
       createCanvas.enabled  = true;
       creditCanvas.enabled  = false;
       quitCanvas.enabled    = false;
    }

    public void PreGameBoardSceneOn()
    {
        BoardManager.startingGame = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void creditOn()
    {
       optionsCanvas.enabled = false;
       mainCanvas.enabled    = false;
       createCanvas.enabled  = false;
       creditCanvas.enabled  = true;
       quitCanvas.enabled    = false;
    }

    public void returnOn()
    {
       optionsCanvas.enabled = false;
       mainCanvas.enabled    = true;
       createCanvas.enabled  = false;
       creditCanvas.enabled  = false;
       quitCanvas.enabled    = false;
    }

    public void quitOn()
    {
       optionsCanvas.enabled = false;
       mainCanvas.enabled    = false;
       createCanvas.enabled  = false;
       creditCanvas.enabled  = false;
       quitCanvas.enabled    = true;
    }

    public void exitGame()
    {
       Application.Quit();
    } 

    public void loadOn()
    {
 	  UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    
    public void netLobbyOn()
    {
	   UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
    
    public void gameLobbyOn()
    {
	   UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void boardManagerOn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }
}