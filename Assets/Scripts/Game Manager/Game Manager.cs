using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseBorder;
    public GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        pauseBorder.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("1. Main Menu");
    }

    public void PauseSetting()
    {
        pauseBorder.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseBorder.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
}
