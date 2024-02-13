using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTE : MonoBehaviour
{
    /*
    public TextMeshProUGUI qTEText;
    bool startQTE;

    float timeRemaining;
    bool keyOne = true;
    bool keyTwo = false;
    bool keyThree = false;

    

    // Start is called before the first frame update
    void Start()
    {
        startQTE = true;
        timeRemaining = 3f;
        QuickTE();

    }

    void TextAndTimer()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }

        if (keyOne == true)
        {
            qTEText.text = "Press W! " + timeRemaining.ToString("0");

        }
        else if (keyTwo == true)
        {
            qTEText.text = "Press F! " + timeRemaining.ToString("0");

        }
        else if (keyThree == true)
        {
            qTEText.text = "Press T! " + timeRemaining.ToString("0");

        }
    }


    void QuickTE()
    {
        TextAndTimer();

        if (Input.GetKeyDown(KeyCode.W) && keyOne == true && timeRemaining != 0)
        {
            keyOne = false;
            keyTwo = true;
            timeRemaining = 3;
        }

        if (Input.GetKeyDown(KeyCode.F) && keyTwo == true && timeRemaining != 0)
        {
            keyTwo = false;
            keyThree = true;
            timeRemaining = 3;
        }

        if (Input.GetKeyDown(KeyCode.T) && keyThree == true && timeRemaining != 0)
        {
            qTEText.text = "You go through!";
            keyThree = false;
            EventOver();
        }
    }

    void RestartQTE()
    {
        if (timeRemaining <= 0)
        {
            //player is put back to original position if applicable
            //timer is set to 3 again
            //keyOne is true, keyTwo is false, keyThree is false
        }
    }

    void EventOver()
    {
        Debug.Log("animation of player completing task (i.e. walking along hallway");
        Debug.Log("player can move again");
    }


    // Update is called once per frame
    void Update()
    {

        if (startQTE == true)
        {

            QuickTE();

        }

        RestartQTE();
    }
    */
}


