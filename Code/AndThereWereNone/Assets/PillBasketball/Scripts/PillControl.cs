using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Score = 0;
    public int TargetScore = 3;
    public Text ScoreText;

    public void PillFallin(bool inGoal)
    {
        if (inGoal)
        {
            Score++;
            if (Score == TargetScore)
            {
                Debug.Log("Victory");
            }
        }
        else
        {
            Score = 0;
        }

        ScoreText.text = String.Format("{0}/{1}",Score,TargetScore);

        GameObject.FindGameObjectWithTag("Player").SendMessage("ResetPlayer");
    }
    
}
