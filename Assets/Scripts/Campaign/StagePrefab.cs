using UnityEngine;

public class StagePrefab : MonoBehaviour
{
    public Material glow;
    public Material normal;
    public GameObject monsterPrefab;
    [SerializeField] int stageIndex;

    MeshRenderer platformRenderer;

    public int StageIndex => stageIndex;

    void Awake()
    {
        platformRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        //Initialize();
    }

    public void Initialize()
    {
        GameProgressData data = SaveManager.LoadData();
        if (data.clearedStages.Contains(stageIndex))
            ClearedStage();
        else
            UnClearedStage();
    }

    void UnClearedStage()
    {
        if (transform.GetChild(1).childCount == 0)
            Instantiate(monsterPrefab, transform.GetChild(1));
        platformRenderer.material = glow;
    }

    void ClearedStage()
    {
        if (transform.GetChild(1).childCount != 0)
            Destroy(transform.GetChild(1).GetChild(0).gameObject);
        platformRenderer.material = normal;
    }
}
