using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCreator : MonoBehaviour {

	public GameObject[] builds;
	public GameObject[] peoples;
	public GameObject[] Enemies;
	public Transform spawnPoint;

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
		StartCoroutine(SpawnObject(peoples[0], 2f, 5f));
        StartCoroutine(SpawnBuilds(3f,7f));
	}
	

	private IEnumerator SpawnObject(GameObject obj, float minDelay, float maxDelay) {
        if (player.horizontalSpeed > 0)
        {
            Instantiate(obj, spawnPoint.position, Quaternion.identity, this.transform);
        }
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds (delay);
		StartCoroutine (SpawnObject(obj, maxDelay, minDelay));
	}

    private IEnumerator SpawnBuilds(float minDelay, float maxDelay)
    {
        int i = Random.Range(0, builds.Length);
        GameObject build = builds[i];
        if (player.horizontalSpeed > 0)
        {
            Instantiate(build, spawnPoint.position, Quaternion.identity, this.transform);
        }
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnBuilds(maxDelay, minDelay));
    }
}
