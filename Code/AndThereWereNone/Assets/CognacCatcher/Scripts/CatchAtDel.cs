using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAtDel : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        gameObject.GetComponent<ParticleSystem>().Play();
	    StartCoroutine(KillMe());
	}

    private IEnumerator KillMe()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
