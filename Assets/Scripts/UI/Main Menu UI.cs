using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class MainMenuUI : MonoBehaviour
{
    public Button levelSelectButton;
    public Button quitButton;
    public Button lvl1Button;
    public Button lvl2Button;
    public Button lvl3Button;
    public Image levelSelectMenu;
    public Image mainMenu;
    public Button backButtion;
     

    // Start is called before the first frame update
    void Start()
    {
        levelSelectButton.onClick.AddListener(OnLevelSelectButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        lvl1Button.onClick.AddListener(OnLvl1ButtonClick);
        lvl2Button.onClick.AddListener(OnLvl2ButtonClick);
        lvl3Button.onClick.AddListener(OnLvl3ButtonClick);
        backButtion.onClick.AddListener(OnBackButtonClick);
        
    }

    void OnLevelSelectButtonClick()
    {
        levelSelectMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }

    void OnLvl1ButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnLvl2ButtonClick()
    {
        SceneManager.LoadScene("");
    }

    void OnLvl3ButtonClick()
    {
        SceneManager.LoadScene("");
    }

    void OnBackButtonClick()
    {
        levelSelectMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
