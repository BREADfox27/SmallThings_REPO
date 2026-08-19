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
        foreach (Transform obj in highscoreTable.highscoreEntryTransformList)
        {
            Destroy(obj.gameObject);
        }

        highscoreTable.highscoreEntryTransformList.Clear();
        highscoreTable.AddHighscoreEntry(highscoreTable.playerScore, highscoreTable.playerName);
        highscoreTable.StartHighscoreTable();

        highscoreTable.playerScore = 0;
        highscoreTable.playerScoreText.text = "SCORE: " + highscoreTable.playerScore;
        highscoreTable.playerName = null;
    }
}