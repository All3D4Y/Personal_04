using UnityEngine;

public class ScoreManager
{
    ComboManager comboManager;

    int score = 0;

    public int Score => score;

    public ScoreManager(ComboManager comboManager)
    {
        this.comboManager = comboManager;
        Initialize();
    }

    public void Initialize()
    {
        score = 0;
        comboManager.onScore += GetScore;
    }

    public void CleanUp()
    {
        comboManager.onScore -= GetScore;
    }

    void GetScore(int score)
    {
        this.score += score;
    }
}
