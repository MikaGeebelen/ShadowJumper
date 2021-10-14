using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private Text _currentScore;
    private int _score = 0;

    public void IncreaseScore(int addedScore)
    {
        _score += addedScore;

        _currentScore.text = $"Score: {_score}";
        PlayerPrefs.SetInt("NewScore",_score);
    }

    public void ResetScore()
    {
        _score = 0;
        _currentScore.text = $"Score: {_score}";
    }
}
