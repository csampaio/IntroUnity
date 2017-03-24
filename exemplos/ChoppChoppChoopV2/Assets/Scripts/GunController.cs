﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunController : MonoBehaviour {

    [Header("Gun Config")]
    public int maxBullets = 10;
    public Transform gunPosition;
    public GameObject bulletsPrefab;
    public KeyCode fireKey;
    public int bulletSpeed = 150;
    public float gunCoolDown = 1;

    private Queue<GameObject> bulletsLoaded;
    private List<GameObject> bulletsShooted;
    private float cooldown = 0;

    public event EventHandler GunShoot;

    public class GunEventArg: EventArgs
    {
        public int numBullets { get; set; }
        public int totalBullets { get; set; } 
    }

    void Start()
    {
        bulletsLoaded = new Queue<GameObject>(maxBullets);
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Instantiate(bulletsPrefab, transform.position, transform.rotation);
            bullet.transform.parent = transform;
            bullet.GetComponent<BulletController>().BulletHits += ResetBullet;
            bulletsLoaded.Enqueue(bullet);
        }
        bulletsShooted = new List<GameObject>(maxBullets);
        ShootEvent();
    }

    void Update () {
        if (Input.GetKey(fireKey))
            FireGun();
	}

    public void FireGun()
    {
        
        if (bulletsLoaded.Count > 0 && cooldown <= Time.time)
        {
            GameObject bullet = bulletsLoaded.Dequeue();
            Rigidbody2D bulletRgb = bullet.GetComponent<Rigidbody2D>();
            bullet.gameObject.SetActive(true);
            bulletRgb.AddRelativeForce(Vector2.right * bulletSpeed);
            bullet.transform.parent = null;
            bulletsShooted.Add(bullet);
            ShootEvent();
            cooldown = Time.time + gunCoolDown;
        }
    }

    void ShootEvent()
    {
        EventHandler handle = GunShoot;
        if (handle != null)
        {
            GunEventArg args = new GunEventArg();
            args.totalBullets = maxBullets;
            args.numBullets = bulletsLoaded.Count;
            handle(gameObject, args);
        }
    }

    private void ResetBullet(object sender, EventArgs args)
    {
        GameObject bullet = sender as GameObject;
        bullet.SetActive(false);
        bullet.transform.position = gunPosition.position;
        bullet.transform.parent = gunPosition;
        bulletsShooted.Remove(bullet);
        bulletsLoaded.Enqueue(bullet);
    }
}