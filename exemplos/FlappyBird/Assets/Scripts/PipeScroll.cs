using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScroll : MonoBehaviour {

    public float pipeVelocity;
    private bool isStopped = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isStopped)
            transform.position += Vector3.left * pipeVelocity * Time.deltaTime;	
	}

    void StopScroll()
    {
        isStopped = true;
    }
}
