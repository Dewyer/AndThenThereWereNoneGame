using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeeController : MonoBehaviour {

    public List<GameObject> Bees;
    public GameObject BeePrefab;

    public int Timeleft = 120;
    public int MaxTime;
    public Text TimeLeftText;
    public Text KillCounter;
    public int Kills = 0;

	// Use this for initialization
	void Start ()
	{
        Bees = new List<GameObject>();

	    MaxTime = Timeleft;
	    StartCoroutine(CountDown());
	    StartCoroutine(SpawnBees());
	}
	
	// Update is called once per frame
	void Update ()
	{
        TouchControllCheck();
	    MouseControllCheck();
	}

    public IEnumerator SpawnBees()
    {
        for (;;)
        {
            var span = 1f/Mathf.Pow((MaxTime - Timeleft) * 0.05f, 1.10f);
            span = span >= 2 ? 1 : span;
            yield return new WaitForSeconds((float)span);
            SpawnBee();
        }
    }

    public void SpawnBee()
    {
        var screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        var pos = Random.Range(-0.9f, 0.9f);
        var realPos = new Vector3(0, 0, 0);
        var side = Random.Range(0, 4);
        var spacing = 1f;

        if (side == 0)
        {
            //UP
            realPos = new Vector3(pos * screenSize.x, screenSize.y + spacing, 0);
        }
        else if (side == 1)
        {
            //DOWN
            realPos = new Vector3(pos * screenSize.x, -screenSize.y - spacing, 0);

        }
        else if (side == 2)
        {
            //Left
            realPos = new Vector3(-screenSize.x - spacing, screenSize.y * pos, 0);

        }
        else if (side == 3)
        {
            //Right
            realPos = new Vector3(screenSize.x + spacing, screenSize.y * pos, 0);

        }
        var gg = (GameObject)Instantiate(BeePrefab, realPos, Quaternion.identity);
        Bees.Add(gg);
    }

    public void KillClickAt(Vector3 at)
    {
        at = new Vector3(at.x,at.y,0);
        var beesClicked =
            Bees.FindAll(bee => (bee.transform.position - at).magnitude <= (bee.transform.localScale.x));


        if (beesClicked.Count != 0)
        {
            //kills
            beesClicked.ForEach(x=>Bees.Remove(x));
            beesClicked.ForEach(x=>x.SendMessage("KillMe"));
            Kills += beesClicked.Count;
            KillCounter.text = Kills.ToString();
        }
    }

    public void MouseControllCheck()
    {
        var dw = Input.GetMouseButtonDown(0);
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        KillClickAt(pos);

    }

    public void TouchControllCheck()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                KillClickAt(Camera.main.ScreenToWorldPoint(touch.position));
            }
        }
    }

    public IEnumerator CountDown()
    {
        while (Timeleft != 0)
        {
            Timeleft--;
            var fp = (Timeleft - (Timeleft % 60)) / 60;
            var sp = Timeleft % 60;
            TimeLeftText.text = System.String.Format("{0}:{1}", fp.ToString(), sp < 10 ? ("0" + sp) : sp.ToString());
            yield return new WaitForSeconds(1f);
        }

        //WIN 
        SceneManager.LoadSceneAsync("TextScene");
    }
}
