using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlaBla : MonoBehaviour
{

    public string FullText = "Bla ...   Bla";
    public string TextNow = "";
    public float timeElapsed;
    public float lettersPerSec;

    // Use this for initialization
    void Start ()
	{
	    timeElapsed = 0f;
	    TextNow = "";
	}

    public void OnDisable()
    {
        timeElapsed = 0f;
        TextNow = "";
    }

    // Update is called once per frame
    void Update ()
	{
	    timeElapsed += Time.deltaTime;
	    var charCount = (int)(timeElapsed * lettersPerSec);
	    if (charCount > FullText.Length)
	    {
	        charCount = 0;
	        timeElapsed = 0f;
	    }

	    TextNow = FullText.Substring(0, charCount);

	    gameObject.GetComponent<Text>().text = TextNow;
	}
}
