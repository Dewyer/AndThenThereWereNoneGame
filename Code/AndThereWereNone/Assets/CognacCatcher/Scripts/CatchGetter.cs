using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGetter : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FallingItem")
        {
            GameObject.FindGameObjectWithTag("Controll").SendMessage("GotDroplet", col.gameObject);
            Destroy(col.gameObject);
        }
    }
}
