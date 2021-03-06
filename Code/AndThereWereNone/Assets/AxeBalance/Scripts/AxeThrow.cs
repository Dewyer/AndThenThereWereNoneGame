﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AxeThrow : MonoBehaviour
{

    public float degs = 5f;

    public float Speed = 5;
    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Speed;
    }
	
	// Update is called once per frame
	void Update ()
	{
        transform.Rotate(new Vector3(0,0, degs));
	}

    public IEnumerator Navigate()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("TextScene");
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("asds");
        Camera.main.gameObject.GetComponent<Animator>().SetBool("Blooding",true);
        StartCoroutine(Navigate());
    }
}
