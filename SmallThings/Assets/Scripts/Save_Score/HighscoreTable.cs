using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContrainer;
    private Transform entryTemplate;

    private List<Transform> highscoreEntryTransformList;

    public int playerScore;
    public string playerName;

    private void Awake()
    {
        entryContrainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContrainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        if (highscores.highscoreEntryList.Count < 10)
        {
            for (int h = highscores.highscoreEntryList.Count; h > 10; h --)
            {
                highscores.highscoreEntryList.RemoveAt(10);
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContrainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH";
                break;

            case 1:
                rankString = "1ST";
                break;

            case 2:
                rankString = "2ND";
                break;

            case 3:
                rankString = "3RD";
                break;
        }

        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().color = Color.cyan;
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.cyan;
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().color = Color.cyan;
        }

        ColorUtility.TryParseHtmlString("#FFD200", out Color color1);
        ColorUtility.TryParseHtmlString("#C6C6C6", out Color color2);
        ColorUtility.TryParseHtmlString("#B76F56", out Color color3);

        switch (rank)
        {
            default:
                entryTransform.Find("Trophy").gameObject.SetActive(false);
                break;

            case 1:
                entryTransform.Find("Trophy").GetComponent<Image>().color = color1;
                break;

            case 2:
                entryTransform.Find("Trophy").GetComponent<Image>().color = color2;
                break;

            case 3:
                entryTransform.Find("Trophy").GetComponent<Image>().color = color3;
                break;
        }

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int newScore, string newName)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = newScore, name = newName };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        if (highscores.highscoreEntryList.Count < 10)
        {
            for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
            {
                highscores.highscoreEntryList.RemoveAt(10);
            }
        }
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
