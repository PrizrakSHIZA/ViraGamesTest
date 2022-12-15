using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
