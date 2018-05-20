using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillGoaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public float Wait;
    private List<string> ColsWith = new List<string>();

    public void OnCollisionEnter2D(Collision2D col)
    {
        ColsWith.Add(col.gameObject.tag);
        if (col.gameObject.tag == "Bound" || col.gameObject.tag == "BinBottom")
            StartCoroutine(ColCounter(col.gameObject.tag));
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (ColsWith.Contains(col.gameObject.tag))
        {
            ColsWith.Remove(col.gameObject.tag);
        }
    }

    public IEnumerator ColCounter(string targetTag)
    {
        if (!ColsWith.Contains(targetTag))
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForSeconds(Wait);
            if (ColsWith.Contains(targetTag))
            {
                GameObject.FindGameObjectWithTag("Controll").SendMessage("PillFallin",targetTag!="Bound");
            }
        }
    }
}
