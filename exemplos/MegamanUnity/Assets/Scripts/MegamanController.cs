using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour {
    [Header("Movement")]
    private float pixelToUnit = 40f;
    public float maxVelocity = 80f; // pixel/seconds

    public Vector3 moveSpeed = Vector3.zero;

    [Header("Components")]
    public SpriteRenderer spriterenderer;
    public Animator animator;
    private Rigidbody2D rigidbody2D;

    [Header("Animation")]
    public bool isFacingLeft = false;
    public bool isRunning = false;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAnimatorParameters();
        HandleHorizontalMoviment();
        HandleVerticalMoviment();
        MoveCharacterController();
	}

    void UpdateAnimatorParameters()
    {
        animator.SetBool("isRunning", isRunning);
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

        if (Input.GetAxis("Horizontal") < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
        } else if (Input.GetAxis("Horizontal") > 0 && isFacingLeft)
        {
            isFacingLeft = false;
        }

        spriterenderer.flipX = isFacingLeft;
    }

    void HandleVerticalMoviment()
    {

    }

    void MoveCharacterController()
    {
        rigidbody2D.velocity = moveSpeed;
    }
}
