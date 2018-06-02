using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.SharedAssets;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public Text GameOverText;

    public void Start()
    {
        //Load random msg
        var source = JsonConvert.DeserializeObject<TextSource>(File.ReadAllText("Resources/en.json"));
        var gors = source.Panels.FindAll(x => x.Name == "GameOvers")[0];
        var randInd = Random.Range(0, gors.Strings.Count);

        GameOverText.text = gors.Strings.ToList()[randInd].Value;
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("lastScene"));
    }

    public void RageQuit()
    {
        Application.Quit();
    }
}
