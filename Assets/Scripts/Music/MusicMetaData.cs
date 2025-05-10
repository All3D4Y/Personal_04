using UnityEngine;

[System.Serializable]
public class MusicMetaData
{
    public Sprite coverImage;
    public string title;
    public string artist;
    [Range(1, 7)] public int difficulty;
    public string musicDataAddress;
}
