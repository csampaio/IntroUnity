using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour {
    private Vector3 camPos;
    private Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
        camPos = mainCamera.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 newPos = new Vector3(mainCamera.transform.position.x, camPos.y, camPos.z);
        mainCamera.transform.position = newPos;
	}
}
