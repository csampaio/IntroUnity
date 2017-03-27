using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PeopleController : MonoBehaviour {

    private new BoxCollider2D collider;
    private new SpriteRenderer renderer;
    private new Rigidbody2D rigidbody;
    private new Animation animation;
    private GameManager manager;

    public AudioSource rescueSound;
    public AudioSource deadSound;

    public event EventHandler PeopleHit;

    public class PeopleArgs: EventArgs
    {
        public bool isDead { get; set; }
    }

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animation>();
        manager = FindObjectOfType<GameManager>();
        PeopleHit += manager.UpdateRescueCounter;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerMask = LayerMask.GetMask("Player", "Enemies", "Bullet", "EnemyBullet", "Bomb");

        if (collider.IsTouchingLayers(layerMask))
        {
            AudioSource sound = rescueSound;
            PeopleArgs args = new PeopleArgs();
            args.isDead = false;
            layerMask = LayerMask.GetMask("Enemies", "Bullet", "EnemyBullet", "Bomb");

            if (collider.IsTouchingLayers(layerMask))
            {
                sound = deadSound;
                renderer.color = Color.red;
                args.isDead = true;
            }

            rigidbody.bodyType = RigidbodyType2D.Static;
            collider.enabled = false;
            animation.Play("blink_people");

            if (sound != null && !sound.isPlaying)
            {
                sound.Play();
            }

            EventHandler handle = PeopleHit;
            if (handle != null)
            {

                handle(gameObject, args);
            }
        }
    }

}
