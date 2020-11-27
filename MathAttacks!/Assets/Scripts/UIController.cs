using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    private GameObject gameOver;
    private GameObject win;

    static bool created = false;

    void Awake()
    {
        if (!created) 
        {
            DontDestroyOnLoad(transform.root.gameObject);
            created = true;
        } else 
        {
            Destroy (transform.root.gameObject);
        }

        gameOver = this.gameObject.transform.GetChild(0).gameObject;
        win = this.gameObject.transform.GetChild(1).gameObject;
    }

    public void RestartTheScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        win.SetActive(false);
        gameOver.SetActive(false);
    }
}
