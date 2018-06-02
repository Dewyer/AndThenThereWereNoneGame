using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RevolverController : MonoBehaviour {

    public Text ReadyText;
    public ShellControll ShellControll;
    public GoSignControll GoSign;

    public bool DidGameStart = false;
    public bool DidAIShoot = false;

    public float minDelay;
    public float maxDelay;

    public float minDelaySign;
    public float maxDelaySign;

    public GameObject PlayerRevo;
    public GameObject AIRevo;

    public Vector3 AIStarting;

    public bool FirstInit = true;

	void Start () 
    {
        AIStarting = AIRevo.transform.parent.localPosition;

        ShellControll.ShellCount = 0;
        StartNewRound();
	}

    private IEnumerator InitSetSequence()
    {
        if (FirstInit)
        {
            yield return new WaitForSeconds(1.4f);
			FirstInit = false;
        }

        ReadyText.text = "Ready";
        yield return new WaitForSeconds(1f);
        for (int i = 3; i >= 1; i--)
        {
            ReadyText.text = String.Format("{0}", i);
            yield return new WaitForSeconds(1f);
        }
        ReadyText.text = "Set";
        yield return new WaitForSeconds(1f);

        ReadyText.text = "";

        StartCoroutine(GoSignSetSequence());
    }

    public void StartNewRound()
    {
        DidGameStart = false;
        DidAIShoot = false;
        GoSign.InState = GoSignControll.GoState.Hiden;
        StartCoroutine(InitSetSequence());

    }

    private IEnumerator GoSignSetSequence()
    {
        GoSign.InState = GoSignControll.GoState.NotGo;

        var delay = UnityEngine.Random.Range(minDelaySign, maxDelaySign);
        yield return new WaitForSeconds(delay);

        GoSign.InState = GoSignControll.GoState.Go;
        DidGameStart = true;
        StartCoroutine(AIShootingSequence());
    }

    private IEnumerator AIShootingSequence()
    {
        var delay = UnityEngine.Random.Range(minDelay,maxDelay);
        yield return new WaitForSeconds(delay / 1000f);
        if (DidGameStart)
        {
            DidAIShoot = true;
            DoRevolverAnim(false);
            OnLoseRound();
        }
    }

    public void DoRevolverAnim(bool isPlayer)
    {
        Debug.Log(isPlayer + " bangs");

		var other = !isPlayer ? PlayerRevo : AIRevo;
        StartCoroutine(AnimateRevolver(isPlayer ? PlayerRevo : AIRevo,other));
        //other.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(force, new Vector3(dir*0.42f,0.42f,0f)+other.transform.position );
    }

    private IEnumerator AnimateRevolver(GameObject revolver,GameObject other)
    {
        other.GetComponent<Animator>().SetBool("Died", true);
        revolver.GetComponent<Animator>().SetBool("DidShot", true);
        yield return new WaitForSeconds(1f);
        revolver.GetComponent<Animator>().SetBool("DidShot", false);
        other.GetComponent<Animator>().SetBool("Died", false);
    }

    public void OnLoseRound()
    {
        Debug.Log("Kee");
        GameObject.FindGameObjectWithTag("Finish").GetComponent<Animator>().SetBool("PlayerDies", true);
        StartCoroutine(StartGameOver());
    }

    private IEnumerator StartGameOver()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void RegisterPlayerShoot()
    {
        if (DidAIShoot)
            return;

        if (ShellControll.ShellCount <= ShellControll.MaxShells)
        {
            ShellControll.ShellCount+=1;
            StartNewRound();
        }
        else
        {
            //WonOVer
            Debug.Log("Player final won");
            SceneManager.LoadSceneAsync("TextScene");
        }
        
    }

    public void OnScreenClick()
    {

        if (!DidGameStart)
        {
            return;
        }

        if (GoSign.InState == GoSignControll.GoState.Go)
        {
            RegisterPlayerShoot();
        }
        else
        {
            //Lost
            OnLoseRound();
        }
        DoRevolverAnim(true);
    }

}
