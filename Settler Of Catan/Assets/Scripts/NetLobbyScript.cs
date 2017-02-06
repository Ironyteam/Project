
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetLobbyScript : MonoBehaviour {
    
    [System.Serializable]
    public struct gameLobbyText
    {
        public Text gameNameText;
        public Text hostNameText;
        public Text playerNumberText;
        public Text statusText;
    }
    
    public struct gameLobbyValues
    {  
        public int    numOfPlayers;
        public string hostName;  
        public string gameName;  
        public string status;
    }

    
    public gameLobbyText[]   lobbyListText   = new gameLobbyText[40];
    public gameLobbyValues[] lobbyListValues = new gameLobbyValues[40];
    
    public void createGame()
    {
	    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void returnToMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
