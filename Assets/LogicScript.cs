using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;

    public Text scoreText;
    public Text highScoreText;

    public GameObject gameOverScreen;
    public Text leaderboardText;

    private List<int> highScores = new List<int>(); //keep track of high scores in a list for leaderboard

    private bool endGame = false; //added to stop scores from being added to leaderboard more than once

    void Start()
    {
        //load scoreboard when game starts
        LoadHighScores();

        //update
        scoreText.text = "Score: 0";
        highScoreText.text = "High Score: " + highScores[0]; //highest score in list with index 0

    }

    //load top 10 scores
    void LoadHighScores()
    {
        highScores.Clear();
        for (int i = 0; i < 10; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("Score" + i, 0));
        }
    }

    //update leaderboard
    void updateLeaderboard(int newScore)
    {
        highScores.Add(newScore);
        highScores.Sort((score1, score2) => score2.CompareTo(score1)); //sorts scores from highest to lowest

        //keep top 10 scores
        if (highScores.Count > 10)
        {
            highScores.RemoveAt(10); //drops score at end of the list
        }

        //save scores to PlayerPrefs
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("Score" + i, highScores[i]);
        }

        PlayerPrefs.Save();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        //checks conditional to see if game is running
        if (endGame) return;

        Debug.Log("Adding +" + scoreToAdd + " score");
        playerScore += scoreToAdd;
        scoreText.text = "Score: " + playerScore;

        //new highscore
        if (playerScore > highScores[0]){
        
            //update on display
            highScoreText.text = "High Score: " + playerScore;
        }
    }

    public void restartGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void gameOver()
    {
        //check conditional
        if (endGame) return;

        endGame = true; 
        
        //update leaderboard once game is over
        updateLeaderboard(playerScore);

        //display
        displayLeaderboard();

        if (!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }

    }

    void displayLeaderboard()
    {
        string text = "Best 10 Scores!!\n\n";

        for (int i = 0; i < highScores.Count; i++)
        {
            text += (i + 1) + ") " + highScores[i] + "\n";
        }

        leaderboardText.text = text;
    }
}
