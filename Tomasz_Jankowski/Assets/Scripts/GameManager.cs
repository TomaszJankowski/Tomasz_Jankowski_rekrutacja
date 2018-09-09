using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Restart"))
        {
            StartCoroutine("Restart");
        }
        if (Input.GetButtonDown("Exit"))
            Application.Quit();
    }

    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Scene thisScene = SceneManager.GetActiveScene();
        Debug.Log(thisScene);
        SceneManager.LoadScene(thisScene.name);
    }
}
