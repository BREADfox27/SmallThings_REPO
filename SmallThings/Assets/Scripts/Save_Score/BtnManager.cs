using UnityEngine;

public class BtnManager : MonoBehaviour
{
    public HighscoreTable highscoreTable;
    
    public void ClickBtn()
    {
        highscoreTable.playerScore = highscoreTable.playerScore + 1;
        highscoreTable.playerScoreText.text = "SCORE: " + highscoreTable.playerScore;
    }

    public void ReadName(string name)
    {
        highscoreTable.playerName = name;
    }

    public void Save()
    {
        highscoreTable.AddHighscoreEntry(highscoreTable.playerScore, highscoreTable.playerName);
        highscoreTable.playerScore = 0;
        highscoreTable.playerScoreText.text = "SCORE: " + highscoreTable.playerScore;
        highscoreTable.playerName = null;
    }
}