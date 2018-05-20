using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchKiller : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="FallingItem")
            Destroy(col.gameObject);
    }

}
