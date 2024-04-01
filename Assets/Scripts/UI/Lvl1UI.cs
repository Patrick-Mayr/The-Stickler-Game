using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Lvl1UI : MonoBehaviour
{
    public Button unpauseButton;
    public Button mainMenuButton;
    public Image pauseMenu;
    public Image gameOverMenu;
    public bool gameOver = false;
    public PlayerHealth playerHealthScript;
    public Button gameOverMainMenuButton;
    public Button restartLevelButton;
    public Button nextLevelButton;
    public TextMeshProUGUI nextLevelButtonText; 
    public TextMeshProUGUI gameOverText;

    

    Scene currentScene;
    string sceneName;

    public bool levelCompleted; 


    // Start is called before the first frame update
    void Start()
    {

        

        unpauseButton.onClick.AddListener(OnUnpauseButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        gameOverMainMenuButton.onClick.AddListener(OnGameOverMainMenuButtonClick);
        restartLevelButton.onClick.AddListener(OnRestartLevelButtonClick);
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);


        Time.timeScale = 1;

         

    }

    void OnUnpauseButtonClick()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        Debug.Log("unpause button clicked");
    }

    void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }

    void OnGameOverMainMenuButtonClick()
    {
        
        gameOverMenu.gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu");
       
    }

    void OnRestartLevelButtonClick()
    {
        if (sceneName == "SampleScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if(sceneName == "Level 2")
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (sceneName == "Level 3")
        {
            SceneManager.LoadScene("Level 3");
        }


    }

    void OnNextLevelButtonClick()
    {


        //load next level scene

        if (sceneName == "SampleScene")
        {
            
            SceneManager.LoadScene("Level 2");
            
        }
        if(sceneName == "Level 2")
        {
           
            SceneManager.LoadScene("Level 3");
            
        }

        
    }

    void GameOver()
    {
        Time.timeScale = 0; 
        gameOverMenu.gameObject.SetActive(true);
        gameOver = false;

        if (levelCompleted != true)
        {
            gameOverText.text = "You died!";
            nextLevelButton.gameObject.SetActive(false);


        }
        else if(levelCompleted == true)
        {
            if (sceneName == "SampleScene" || sceneName == "Level 2")
            {
                gameOverText.text = "You beat the level!";
                restartLevelButton.gameObject.SetActive(false);
            }
            else if(sceneName == "Level 3")
            {
                gameOverText.text = "You beat the Stickler!";
                restartLevelButton.gameObject.SetActive(false);
                nextLevelButton.gameObject.SetActive(false);
            }
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
        {
            if (playerHealthScript.health <= 0)
            {
                gameOver = true;
            }

            if (gameOver == true && Time.timeScale == 1)
            {
                GameOver();
            }
        }

        currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
    }
}
