using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassicModeUIController : MonoBehaviour
{
    public Text ScoreLabel;
    public Text LevelLabel;
    public Text LivesLabel;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject saveNameScreen;

    public void UpdateScoreLabel(int _value){
        ScoreLabel.text = "Score: " + _value.ToString();
    }

    public void UpdateLivesLabel(int _lives){
        LivesLabel.text = "Lives: " + _lives.ToString();
    }

    public void UpdateLevelLabel(int _level){
        LevelLabel.text = "Level: " + _level.ToString();
    }

    public void ShowGameOverScreen(bool show){
        gameOverScreen.SetActive(show);
    }

    public void ShowSaveNameScreen(bool show){
        saveNameScreen.SetActive(show);
    }

    public void ShowPauseScreen(bool show){
        pauseScreen.SetActive(show);
    }
}

