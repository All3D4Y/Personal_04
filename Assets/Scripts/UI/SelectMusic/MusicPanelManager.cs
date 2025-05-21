using UnityEngine;

public class MusicPanelManager : MonoBehaviour
{
    public MusicDataBase musicDataBase;
    public GameObject musicPanel;

    Transform panelParent;
    MusicPanel[] musicPanels;
    
    public MusicPanel[] MusicPanels => musicPanels;

    void Awake()
    {
        panelParent = transform.GetChild(2).GetChild(0).GetChild(0);
    }

    public void Initialize()
    {
        int count = musicDataBase.musicList.Count;
        musicPanels = new MusicPanel[count];
        for (int i = 0; i < count; i++)
        {
            MusicPanel panel = GameObject.Instantiate(musicPanel, panelParent).GetComponent<MusicPanel>();
            panel.Initialize(musicDataBase.musicList[i]);
            musicPanels[i] = panel;
        }
    }
}
