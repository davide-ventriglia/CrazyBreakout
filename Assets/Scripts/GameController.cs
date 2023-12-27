using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    ClassicModeUIController UIController;
    BlockController blockController;
    public AudioController audioController;
    Ball ball;
    public int Lives;
    public int Score;
    public int Level;
    public bool isGameOver {get; private set;}
    public bool isGamePaused;
    public TMP_InputField nameInputField;
    public List<PowerUp> PowerUpsPrefabs;
    public int destroyedBlocks;

    private void Start() {
        UIController = FindObjectOfType<ClassicModeUIController>();
        blockController = FindObjectOfType<BlockController>();
        // audioController = FindObjectOfType<AudioController>();
        ball = FindObjectOfType<Ball>();

        Lives = 3;
        Level = 0;
        Score = 0;
        destroyedBlocks = 0;
        isGameOver = false;
        isGamePaused = false;

        RefreshUI();
        blockController.DrawLevel(Level);

        nameInputField.onEndEdit.AddListener(delegate{SaveScore();});
    }

    void RefreshUI(){
        UIController.UpdateScoreLabel(Score);
        UIController.UpdateLivesLabel(Lives);
        UIController.UpdateLevelLabel(Level);
        UIController.ShowGameOverScreen(false);
        UIController.ShowPauseScreen(false);
    }

    public void AddScore(int _value){
        Score += _value;
        UIController.UpdateScoreLabel(Score);    
    }

    public void AddLife(){
        Lives++;
    }

    public void RemoveLife(){
        Lives -= 1;
        UIController.UpdateLivesLabel(Lives);
        if(Lives<=0){
            GameOver();
        }
    }

    public void GameOver(){
        Time.timeScale = 0;
        UIController.ShowSaveNameScreen(true);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        isGamePaused = true;
        UIController.ShowPauseScreen(true);
    }

    public void UnpauseGame(){
        Time.timeScale = 1;
        isGamePaused = false;
        UIController.ShowPauseScreen(false);
    }

    public void BackToTitle(){
        BlockController.instance.ClearLevel();
        Time.timeScale = 1;
        SceneManager.LoadScene("Title",LoadSceneMode.Single);
    }

    public void RestartGame(){
        Level = 0;
        Lives = 3;
        Score = 0;

        RefreshUI();
        blockController.DrawLevel(Level);
        Time.timeScale = 1;
    }

    public void PlayNextLevel(){
        Level++;
        destroyedBlocks = 0;
        UIController.UpdateLevelLabel(Level);
        ball.SpawnBall();
        blockController.DrawLevel(Level);
    }

    public void SaveScore(){
        User user = new User();
        user.name = nameInputField.text;
        user.score = Score;
        DBManager.Instance.SaveNewPlayer(DBManager.Instance.ClassicLeaderboardData, "ClassicLeaderboardData", user);
        UIController.ShowSaveNameScreen(false);
        UIController.ShowGameOverScreen(true);
    }
}
