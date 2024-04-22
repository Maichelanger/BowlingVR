using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    internal int currentRound = 0;

    private TextMeshProUGUI[] scoreTexts;
    private int totalScore = 0;

    private void Start()
    {
        scoreTexts = GetComponentsInChildren<TextMeshProUGUI>();
        scoreTexts[currentRound].color = Color.red;
    }

    public void UpdateScore(int score)
    {
        scoreTexts[currentRound].text = score.ToString();
        scoreTexts[currentRound].color = Color.black;

        totalScore += score;
        scoreTexts[10].text = totalScore.ToString();

        currentRound++;
        scoreTexts[currentRound].color = Color.red;
    }
}
