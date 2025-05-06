using UnityEngine;

public class HUDManager : MonoBehaviour
{
    static HUDManager instance;

    GameHUDViewModel gameHUDViewModel;
    JudgeUI judgeUI;

    public static HUDManager Instance => instance;
    public GameHUDViewModel GameHUDViewModel => gameHUDViewModel;
    public bool IsInitialized { get; private set; }

    void Awake()
    {
        if (instance == null)
            instance = this;

        judgeUI = GetComponentInChildren<JudgeUI>();
    }

    public void Initialize()
    {
        gameHUDViewModel = new GameHUDViewModel();

        // 델리게이트 등록
        NoteManager.Instance.ComboManager.onCombo += gameHUDViewModel.ComboUpdate;
        NoteManager.Instance.ComboManager.onScore += gameHUDViewModel.ScoreUpdate;
        HitZone.Instance.onHit += gameHUDViewModel.OnHitNote;

        // 초기화
        judgeUI.Initialize();

        IsInitialized = true;
    }

    public void CleanUp()
    {
        // 델리게이트 해제
        NoteManager.Instance.ComboManager.onCombo -= gameHUDViewModel.ComboUpdate;
        NoteManager.Instance.ComboManager.onScore -= gameHUDViewModel.ScoreUpdate;
        HitZone.Instance.onHit -= gameHUDViewModel.OnHitNote;
        gameHUDViewModel = null;

        // 클린업
        judgeUI.CleanUp();
    }
}
