using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

    public Text bulletsCounter;
    public Text bombCounter;
    public Text scoreCounter;
    public Text rescuesCounter;

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        GunController[] guns = player.GetComponentsInChildren<GunController>();	
        foreach (GunController gun in guns)
        {
            gun.GunShoot += UpdateBulletsCounter;
        }
	}
	
	
    private void UpdateBulletsCounter(object sender, EventArgs args)
    {
        GameObject gun = sender as GameObject;
        GunController.GunEventArg gunArgs = args as GunController.GunEventArg;
        string counterText = gunArgs.numBullets + "/" + gunArgs.totalBullets;

        if (gun.name.Equals("BombLauncher"))
        {
            bombCounter.text = "Bombs: " + counterText;
        } else if (gun.name.Equals("MachineGun"))
        {

            bulletsCounter.text = "Bullets: " + counterText;
        }
    }
}
