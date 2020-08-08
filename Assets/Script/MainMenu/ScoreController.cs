using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText1, scoreText2;
    [HideInInspector] public int score1, score2;

    public static ScoreController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        score1 = score2 = 0;
        scoreText1.text = "Score : "+score1.ToString();
        scoreText2.text = "Score : " + score2.ToString();
        scoreText1.gameObject.SetActive(true);
        if (GameControl.instance.data.whichMode == 1)
            scoreText2.gameObject.SetActive(true);
        else
            scoreText2.gameObject.SetActive(false);
    }

    public void AddScore(int i)
    {
        if (i == 1)
        {
            score1++;
            scoreText1.text = "Score : " + score1.ToString();
        }
        else
        {
            score2++;
            scoreText2.text = "Score : " + score2.ToString();
        }
    }
}
