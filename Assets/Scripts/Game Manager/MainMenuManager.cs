using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject HTPPanel;
    public GameObject backHtp;

    public void Exit()
    {
        Application.Quit();
    }

    public void HtpButton()
    {
        HTPPanel.SetActive(true);
        backHtp.SetActive(true);
    }

    public void HtpBack()
    {
        HTPPanel.SetActive(false);
        backHtp.SetActive(false);
    }
}
