using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private Text text;
    private int score = 0;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int points)
    {
        score += points;
        text.text = score.ToString();
    }
}
