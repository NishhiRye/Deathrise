

//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : WorldVariableCount
{

    public static MainMenu MenuInstance;
    
    //Canvas Variables
    //------------------------------------------------------------------------------
    public GameObject OptionsCanvas;
    public GameObject MenuCanvas;
    public GameObject DifficultyCanvas;
    public GameObject HelpCanvas;
    public GameObject LoadingCanvas;

    public GameObject CustomizationEasyCanvas;
    public GameObject CustomizationMediumCanvas;
    public GameObject CustomizationExpertCanvas;

    public GameObject BeginButtonEasy;
    public GameObject BeginButtonMedium;
    public GameObject BeginButtonExpert;

    public GameObject MediumButtonLocked;
    public GameObject VeteranButtonLocked;
    public GameObject MediumButtonUnlocked;
    public GameObject VeteranButtonUnlocked;

    public GameObject GameModeCanvas;
    public GameObject SingleplayerCanvas;
    public GameObject MultiplayerCanvas;

    public GameObject Operation1Canvas;
    public GameObject Operation2Canvas;
    public GameObject Operation3Canvas;

    public GameObject NickReady;    

    public GameObject RoomSetup;

    public GameObject CreatingRoom;
    public GameObject InsideRoom;

    public GameObject FindingRoom;

    public GameObject GameConnectionHolder;

    public GameObject GameStartButton;
    //------------------------------------------------------------------------------------

    //Game Starting
    //------------------------------------------------------------------------------------
   /* public static bool GameIsStarting;
    public Text StartingCountDown;
    public GameObject StartingFadeImage;
    public GameObject StartingReturn;
    bool isRoomOwner = false;*/
    //------------------------------------------------------------------------------------


    #region Awake
    //----------------------------------------------------------------------------------
    private void Awake()
    {
        MenuInstance = this;
        Cursor.visible = true;Cursor.lockState = CursorLockMode.None;
        WorldVariable = 0;

    }

    #endregion

    
    #region Update
    //----------------------------------------------------------------------------------
    private void Update()
    {
        if (!GameConnection.Instance.RoomOwner() /*&& !isRoomOwner*/  || GameConnection.GameIsStarting)
        {
            GameStartButton.SetActive(false);
        }
        else
        {
            //isRoomOwner = true;
            GameStartButton.SetActive(true);
        }

        /*if (GameIsStarting)
        {
            GameStartButton.SetActive(false);
            StartingReturn.SetActive(false);
            Image FadeImage = StartingFadeImage.GetComponent<Image>();
            Color fadeAlpha = FadeImage.color;
            fadeAlpha.a += Time.deltaTime;

            float fadeCountDown = 5f;
            fadeCountDown -= Time.deltaTime;
            StartingCountDown.text = "GAME IS STARTING!\n\n" + fadeCountDown.ToString("F0");
        }*/

        if (WorldVariable == 0)
        {
            BeginButtonEasy.SetActive(false);
            BeginButtonMedium.SetActive(false);
            BeginButtonExpert.SetActive(false);
        }

        if(WorldVariable == -1 || WorldVariable == 1 || WorldVariable  == 11 || WorldVariable == 111)
        {
            BeginButtonEasy.SetActive(true);
        }

        if (WorldVariable == -2 || WorldVariable == 2 || WorldVariable == 22 || WorldVariable == 222)
        {
            BeginButtonMedium.SetActive(true);
        }

        if (WorldVariable == -3 || WorldVariable == 3 || WorldVariable == 33 || WorldVariable == 333)
        {
            BeginButtonExpert.SetActive(true);
        }        
    }

    #endregion

    #region MenuOpensAndCloses
    //----------------------------------------------------------------------------------
    //Send the player to choose the game difficulty.
    public void StarGame()
    {
        MenuCanvas.SetActive(false);
        DifficultyCanvas.SetActive(true);


        if (UnlockBehaviour.MagnumUnlock)
        {
            MediumButtonLocked.SetActive(false);
            MediumButtonUnlocked.SetActive(true);
        }

        if (UnlockBehaviour.AWPVar)
        {
            VeteranButtonLocked.SetActive(false);
            VeteranButtonUnlocked.SetActive(true);
        }
    }

    //----------------------------------------------------------------------------------
    //Set Options Menu to run and shut down the Main Menu.
    public void OptionsMenu()
    {
        OptionsCanvas.SetActive(true);
        MenuCanvas.SetActive(false);

    }

    //----------------------------------------------------------------------------------
    //Set the player back to Main Menu.
    public void BackToMenu()
    {
        WorldVariable = 0;

        DifficultyCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
        HelpCanvas.SetActive(false);

        CustomizationEasyCanvas.SetActive(false);
        CustomizationMediumCanvas.SetActive(false);
        CustomizationExpertCanvas.SetActive(false);

    }


    //----------------------------------------------------------------------------------
    public void Help()
    {
        MenuCanvas.SetActive(false);
        HelpCanvas.SetActive(true);
    }

    //----------------------------------------------------------------------------------
    //Quit the game.
    public void QuitGame()
    {
        Application.Quit();

    }

    //----------------------------------------------------------------------------------
    //Set the game to easy.
    public void EasyDifficulty()
    {
        DifficultyCanvas.SetActive(false);
        CustomizationEasyCanvas.SetActive(true);  
        
    }

    //----------------------------------------------------------------------------------
    //Set the game to Medium.
    public void MediumDifficulty()
    {
        DifficultyCanvas.SetActive(false);
        CustomizationMediumCanvas.SetActive(true);

    }

    //----------------------------------------------------------------------------------
    //Set the game to Expert.
    public void ExpertDifficulty()
    {
        DifficultyCanvas.SetActive(false);
        CustomizationExpertCanvas.SetActive(true);

    }

    //----------------------------------------------------------------------------------
    public void CustomizeEasyZero()
    {
        // WorldVariable = 1;
        WorldVariable = -1;

    }

    //
    public void CustomizeEasyOne()
    {
       // WorldVariable = 1;
        WorldVariable = 0;

    }


    //
    public void CustomizeEasyTwo()
    {
        //WorldVariable = 11;
        WorldVariable = 0;

    }


    //
    public void CustomizeEasyThree()
    {
        //WorldVariable = 111;
        WorldVariable = 0;

    }

    //----------------------------------------------------------------------------------
    public void CustomizeMediumZero()
    {
        // WorldVariable = 1;
        WorldVariable = -2;

    }

    //
    public void CustomizeMediumOne()
    {
        //WorldVariable = 2;
        WorldVariable = 0;

    }


    //
    public void CustomizeMediumTwo()
    {
       // WorldVariable = 22;
        WorldVariable = 0;

    }


    //
    public void CustomizeMediumThree()
    {
        //WorldVariable = 222;
        WorldVariable = 0;

    }


    //----------------------------------------------------------------------------------

    public void CustomizeExpertZero()
    {
        // WorldVariable = 1;
        WorldVariable = -3;

    }

    //
    public void CustomizeExpertOne()
    {
        //WorldVariable = 3;
        WorldVariable = 0;

       
    }


    //
    public void CustomizeExpertTwo()
    {
       // WorldVariable = 33;
        WorldVariable = 0;

        
    }


    //
    public void CustomizeExpertThree()
    {
        //WorldVariable = 333;
        WorldVariable = 0;

        
    }


    //----------------------------------------------------------------------------------
    public void Begin()
    {
        CustomizationEasyCanvas.SetActive(false);
        CustomizationMediumCanvas.SetActive(false);
        CustomizationExpertCanvas.SetActive(false);
        DifficultyCanvas.SetActive(false);
        GameModeCanvas.SetActive(true);
        /*LoadingCanvas.SetActive(true);
        SceneManager.LoadScene("Main");*/
        /* if (!UnlockBehaviour.tutorialSkip)
         {
             LoadingCanvas.SetActive(true);
             SceneManager.LoadScene("Tutorial");
         }
         else
         {
             LoadingCanvas.SetActive(true);
             SceneManager.LoadScene("Main");
         }*/
    }

    //----------------------------------------------------------------------------------
    public void SPMode()
    {        
        GameModeCanvas.SetActive(false);
        SingleplayerCanvas.SetActive(true);
        MainManager.IsMultiplayer = false;
    }

    public void Operation1Load()
    {
        LoadingCanvas.SetActive(true);
        SceneManager.LoadScene("Main");
    }

    public void Operation2Load()
    {
        LoadingCanvas.SetActive(true);
        SceneManager.LoadScene("OP2");
        
    }

    public void Operation3Load()
    {
        LoadingCanvas.SetActive(true);
        SceneManager.LoadScene("OP3");
    }

    //----------------------------------------------------------------------------------
    public void MPMode()
    {                
        GameModeCanvas.SetActive(false);
        MultiplayerCanvas.SetActive(true);
        GameConnection.Instance.ConnectingToServer();
        MainManager.IsMultiplayer = true;
        if (GameConnection.Instance.PlayerConnected) {
            NickReady.SetActive(true);
        }

    }

   public void RoomSelect()
    {        
        NickReady.SetActive(false);        
        RoomSetup.SetActive(true);
        
    }

    public void CreateRoomCanvas()
    {
        RoomSetup.SetActive(false);
        CreatingRoom.SetActive(true);
    }

    public void InRoom()
    {
        CreatingRoom.SetActive(false);
        InsideRoom.SetActive(true);
        
    }

    public void FindRoom()
    {
        RoomSetup.SetActive(false);
        FindingRoom.SetActive(true);
    }

    public void FindedRoom()
    {
        FindingRoom.SetActive(false);
        InsideRoom.SetActive(true);
    }
    public void ReturnFromGameMode()
    {
        GameModeCanvas.SetActive(false);
        DifficultyCanvas.SetActive(true);
    }

    public void ReturnFromNickName()
    {
        GameModeCanvas.SetActive(true);
        MultiplayerCanvas.SetActive(false);
    }

    public void ReturnFromRoomChoice()
    {
        NickReady.SetActive(true);
        RoomSetup.SetActive(false);
    }
    public void Return()//return from room
    {
        MultiplayerCanvas.SetActive(true);
        RoomSetup.SetActive(true);
        FindingRoom.SetActive(false);
        CreatingRoom.SetActive(false);
        InsideRoom.SetActive(false);        

        GameConnectionHolder.GetComponent<GameConnection>().OnLeftRoom();
        GameConnection.Instance.LeaveRoom();

    }
    public void GameStart()
    {

        /* LoadingCanvas.SetActive(true);
         StartingFadeImage.SetActive(true);
         GameIsStarting = true;
         StartCoroutine(GameIsAboutToStart());    */
        SceneManager.LoadScene("Main");
    }

    IEnumerator GameIsAboutToStart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main");
    }
    #endregion

    
}
