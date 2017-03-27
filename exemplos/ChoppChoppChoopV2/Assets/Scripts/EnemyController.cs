using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : ScrollObjects {

    private Animator animator;
    private bool isMoving = true;
    private bool isAttacking = false;
    private GunController gun;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider2d;
    private new SpriteRenderer renderer;
    private bool isDead = false;
    private float deltaLife;
    public int life;
    public Transform cannonPosition;
    public GameObject explosionPrefab;
    public GameObject lifeCounter;

    public event EventHandler EnemyDestroyed;


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        StartCoroutine(StartShoot());
        gun = GetComponentInChildren<GunController>();
        collider2d = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        deltaLife = lifeCounter.transform.localScale.x / life;
    }


    protected override void ScrollByPlayerMoviment()
    {
        animator.SetBool("moving", isMoving);
        animator.SetBool("attacking", isAttacking);

        base.ScrollByPlayerMoviment();
        if (isMoving)
        { 
            transform.position += Vector3.left * Time.deltaTime/2;
        }
    }

    private void Shoot()
    {
        if (gun != null && !isDead)
            gun.FireGun();
    }

    private IEnumerator StartShoot()
    {
        float delay = UnityEngine.Random.Range(1f, 5f);
        isAttacking = true;
        Shoot();
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        StartCoroutine(StartShoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (layerName.Equals("Bomb") || layerName.Equals("Bullet") && !isDead)
        {
            BulletController bulletController = collision.GetComponent<BulletController>();
            if (bulletController != null)
            {
                int bulletHitPoint = bulletController.hitPoints;
                life -= bulletHitPoint;
            }
            life = Math.Max(0, life);
            Vector3 scale = new Vector3(life * deltaLife, lifeCounter.transform.localScale.y);
            lifeCounter.transform.localScale = scale;
            if (life <= 0)
            {
                isDead = true;
                StartCoroutine(Kaboon());
            }
            
        }
        
    }

    private IEnumerator Kaboon()
    {
        isMoving = false;
        isAttacking = false;
        renderer.enabled = false;
        collider2d.enabled = false;
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform, false);
        if (EnemyDestroyed != null)
            EnemyDestroyed(gameObject, EventArgs.Empty);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
