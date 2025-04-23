using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;

    GameHUDViewModel gameHUDViewModel;

    public static UIManager Instance => instance;
    public GameHUDViewModel GameHUDViewModel => gameHUDViewModel;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Initialize()
    {
        gameHUDViewModel = new GameHUDViewModel();
        NoteManager.Instance.ComboManager.onCombo += gameHUDViewModel.ComboUpdate;
        NoteManager.Instance.ComboManager.onScore += gameHUDViewModel.ScoreUpdate;
    }

    public void CleanUp()
    {
        NoteManager.Instance.ComboManager.onCombo -= gameHUDViewModel.ComboUpdate;
        NoteManager.Instance.ComboManager.onScore -= gameHUDViewModel.ScoreUpdate;
    }
}
