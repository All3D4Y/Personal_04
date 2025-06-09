using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button campaign;
    public Button freePlay;
    public Button character;
    public Button status;

    Button quitGame;
    Button settings;
    Button left;
    Button right;

    void Awake()
    {
        quitGame = transform.GetChild(2).GetComponent<Button>();
        settings = transform.GetChild(3).GetComponent<Button>();
        left = transform.GetChild(4).GetComponent<Button>();
        right = transform.GetChild(5).GetComponent<Button>();
    }

    void OnEnable()
    {
        quitGame.onClick.AddListener(GameManager.Instance.QuitGame);
        //settings.onClick.AddListener();
    }
}
