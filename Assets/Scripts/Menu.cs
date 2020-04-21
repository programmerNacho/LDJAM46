using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public void NewGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowControls()
    {
        panel.SetActive(true);
    }
    public void QuitControls()
    {
        panel.SetActive(false);
    }
}
