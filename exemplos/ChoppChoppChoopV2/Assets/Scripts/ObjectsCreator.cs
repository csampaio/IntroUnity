using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCreator : MonoBehaviour {

	public GameObject[] builds;
	public GameObject[] peoples;
	public GameObject[] Enemies;
	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnObject(peoples[0], 2f));
		StartCoroutine(SpawnObject(builds[0], 2.5f));
		StartCoroutine(SpawnObject(builds[1], 3f));
		StartCoroutine(SpawnObject(builds[2], 4f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator SpawnObject(GameObject obj, float delay) {
		Instantiate (obj, spawnPoint.position, Quaternion.identity, this.transform);
		yield return new WaitForSeconds (delay);
		StartCoroutine (SpawnObject(obj, delay));
	}
}
