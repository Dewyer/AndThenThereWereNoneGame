using System.Collections;
using System.Collections.Generic;
using Assets.SharedAssets;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuControll : MonoBehaviour
{

    public Slider VolSlider;

    public void StartPressed()
    {
        var res = JsonConvert.SerializeObject(new TextSource()
        {
            Panels = new List<Panel>() {new Panel() {Strings = new Dictionary<string, string>() {{"asd", "asd"},{"ass","my"}}},new Panel() {Strings = new Dictionary<string, string>(){{"my","ass"}}}}
        });

        File.WriteAllText("Resources/enb.json",res);
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
