using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEndscreen : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore",0);
        int newScore = PlayerPrefs.GetInt("NewScore");


        if (highScore < newScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            _scoreText.text = $"Highscore\n{newScore}\nScore\n{newScore}";
        }
        else
        {
            _scoreText.text = $"Highscore\n{highScore}\nScore\n{newScore}";
        }

    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0); //game scene
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
