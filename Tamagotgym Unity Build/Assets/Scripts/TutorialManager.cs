using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;

    public GameObject panel;
    public GameObject agilityButton;
    public GameObject strengthButton;
    public GameObject muteButton;
    public GameObject backgroundButton;
    public GameObject tileButton;
    public GameObject quitButton;

    private static int STAT_BUTTONS = 5;
    private static int PANEL = 9;
    private static int BACKGROUND_BUTTONS = 10;
    private static int QUIT_BUTTON = 11;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        agilityButton.SetActive(false);
        strengthButton.SetActive(false);
        muteButton.SetActive(false);
        backgroundButton.SetActive(false);
        tileButton.SetActive(false);
        quitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
        }

        if (popUpIndex < popUps.Length)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
            }
            if (popUpIndex == STAT_BUTTONS)
            {
                agilityButton.SetActive(true);
                strengthButton.SetActive(true);
            }
            if (popUpIndex == PANEL)
            {
                panel.SetActive(true);
            }
            if (popUpIndex == BACKGROUND_BUTTONS)
            {
                backgroundButton.SetActive(true);
                tileButton.SetActive(true);
            }
            if (popUpIndex == QUIT_BUTTON)
            {
                quitButton.SetActive(true);
            }
        }
        else
        {
            muteButton.SetActive(true);
            PlayerPrefs.SetString("doneTutorial", "true");
        }
    }
}
