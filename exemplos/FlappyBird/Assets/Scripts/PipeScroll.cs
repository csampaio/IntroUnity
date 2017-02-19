using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScroll : MonoBehaviour {

    public float pipeVelocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.left * pipeVelocity * Time.deltaTime;	
	}
}
