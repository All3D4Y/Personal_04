using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public string musicDataAddress;
    public GameObject environment;
    public GameObject bossMonster;
}
