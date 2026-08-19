using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContrainer;
    private Transform entryTemplate;

    private List<HighscoreEntry> HighscoreEntryList;
    private List<Transform> HighscoreEntryTransformList;


    private void Awake()
    {
        entryContrainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContrainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        HighscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ score = 10000, name = "AAA" },
            new HighscoreEntry{ score = 21435, name = "PAS" },
            new HighscoreEntry{ score = 567, name = "DIN" },
            new HighscoreEntry{ score = 12677, name = "LOP" },
            new HighscoreEntry{ score = 1, name = "KIK" },
            new HighscoreEntry{ score = 96895, name = "BUT" },
            new HighscoreEntry{ score = 2137724, name = "QER" },
            new HighscoreEntry{ score = 1300, name = "MMM" },
        };

        HighscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in HighscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContrainer, HighscoreEntryTransformList);
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
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
