using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmmoBox : MonoBehaviour {
    public int ammoNumber = 10;
    public AudioSource gotSound;

    public event EventHandler ReloadEvent;

    public class ReloadArgs: EventArgs
    {
        public int ammoNumber { get; set; }
    }

    public void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        GunController[] guns = player.GetComponentsInChildren<GunController>();
        foreach( GunController gun in guns)
        {
            ReloadEvent += gun.ReloadGun;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            GotAmmo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            GotAmmo();
    }

    private void GotAmmo()
    {
        if (gotSound != null && !gotSound.isPlaying)
            gotSound.Play();

        if (ReloadEvent != null)
        {
            ReloadArgs args = new ReloadArgs();
            args.ammoNumber = ammoNumber;
            ReloadEvent(gameObject, args);
        }
        gameObject.SetActive(false);
        DestroyObject(gameObject, 2f);
    }
}
