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

    public GameObject nameInputScreen;
    public InputField nameInput;

    private List<int> highScores = new List<int>(); //keep track of high scores in a list for leaderboard
    private List<string> highScoresNames = new List<string>(); //list of names that will go with their respective high score

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
        highScoresNames.Clear(); 

        for (int i = 0; i < 10; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("Score" + i, 0));
            highScoresNames.Add(PlayerPrefs.GetString("Name" + i, "..."));
        }
    }

    //update leaderboard
    void updateLeaderboard(int newScore, string newName)
    {
        highScores.Add(newScore);
        highScoresNames.Add(newName);

        //sorting algorithm for player names and scores that pair them together
        for (int i = 0; i < highScores.Count - 1; i++)
        {
            for (int j = i + 1; j <highScores.Count; j++)
            {
                if (highScores[j] > highScores[i])
                {
                    //scores will swap if there is a greater one
                    int tempS = highScores[i];
                    highScores[i] = highScores[j];
                    highScores[j] = tempS;

                    //names will swap with score
                    string tempN = highScoresNames[i];
                    highScoresNames[i] = highScoresNames[j];
                    highScoresNames[j] = tempN;
                }
            }
        }

        //keep top 10 scores with names
        if (highScores.Count > 10)
        {
            highScores.RemoveAt(10); //drops score at end of the list
            highScoresNames.RemoveAt(10); //drops name at end of the list
        }

        //save scores to PlayerPrefs
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("Score" + i, highScores[i]);
            PlayerPrefs.SetString("Name" + i, highScoresNames[i]);
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

        //check to see if score is in the top 10 to ask for playerName
        if (playerScore > highScores[highScores.Count - 1])
        {
            nameInputScreen.SetActive(true); //assures that the player is only asked their name if they make it on the leaderboard
            return;
        }
        
        //update leaderboard once game is over, does not ask for playerName 
        updateLeaderboard(playerScore, "...");

        //display
        displayLeaderboard();

        if (!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }

    }

    //submit button function
    public void submitName()
    {
        string name_submit = nameInput.text;

        if (string.IsNullOrEmpty(name_submit))
        {
            name_submit = "?"; //if player does not submit a name, default is chosen
        }

        //call leaderboard when submitted
        updateLeaderboard(playerScore, name_submit);
        nameInputScreen.SetActive(false);
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
            text += (i + 1) + ") " + highScoresNames[i] + " -> " + highScores[i] + "\n";
        }

        leaderboardText.text = text;
    }
}
