using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObjects : MonoBehaviour {

	PlayerController player;

	void Start() {
		player = FindObjectOfType<PlayerController> ();
	}

	void Update () {
		if (player.horizontalSpeed != 0) {
			transform.position += Vector3.left * player.horizontalSpeed * Time.deltaTime * 3;
		}
	}

}