using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGetter : MonoBehaviour
{

    public GameObject particle;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FallingItem")
        {
            GameObject.FindGameObjectWithTag("Controll").SendMessage("GotDroplet", col.gameObject);
            GameObject.Instantiate(particle, col.gameObject.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }
}
