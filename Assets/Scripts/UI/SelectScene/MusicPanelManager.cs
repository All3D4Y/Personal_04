using UnityEngine;

public class MusicPanelManager : MonoBehaviour
{
    public MusicDataBase musicDataBase;
    public GameObject musicPanel;

    Transform panelParent;

    void Awake()
    {
        panelParent = transform.GetChild(1).GetChild(0).GetChild(0);
    }

    public void Initialize()
    {
        int count = musicDataBase.musicList.Count;
        for (int i = 0; i < count; i++)
        {
            MusicPanel panel = GameObject.Instantiate(musicPanel, panelParent).GetComponent<MusicPanel>();
            panel.Initialize(musicDataBase.musicList[i]);
        }
    }
}
