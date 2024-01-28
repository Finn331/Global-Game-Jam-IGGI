using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject HTPPanel;

    void Start()
    {
        HTPPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HtpButton()
    {
        HTPPanel.SetActive(true);
    }

    public void HtpBack()
    {
        HTPPanel.SetActive(false);
    }
}
