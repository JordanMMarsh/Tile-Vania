using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour {

    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] int timeToLoadNextLevel = 2;

    private void Awake()
    {
        if (!sceneLoader)
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(timeToLoadNextLevel);
        sceneLoader.NextScene();
    }
}
