using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

    public Text bulletsText;
    public Text bombsText;
    public Text scoreText;
    public Text rescuesText;
    public Text killedText;

    private int killCounter = 0;
    private int rescueCounter;

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
            bombsText.text = counterText;
        } else if (gun.name.Equals("MachineGun"))
        {

            bulletsText.text = counterText;
        }
    }

    public void UpdateRescueCounter(object sender, EventArgs args)
    {
        PeopleController.PeopleArgs peopleArgs = args as PeopleController.PeopleArgs;
        if (peopleArgs.isDead)
        {
            killCounter++;
            killedText.text = killCounter.ToString();
        } else
        {
            rescueCounter++;
            rescuesText.text = rescueCounter.ToString();
        }

        StartCoroutine(DestroyPeople(sender as GameObject));
    }

    private IEnumerator DestroyPeople(GameObject people)
    {
        yield return new WaitForSeconds(2);
        people.SetActive(false);
        DestroyImmediate(people);
    }
}
