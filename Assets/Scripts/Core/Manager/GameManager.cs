using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    HitZone hitZone;

    public HitZone HitZone => hitZone;
    protected override void OnInitialize()
    {
        Time.timeScale = 0.5f;

        hitZone = FindAnyObjectByType<HitZone>();
    }
}
