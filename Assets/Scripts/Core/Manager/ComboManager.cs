using System;
using UnityEngine;

public class ComboManager
{
    ScoreManager scoreManager = null;

    int currentCombo = 0;
    int maxCombo = 0;

    public Action<int> onCombo;
    public Action<int> onScore;

    public ScoreManager ScoreManager => scoreManager;
    public int MaxCombo => maxCombo;
    float ScoreMultiplier
    {
        get 
        {
            if (currentCombo > 199)
                return 2.0f;
            else if (currentCombo > 99)
                return 1.5f;
            else if (currentCombo > 49)
                return 1.2f;
            else
                return 1.0f;
        }
    }

    public void Initialize()
    {
        currentCombo = 0;
        maxCombo = 0;
        scoreManager = new ScoreManager(this);
        GameManager.Instance.HitZone.onHit += OnNoteHit;
    }

    public void CleanUp()
    {
        scoreManager.CleanUp();
        scoreManager = null;
        GameManager.Instance.HitZone.onHit -= OnNoteHit;
    }

    void OnNoteHit(HitEnum hit)
    {
        if ((int)hit < 2)
        {
            currentCombo++;
            maxCombo = Mathf.Max(maxCombo, currentCombo);
            OnComboChanged(); // 콤보 UI 갱신
            if ((int)hit == 0)      // PERFECT
                GetScore(10000);
            else                    // GOOD
                GetScore(8000);
        }
        else
        {
            currentCombo = 0;
            OnComboChanged();
            if ((int)hit == 3)      // BAD
                GetScore(1000);
        }
    }

    void OnComboChanged()
    {
        onCombo?.Invoke(currentCombo);
    }

    void GetScore(int score)
    {
        float temp = score * ScoreMultiplier;
        onScore?.Invoke(Mathf.RoundToInt(temp));
    }
}
