using System.Collections.Generic;

[System.Serializable]
public class GameProgressData
{
    public int lastClearedStage = -1;
    public List<int> clearedStages = new List<int>();
    public bool isFreePlayUnlocked = false;
}
