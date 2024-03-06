using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl1UI : MonoBehaviour
{
    public Button unpauseButton;
    public Button mainMenuButton;
    public Image pauseMenu;
    public Image gameOverMenu; 


    // Start is called before the first frame update
    void Start()
    {
        unpauseButton.onClick.AddListener(OnUnpauseButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    void OnUnpauseButtonClick()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
