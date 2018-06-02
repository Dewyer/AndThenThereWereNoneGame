using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.SharedAssets;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSceneControll : MonoBehaviour
{
    public Text MainText;
    public GameObject NextButton;
    public GameObject BackButton;

    public TextSource source;
    public Panel ThisPanel;

    private int _atSlide;
    private int slideCount;

    public int AtSlide
    {
        get { return _atSlide; }
        set
        {
            if (value >= 0 && value < slideCount)
            {
                _atSlide = value;

                AtSlideChanged();
            }

        }
    }

    private void AtSlideChanged()
    {
        if (AtSlide == slideCount - 1)
        {
            //Last
            NextButton.GetComponentInChildren<Text>().text = "Start";
        }
        else
        {
            NextButton.GetComponentInChildren<Text>().text = ">>>>";

        }

        MainText.text = ThisPanel.Strings[AtSlide.ToString()];

        if (AtSlide == 0)
        {
            BackButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            BackButton.GetComponent<Button>().interactable = true;

        }
    }

    public void ChangeButtonPressed(int direction)
    {
        if (direction == 1 && AtSlide == slideCount-1)
        {
            //navigate
            PlayerPrefs.SetString("lastScene", ThisPanel.NextScene);
            PlayerPrefs.SetString("nextPanel",ThisPanel.NextPanel);
            PlayerPrefs.Save();
            SceneManager.LoadScene(ThisPanel.NextScene);
        }
        else
        {
            AtSlide += direction;

        }
    }

    void Start ()
	{
	    try
	    {
	        source = JsonConvert.DeserializeObject<TextSource>(File.ReadAllText("Resources/en.json"));
	    }
	    catch (Exception e)
	    {
	        Application.Quit();
	    }

	    var thisPanelName = PlayerPrefs.GetString("nextPanel");
	    var matchPanels = source.Panels.FindAll(x => x.Name == thisPanelName);
	    if (matchPanels.Count == 0)
	    {
	        Application.Quit();
        }

	    ThisPanel = matchPanels[0];
	    slideCount = ThisPanel.Strings.Count;
	    AtSlide = 0;
	}
	

}
