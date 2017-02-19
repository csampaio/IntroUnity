using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringPipe : MonoBehaviour {

    private ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.AddScore(1);
        }
    }
}
