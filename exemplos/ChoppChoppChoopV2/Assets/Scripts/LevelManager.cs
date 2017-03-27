using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager: MonoBehaviour {

    private bool isMenu = false;
	public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnEnable()
    {
        isMenu = SceneManager.GetActiveScene().name.Equals("Menu");
    }

    public void Update()
    {
        if (Input.anyKeyDown && isMenu)
        {
            StartGame();
        }
    }

}
