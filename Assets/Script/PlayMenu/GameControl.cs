using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    [HideInInspector] public bool isAlive1, isAlive2;
    [HideInInspector] public bool immortal1, immortal2;
    [HideInInspector] public bool isPause;

    public ScriptableData data;
    public GameObject pausePanel, pauseInfoText;
    public GameObject player, restartText;
    public TextMeshProUGUI lifeText1, lifeText2;
    public TextMeshProUGUI scoreDisplay;

    public GameObject wait1, wait2;
    [HideInInspector] public bool isWait;
    public GameObject scoreMultiDisplay;
    public TextMeshProUGUI multiScore1, multiScore2;

    int life1, life2;
    bool restart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isAlive1 = isAlive2 = true;
        immortal1 = immortal2 = false;
        isPause = false;
    }

    private void Start()
    {
        isWait = false;
        wait1.SetActive(false);
        wait2.SetActive(false);
        restartText.SetActive(false);
        scoreMultiDisplay.SetActive(false);
        restart = false;
        pausePanel.SetActive(false);
        if (data.whichMode == 2)
            pauseInfoText.SetActive(true);
        else
            pauseInfoText.SetActive(false);

        if (data.whichMode == 0)
        {
            life1 = 5;
            lifeText1.text = "Live : " + life1.ToString();
            lifeText1.gameObject.SetActive(true);
            lifeText2.gameObject.SetActive(false);
        }
        else if (data.whichMode == 1)
        {
            life1 = life2 = 5;
            lifeText1.text = "Live : " + life1.ToString();
            lifeText2.text = "Live : " + life2.ToString();
            lifeText1.gameObject.SetActive(true);
            lifeText2.gameObject.SetActive(true);
        }
        else
        {
            life1 = 5;
            lifeText1.text = "Live : -";
            lifeText1.gameObject.SetActive(true);
            lifeText2.gameObject.SetActive(false);
        }

        if(data.whichMode == 1)
        {
            player.SetActive(true);
        }
        else
        {
            player.SetActive(false);
        }
    }

    private void Update()
    {
        if (data.whichMode == 2 && Input.GetKeyDown(KeyCode.R))
        {
            GamePause();
        }
        else if (data.whichMode == 1)
        {

        }
    }

    #region PRACTICE

    public void GamePause()
    {
        Time.timeScale = 0f;
        isPause = true;
        pausePanel.SetActive(true);
        pauseInfoText.SetActive(false);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPause = false;
        pausePanel.SetActive(false);
        pauseInfoText.SetActive(true);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    #endregion

    #region SINGLE / MULTI PLAYER

    public void GetDamage(int which)
    {
        if (which == 1)
            immortal1 = true;
        else
            immortal2 = true;

        if (data.whichMode != 2)
        {
            switch (which)
            {
                case 1:
                    life1--;
                    lifeText1.text = "Live : " + life1.ToString();
                    break;
                case 2:
                    life2--;
                    lifeText2.text = "Live : " + life2.ToString();
                    break;
                default:
                    break;
            }

        }

        if (which == 1)
        {
            PlayerController.instance.PlayAnimationImmortal();
            StartCoroutine(Immortal(which));
        }
        else
        {
            PlayerControllerSide.instance.PlayAnimationImmortal();
            StartCoroutine(Immortal(which));
        }

    }

    public bool IsLifeRemaining(int which)
    {
        switch (which)
        {
            case 1:
                bool temp1 = (life1 <= 0) ? false : true;
                return temp1;
            case 2:
                bool temp2 = (life2 <= 0) ? false : true;
                return temp2;
            default:
                return false;
        }
    }

    public void ShowRestartTextSingle()
    {
        scoreDisplay.text = ScoreController.instance.score1.ToString();
        restart = true;
        restartText.SetActive(true);
    }

    public void ShowRestartTextMulti()
    {
        wait1.SetActive(false);
        wait2.SetActive(false);
        multiScore1.text = ScoreController.instance.score1.ToString();
        multiScore2.text = ScoreController.instance.score2.ToString();
        scoreMultiDisplay.SetActive(true);
    }

    public void Wait(int i)
    {
        isWait = true;
        if (i == 1)
        {
            wait1.SetActive(true);
        }
        else
        {
            wait2.SetActive(true);
        }
    }

    IEnumerator Immortal(int which)
    {
        yield return new WaitForSeconds(3f);
        if (which == 1)
            immortal1 = false;
        else
            immortal2 = false;
    }

    #endregion

}