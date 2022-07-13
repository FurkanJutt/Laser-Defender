using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    int sceneNumber;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadNormalMode()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("NormalMode");
    }

    public void LoadHardMode()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("HardMode");
    }

    public void LoadExpertMode()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("ExpertMode");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }

    public static string PreviousLevel { get; private set; }
    private void OnDestroy()
    {
        PreviousLevel = gameObject.scene.name;
    }

    public void RestartGame()
    {    
        SceneManager.LoadScene(Level.PreviousLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
