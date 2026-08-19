using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContrainer;
    private Transform entryTemplate;
    
    private void Awake()
    {
        entryContrainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContrainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 60f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContrainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default: rankString = rank + "TH"; break;
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = Random.Range(0, 10000);
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = "AAA";
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        }
    }
}
