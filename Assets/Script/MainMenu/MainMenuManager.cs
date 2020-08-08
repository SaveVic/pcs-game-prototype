using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    const float width = 430f, height = 688f;

    private void Start()
    {
        Screen.SetResolution((int)width, (int)height, false);
        Camera.main.aspect = 10f / 16f;
    }

    public ScriptableData data;

    public void SinglePlayer()
    {
        data.whichMode = 0;
        SceneManager.LoadScene(1);
    }

    public void MultiPlayer()
    {
        data.whichMode = 1;
        SceneManager.LoadScene(1);
    }

    public void Practice()
    {
        data.whichMode = 2;
        SceneManager.LoadScene(1);
    }
}
