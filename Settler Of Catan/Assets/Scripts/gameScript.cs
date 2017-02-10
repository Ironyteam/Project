using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class gameScript : MonoBehaviour {
    public Canvas gameCanvas;
    public Canvas escapeCanvas;
	public Canvas optionsCanvas;
    public InputField chatInput;
           int brickQuantity  = 0;
           int goldQuantity   = 100;
           int oreQuantity    = 0;
    public int randomNumber1;
    public int randomNumber2;
    public int randomNumberActual;
           int sheepQuantity  = 0;
           int tempEscPressed = 0; 
           // Will be removed once i figure out how getKeyDown really works
           int wheatQuantity  = 0;
           int woodQuantity   = 0;
           string player1Name = "GhostRag3: ";
    public System.Random randDiceObject = new System.Random();
    public Text brickScore;
    public Text chatBox;
    public Text diceValue;
    public Text goldScore;
    public Text oreScore;
    public Text sheepScore;
    public Text wheatScore;
    public Text woodScore;

    void Awake()
    {
	    gameCanvas.enabled = true;
	    escapeCanvas.enabled = false;
		optionsCanvas.enabled = false;
    }

    void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(tempEscPressed == 0)
            {
                escapeCanvas.enabled = true;
                tempEscPressed = 1;
            }
            else
            {
                escapeCanvas.enabled = false;
                tempEscPressed = 0;
            }
        }
    }

    public void moreWheat()
    {
        wheatQuantity++;
        wheatScore.text = wheatQuantity.ToString(); 
    }

    public void lessWheat()
    {
        if(wheatQuantity > 0)
        {
            wheatQuantity--;
            wheatScore.text = wheatQuantity.ToString();
        }
    }
       
    public void moreSheep()
    {
        sheepQuantity++;
        sheepScore.text = sheepQuantity.ToString();
    }

    public void lessSheep()
    {
        if(sheepQuantity > 0)
        {
            sheepQuantity--;
            sheepScore.text = sheepQuantity.ToString();
        }
    }
       
    public void moreBrick()
    {
        brickQuantity++;
        brickScore.text = brickQuantity.ToString();
    } 

    public void lessBrick()
    {
        if(brickQuantity > 0)
        {
            brickQuantity--;
            brickScore.text = brickQuantity.ToString();
        }
    }   
    public void moreWood()
    {
        woodQuantity++;
        woodScore.text = woodQuantity.ToString();
    }

    public void lessWood()
    {
        if(woodQuantity > 0)
        {
            woodQuantity--;
            woodScore.text = woodQuantity.ToString();
        }
    }

    public void moreOre()
    {
        oreQuantity++;
        oreScore.text = oreQuantity.ToString();
    }

    public void randDice()
    {
        randomNumber1 = randDiceObject.Next(1, 7);
        randomNumber2 = randDiceObject.Next(1, 7);
        randomNumberActual = randomNumber1 + randomNumber2;

        if(randomNumberActual == 4 || randomNumberActual == 8 || randomNumberActual == 12 || randomNumberActual == 6 || randomNumberActual == 10 || randomNumberActual == 11)
        {
            moreSheep();
            moreBrick();
        }
        else if(randomNumberActual == 3 || randomNumberActual == 2 || randomNumberActual == 5 || randomNumberActual == 7 || randomNumberActual == 9)
        {
            moreOre();
            moreWood();
            moreWheat();
        }             
        diceValue.text = randomNumberActual.ToString();
    }

    public void lessOre()
    {
        if(oreQuantity > 0)
        {
            oreQuantity--;  
            oreScore.text = oreQuantity.ToString();
        }
    }

    public void moreGoldSettlement()
    {
        goldQuantity += 20;
        goldScore.text = goldQuantity.ToString();
    }

    public void moreGoldCity()
    {
        goldQuantity += 50;
        goldScore.text = goldQuantity.ToString();
    }

    public void lessGold()
    {
        if(goldQuantity > 0)
        {
            goldQuantity--;
            goldScore.text = goldQuantity.ToString();
        }
    }

    public void updateText()
    {
        chatBox.text += '\n' + player1Name + chatInput.text;
    }

    public void goBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void resume()
    {
        escapeCanvas.enabled = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }

	public void showOptions()
	{
		escapeCanvas.enabled = false;
		optionsCanvas.enabled = true;
    }

	public void hideOptions()
	{
		escapeCanvas.enabled = true;
		optionsCanvas.enabled = false;
	}
}
