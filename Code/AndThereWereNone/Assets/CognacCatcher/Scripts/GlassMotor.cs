using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassMotor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    KeyboardControll();	
	}

    public float Speed;

    public void KeyboardControll()
    {
        bool dPressed = Input.GetKey(KeyCode.D);
        bool aPressed = Input.GetKey(KeyCode.A);

        if (dPressed || aPressed)
        {
            Vector3 tempVect = Vector3.zero;
            tempVect += dPressed ? Vector3.zero : Vector3.left;
            tempVect += aPressed ? Vector3.zero : Vector3.right;
            
            tempVect = tempVect.normalized * Speed * Time.deltaTime;
            gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + tempVect);
        }
    }
}
