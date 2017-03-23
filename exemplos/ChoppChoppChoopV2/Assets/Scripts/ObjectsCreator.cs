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
		StartCoroutine(RandonSpawn(peoples, 2f, 5f));
        StartCoroutine(RandonSpawn(builds, 3f,7f));
        StartCoroutine(RandonSpawn(Enemies, 2f, 10f));
    }
	

    private IEnumerator RandonSpawn(GameObject[] prefabs, float minDelay, float maxDelay)
    {
        int i = Random.Range(0, prefabs.Length);
        GameObject item = prefabs[i];
        if (player.horizontalSpeed > 0)
        {
            Instantiate(item, spawnPoint.position, Quaternion.identity, this.transform);
        }
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        StartCoroutine(RandonSpawn(prefabs, maxDelay, minDelay));
    }
}
