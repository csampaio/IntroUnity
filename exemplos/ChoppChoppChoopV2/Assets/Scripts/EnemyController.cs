using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ScrollObjects {

    private Animator animator;
    private bool isMoving = true;
    private bool isAttacking = false;
    public GameObject bulletPrefab;
    public Transform cannonPosition;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        StartCoroutine(StartShoot());
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
        GameObject bullet = Instantiate(bulletPrefab, cannonPosition.position, cannonPosition.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bullet.SetActive(true);
        bulletRb.AddRelativeForce(Vector2.left * 150);
    }

    private IEnumerator StartShoot()
    {
        float delay = Random.Range(2f, 10f);
        isAttacking = true;
        Shoot();
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        StartCoroutine(StartShoot());
    }

}
