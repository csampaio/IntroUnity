using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    private float pixelToUnit = 40f;
    public float maxVelocity = 80f; // pixel/seconds

    public Vector3 moveSpeed = Vector3.zero;

    [Header("Components")]
    public SpriteRenderer spriterenderer;
    public Animator animCtrl;
    public GameObject gunPrefab;
    private Rigidbody2D rigidbody2D;
    public Transform gunPosition;


    [Header("Animation")]
    public bool isFacingLeft = false;
    public bool isRunning = false;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animCtrl = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAnimatorParameters();
        HandleHorizontalMoviment();
        HandleVerticalMoviment();
        MoveCharacterController();
        CharacterActionController();
	}

    void UpdateAnimatorParameters()
    {
        animCtrl.SetBool("Running", isRunning);
    }

    void HandleHorizontalMoviment()
    {
        moveSpeed.x = Input.GetAxis("Horizontal") * (maxVelocity / pixelToUnit);
        if (moveSpeed.x != 0)
        {
            isRunning = true;
        } else
        {
            isRunning = false;
        }

        Vector2 pos = gunPosition.localPosition;
        if (Input.GetAxis("Horizontal") < 0 && !isFacingLeft)
        {
            isFacingLeft = true;            
            pos.x *= -1;
        } else if (Input.GetAxis("Horizontal") > 0 && isFacingLeft)
        {
            pos.x *= -1;
            isFacingLeft = false;
        }
        gunPosition.localPosition = pos;

        spriterenderer.flipX = isFacingLeft;
    }

    void HandleVerticalMoviment()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody2D.AddForce(Vector2.up * 200, ForceMode2D.Impulse);
        }
    }

    void MoveCharacterController()
    {
        rigidbody2D.velocity = moveSpeed;
    }

    void CharacterActionController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animCtrl.SetTrigger("Fire");
            animCtrl.SetBool("Shooting",true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animCtrl.SetBool("Shooting", false);
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(gunPrefab) as GameObject;
        bullet.GetComponent<BulletController>().isLeft = isFacingLeft;
        bullet.transform.position = gunPosition.position;
    }

}
