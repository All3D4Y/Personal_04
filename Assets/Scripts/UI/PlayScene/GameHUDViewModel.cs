using System;
using UnityEngine;

public class GameHUDViewModel
{
    int score;
    int combo;

    public Action<JudgeEnum> onHitNote;

    public int Score => score;
    public int Combo => combo;
    

    public GameHUDViewModel()
    {
        score = 0;
        combo = 0;
    }

    public void ScoreUpdate(int score)
    {
        this.score += score;
    }

    public void ComboUpdate(int combo)
    {
        this.combo = combo;
    }

    public void OnHitNote(JudgeEnum hit)
    {
        onHitNote?.Invoke(hit);
    }
}
