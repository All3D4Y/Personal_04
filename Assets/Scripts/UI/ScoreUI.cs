using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public int scoreUpMinSpeed;
    [Range(-10, 10)] public int scoreUpSpeed;
    public Sprite[] numSprites;

    Image[] numImages;

    int displayScore = 0;

    public int Score { get; private set; }

    void Awake()
    {
        numImages = new Image[transform.GetChild(0).childCount];

        for (int i = 0; i < numImages.Length; i++)
        {
            numImages[i] = transform.GetChild(0).GetChild(i).GetComponent<Image>();
            numImages[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {
        Score = 0;
        ShowScore();
    }

    void Update()
    {
        Score = UIManager.Instance.GameHUDViewModel.Score;

        if (displayScore < Score - 50)
        {
            int speed = Mathf.Max((Score - displayScore) * scoreUpSpeed, scoreUpMinSpeed);
            displayScore += (int)(Time.deltaTime * speed);
            displayScore = Mathf.Min(displayScore, Score);
            ShowScore();
        }
        else if (displayScore < Score)
        {
            displayScore = Score;
            ShowScore();
        }
    }

    public void ShowScore()
    {
        if (displayScore <= 0)
        {
            foreach (var img in numImages)
                img.gameObject.SetActive(false);
            return;
        }
        else
        {
            string scoreStr = displayScore.ToString();
            int strCount = scoreStr.Length;

            for (int i = 0; i < numImages.Length; i++)
            {
                if (i < strCount)
                {
                    int num = scoreStr[strCount - 1 - i] - '0';
                    numImages[i].sprite = numSprites[num];
                    numImages[i].gameObject.SetActive(true);
                }
                else
                {
                    numImages[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void GetScore(int score)
    {
        Score += score;
    }
}
