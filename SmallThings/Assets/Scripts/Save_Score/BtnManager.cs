using UnityEngine;

public class BtnManager : MonoBehaviour
{
    public HighscoreTable highscoreTable;
    
    public void ClickBtn()
    {
        highscoreTable.playerScore = highscoreTable.playerScore + 1;
        Debug.Log(highscoreTable.playerScore);
    }
}