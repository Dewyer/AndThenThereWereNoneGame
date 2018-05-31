using System.Collections;
using System.Collections.Generic;
using Assets.SharedAssets;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuControll : MonoBehaviour
{

    public Slider VolSlider;

    public void StartPressed()
    {
        PlayerPrefs.SetString("nextPanel","00Story");
        PlayerPrefs.Save();
        SceneManager.LoadScene("TextScene");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    public void VolChanged()
    {
        GameObject.FindGameObjectWithTag("SoundTrack").gameObject.GetComponent<AudioSource>().volume = VolSlider.value;
    }
}
