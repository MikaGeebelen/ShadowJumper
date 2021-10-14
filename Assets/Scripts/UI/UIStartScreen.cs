using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartScreen : MonoBehaviour
{
    [SerializeField] private GameObject _MenuScreen = null;
    [SerializeField] private GameObject _howToPlayScreen = null;
    private void Start()
    {
        _MenuScreen.SetActive(true);
        _howToPlayScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); //game scene
    }

    public void OpenHowToPlay()
    {
        _MenuScreen.SetActive(false);
        _howToPlayScreen.SetActive(true);
    }
    public void BackToMain()
    {
        _MenuScreen.SetActive(true);
        _howToPlayScreen.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
