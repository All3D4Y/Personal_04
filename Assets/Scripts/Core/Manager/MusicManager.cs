using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    MusicData musicData;

    public MusicData MusicData => musicData;

    public void SetData(MusicData data)
    {
        musicData = data;
    }

    public void ClearData()
    {
        musicData = null;
    }
}
