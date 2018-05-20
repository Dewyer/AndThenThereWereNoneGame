using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillMotor : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    startPos = gameObject.transform.position;
	    rotation = gameObject.transform.rotation;

	}

    public Vector3 startPos;

    public Quaternion rotation;

    //Mouse vars
    public int MouseButton = 1;
    public float MaxPower = 200f;
    public float maxDP = 6f;
    public bool OnDrag = false;
    public Vector3 StartDrag;
    public Vector3 EndDrag;

    public void OnDragLiftUp()
    {
        var delta = StartDrag - EndDrag;
        var power = (delta.magnitude >= maxDP ? maxDP : delta.magnitude)/maxDP * MaxPower;
        var forcer = delta.normalized * power;

        gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(forcer,gameObject.transform.position+new Vector3(0.0f,-0.2f,0));
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
    }

    public void MouseDragCheck()
    {
        var firstDown = Input.GetMouseButtonDown(MouseButton);
        if (firstDown && !OnDrag)
        {
            OnDrag = true;
            var mp = Input.mousePosition;
            StartDrag = Camera.main.ScreenToWorldPoint(mp);
        }

        var down = Input.GetMouseButton(MouseButton);
        if (down)
        {
            var mp = Input.mousePosition;
            EndDrag = Camera.main.ScreenToWorldPoint(mp);

        }

        if (Input.GetMouseButtonUp(1) && OnDrag)
        {
            OnDrag = false;
            OnDragLiftUp();
        }

    }

    public void ResetPlayer()
    {
        gameObject.transform.position = startPos;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.transform.rotation = rotation;
    }

    // Update is called once per frame
	void Update ()
	{
	    MouseDragCheck();

	}

    
}
