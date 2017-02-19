using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    private float pixelToUnit = 40f;
    public float maxVelocity = 80f; 
    public Vector3 moveSpeed = Vector3.zero;
    public float jumpForce = 250;


    [Header("Components")]
    private SpriteRenderer spriterenderer;
    private Animator animCtrl;
    public GameObject bulletPrefab;
    private Rigidbody2D rigidbody2D;
    public Transform gunPosition;
    public Transform groundCheck;


    [Header("Animation")]
    public bool isFacingLeft = false;
    public bool isRunning = false;
    public bool isGrounded = false;
    public bool isFalling = false;
    public bool setJumpTrigger = false;
    public bool setFallTrigger = false;
    public bool setLandTrigger = false;

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
        animCtrl.SetBool("isRunning", isRunning);
        if (setJumpTrigger)
        {
            animCtrl.SetTrigger("Jump");
            setJumpTrigger = false;
        }
        else
        {
            animCtrl.ResetTrigger("Jump");
        }

        if (setFallTrigger)
        {
            animCtrl.SetTrigger("Fall");
            setFallTrigger = false;
        }
        else
        {
            animCtrl.ResetTrigger("Fall");
        }

        if (setLandTrigger)
        {
            animCtrl.SetTrigger("Land");
            setLandTrigger = false;
        }
        else
        {
            animCtrl.ResetTrigger("Land");
        }
    }

    void HandleHorizontalMoviment()
    {
        moveSpeed.x = Input.GetAxis("Horizontal") * (maxVelocity / pixelToUnit);
        if (RaycastAgainstLayer("Ground", groundCheck))
        {
            isGrounded = true;
            if (moveSpeed.x != 0)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
        }
        else
        {
            isGrounded = false;
            isRunning = false;
        }

        Vector2 pos = gunPosition.localPosition;
        if (Input.GetAxis("Horizontal") < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            pos.x *= -1;
        }
        else if (Input.GetAxis("Horizontal") > 0 && isFacingLeft)
        {
            pos.x *= -1;
            isFacingLeft = false;
        }
        gunPosition.localPosition = pos;

        spriterenderer.flipX = isFacingLeft;
        
    }

    void HandleVerticalMoviment()
    {
        moveSpeed.y = rigidbody2D.velocity.y;
        if (isGrounded)
        {
            if (isFalling)
            {
                setLandTrigger = true;
                isFalling = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce);
                setJumpTrigger = true;
                isGrounded = false;
            }
        }
        else
        {
            if (moveSpeed.y > 0 && Input.GetKeyUp(KeyCode.Space))
            {
                moveSpeed.y = 0;
            }
            if (moveSpeed.y < 0 && !isFalling)
            {
                isFalling = true;
                setFallTrigger = true;
            }
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
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.GetComponent<BulletController>().isLeft = isFacingLeft;
        bullet.transform.position = gunPosition.position;
    }

    RaycastHit2D RaycastAgainstLayer(string layerName, Transform endPoint)
    {
        int layer = LayerMask.NameToLayer(layerName);
        int layerMask = 1 << layer; // camada 1 = 100, camada 4 = 10000
        // layer - 0000 0000 0000 0000 0000 0000 0000 1001
        // Filtrar as layer 1 e 4
        // camdas 2 e 4 = (1 << 2) + ( 1 << 4) = 100 + 10000 = 10100

        Vector2 originPosition = new Vector2(transform.position.x, transform.position.y);
        float rayLength = Mathf.Abs(endPoint.localPosition.y);
        Vector2 direction = endPoint.localPosition.normalized;
        RaycastHit2D hit2d = Physics2D.Raycast(originPosition, direction, rayLength, layerMask);

#if UNITY_EDITOR
        Color color;
        if (hit2d.collider != null)
        {
            color = Color.green;
        }
        else
        {
            color = Color.red;
        }

        Debug.DrawLine(originPosition, originPosition + direction * rayLength, color, 0f, false);
#endif


        return hit2d;
    }

}
