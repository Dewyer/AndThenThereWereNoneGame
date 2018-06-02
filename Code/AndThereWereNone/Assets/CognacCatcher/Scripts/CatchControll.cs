using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CatchControll : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(DropStuff());
	    StartCoroutine(CountDown());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float maxTimeBetween = 1f;
    public GameObject Droplett;
    public Sprite Cognac;
    public Sprite Pill;
    public Slider CognacProgress;
    public Slider PillProgress;


    public int CognacCount;
    public int PillCount;
    public int TargetCount;

    public float Timeleft;
    public Text TimeLeftText;

    public IEnumerator DropStuff()
    {
        for (;;)
        {
            yield return new WaitForSeconds(Random.Range(0.2f,1f)*maxTimeBetween);

            SpawnDroplet();
        }
    }

    private void SpawnDroplet()
    {
        var screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        var fallPos = new Vector3(Random.Range(-0.9f,0.9f)*screenSize.x,screenSize.y+0.5f,0);
        var gm = (GameObject) Instantiate(Droplett, fallPos, Quaternion.identity);

        gm.GetComponent<SpriteRenderer>().sprite = Random.Range(0f, 1f) >= 0.5f? Pill:Cognac;
    }

    public void UpdateProgress()
    {
        CognacProgress.value = (float) CognacCount / TargetCount;
        PillProgress.value = (float) PillCount / TargetCount;

    }

    public IEnumerator CountDown()
    {
        while (Timeleft != 0)
        {
            Timeleft--;
            var fp = (Timeleft - (Timeleft % 60)) / 60;
            var sp = Timeleft % 60;
            TimeLeftText.text = String.Format("{0}:{1}",fp.ToString(),sp<10?("0"+sp):sp.ToString());
            yield return new WaitForSeconds(1f);
        }

        //lost 
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void GotDroplet(GameObject what)
    {
        var gotPill = what.gameObject.GetComponent<SpriteRenderer>().sprite == Pill;

        if (gotPill)
        {
            if (CognacCount == TargetCount)
            {
                PillCount++;
                if (PillCount == TargetCount)
                {
                    //WIN
                    SceneManager.LoadSceneAsync("TextScene");
                }
            }
            else
            {
                CognacCount = CognacCount == 0 ? 0 : CognacCount - 1;
            }
        }
        else
        {
            if (CognacCount != TargetCount)
            {
                CognacCount++;
            }
            else
            {
                PillCount = PillCount == 0 ? 0 : PillCount - 1;
            }
        }
        UpdateProgress();
    }
}
