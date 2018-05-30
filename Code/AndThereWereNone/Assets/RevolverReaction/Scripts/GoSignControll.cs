using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSignControll : MonoBehaviour {

    public Color NoGoColor;
    public Color GoColor;

    public enum GoState { Hiden, NotGo, Go };
    private GoState _inState;
    public GoState InState
    {
        get
        {
            return _inState;
        }

        set
        {
            _inState = value;
            StateChanged();
        }
    }

    public void StateChanged()
    {
        if (InState == GoState.Hiden)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0f);
        }
        else if (InState == GoState.NotGo)
        {
            gameObject.GetComponent<SpriteRenderer>().color = NoGoColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = GoColor;
        }
    }

	// Use this for initialization
	void Start () {
        InState = GoState.Hiden;
	}
	
}
