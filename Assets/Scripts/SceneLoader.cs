using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneIndex(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
