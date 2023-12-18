using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    UIController uIController;
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
    public int hitBlocks;

    private void Start() {
        uIController = FindObjectOfType<UIController>();
        blockController = FindObjectOfType<BlockController>();
        // audioController = FindObjectOfType<AudioController>();
        ball = FindObjectOfType<Ball>();

        Lives = 3;
        Level = 0;
        Score = 0;
        hitBlocks = 0;
        isGameOver = false;
        isGamePaused = false;

        RefreshUI();
        blockController.DrawLevel(Level);

        nameInputField.onEndEdit.AddListener(delegate{SaveScore();});
    }

    void RefreshUI(){
        uIController.UpdateScoreLabel(Score);
        uIController.UpdateLivesLabel(Lives);
        uIController.UpdateLevelLabel(Level);
        uIController.ShowGameOverScreen(false);
        uIController.ShowPauseScreen(false);
    }

    public void AddScore(int _value){
        Score += _value;
        uIController.UpdateScoreLabel(Score);    
    }

    public void AddLife(){
        Lives++;
    }

    public void RemoveLife(){
        Lives -= 1;
        uIController.UpdateLivesLabel(Lives);
        if(Lives<=0){
            GameOver();
        }
    }

    public void GameOver(){
        Time.timeScale = 0;
        uIController.ShowSaveNameScreen(true);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        isGamePaused = true;
        uIController.ShowPauseScreen(true);
    }

    public void UnpauseGame(){
        Time.timeScale = 1;
        isGamePaused = false;
        uIController.ShowPauseScreen(false);
    }

    public void BackToMenu(){
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
        hitBlocks = 0;
        uIController.UpdateLevelLabel(Level);
        ball.SpawnBall();
        blockController.DrawLevel(Level);
    }

    public void SaveScore(){
        User user = new User();
        user.name = nameInputField.text;
        user.score = Score;
        DBManager.Instance.SaveRecord(DBManager.Instance.ClassicLeaderboardData, "ClassicLeaderboardData", user);
        uIController.ShowSaveNameScreen(false);
        uIController.ShowGameOverScreen(true);
    }
}
