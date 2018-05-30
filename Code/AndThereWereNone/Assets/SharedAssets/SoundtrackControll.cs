using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackControll : MonoBehaviour {

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
