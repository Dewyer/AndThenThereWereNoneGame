using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargePushControll : MonoBehaviour
{

    public GameObject Hand;
    public GameObject Face;
    public GameObject Dialog;

    public Slider PushProgress;

    //face
    public KeyCode KeyBoardChargeControll;
    public bool IsLooking;
    public bool IsCharging;

    public float PercentPerSecond;
    public bool IsOver;

    public float MaxNotLookingSec;
    public float MaxLookingSec;
    public float NextNotLookingSec;
    public float NextLookingSec;

	// Use this for initialization
	void Start ()
	{
	    IsLooking = false;
	    IsCharging = false;
	    IsOver = false;

        RestartLookingSequence();
	}

    public void RestartLookingSequence()
    {
        NextNotLookingSec = Random.Range(0.7f, MaxNotLookingSec);
        NextLookingSec = Random.Range(0.7f, MaxLookingSec);

        StartCoroutine(LookingSequence());
    }

    public IEnumerator LookingSequence()
    {
        IsLooking = false;
        Dialog.SetActive(true);
        Face.GetComponent<Animator>().SetBool("Looking",false);
        yield return new WaitForSeconds(NextNotLookingSec);
        Face.GetComponent<Animator>().SetBool("Looking", true);
        yield return new WaitForSeconds(1f);
        Dialog.SetActive(false);
        IsLooking = true;
        yield return new WaitForSeconds(NextLookingSec);

        RestartLookingSequence();
    }

    // Update is called once per frame
    void FixedUpdate ()
	{
	    KeyBoardControll();
	    TouchControll();

	    if (IsCharging && IsLooking)
	    {
            //U lose
            Debug.Log("Lost");
	        IsOver = true;
	    }

	    if (!IsOver && IsCharging)
	    {
	        if (PushProgress.value == 1f)
	        {
                //WON
                Debug.Log("Won");
	        }
	        else
	        {
	            PushProgress.value += PercentPerSecond * Time.fixedDeltaTime;
	        }

	    }
	}

    public void Charging(bool started)
    {
        Hand.GetComponent<Animator>().SetBool("Pushing", started);
        IsCharging = started;
    }

    public void TouchControll()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Charging(true);
            }
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                Charging(false);
            }
        }
    }

    public void KeyBoardControll()
    {
        if (Input.GetKeyDown(KeyBoardChargeControll))
        {
            Charging(true);
        }

        else if (Input.GetKeyUp(KeyBoardChargeControll))
        {
            Charging(false);
        }

        else if (Input.GetKey(KeyBoardChargeControll) && !IsCharging)
        {
            Charging(true);
        }

        else if(!Input.GetKey(KeyBoardChargeControll) && IsCharging)
        {
            Charging(false);
        }
    }

    public void SwitchLooking()
    {
        IsLooking = !IsLooking;
        Face.GetComponent<Animator>().SetBool("Looking", IsLooking);
    }

}
