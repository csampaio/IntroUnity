using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ScrollObjects {

    private Animator animator;
    private bool isMoving = true;
    private bool isAttacking = false;
    private GunController gun;
    public Transform cannonPosition;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        StartCoroutine(StartShoot());
        gun = GetComponentInChildren<GunController>();
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
        if (gun != null)
            gun.FireGun();
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
