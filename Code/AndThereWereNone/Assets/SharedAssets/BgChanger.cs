using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgChanger : MonoBehaviour {

    private static bool created = false;
    private string LastScene = "";

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            ChangeCol();
        }
    }

    private void ChangeCol()
    {
        var randcol = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 0f);
        Camera.main.backgroundColor = randcol;
    }

    // Update is called once per frame
    void Update ()
    {
        var thisScene = SceneManager.GetActiveScene().name;
        if (LastScene != thisScene)
        {
            if (thisScene != "TextScene" && thisScene != "MainMenu" && thisScene != "Credits")
            {
                ChangeCol();
            }
        }

        LastScene = thisScene;
    }
}
