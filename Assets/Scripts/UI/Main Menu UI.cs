using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public Button levelSelectButton;
    public Button quitButton;
    public Button lvl1Button;
    public Button lvl2Button;
    public Button lvl3Button;
    public Button lvl4Button; 

    // Start is called before the first frame update
    void Start()
    {
        levelSelectButton.onClick.AddListener(OnLevelSelectButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        lvl1Button.onClick.AddListener(OnLvl1ButtonClick);
        lvl2Button.onClick.AddListener(OnLvl2ButtonClick);
        lvl3Button.onClick.AddListener(OnLvl3ButtonClick);
        lvl4Button.onClick.AddListener(OnLvl4ButtonClick);
    }

    void OnLevelSelectButtonClick()
    {

    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }

    void OnLvl1ButtonClick()
    {

    }

    void OnLvl2ButtonClick()
    {

    }

    void OnLvl3ButtonClick()
    {

    }

    void OnLvl4ButtonClick()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
