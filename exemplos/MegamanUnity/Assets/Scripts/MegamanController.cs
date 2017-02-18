using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour {

    [Header("Scene Reference")]
    public Transform groundCheck;

    [Header("Movement")]
    private float pixelToUnit = 40f;
    public float maxVelocity = 80f; // pixel/seconds

    public Vector3 moveSpeed = Vector3.zero;
    public float jumpForce = 400f;

    [Header("Components")]
    private SpriteRenderer spriterenderer;
    private Animator animator;
    private Rigidbody2D rigidbody2D;

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
        if (setJumpTrigger)
        {
            animator.SetTrigger("Jump");
            setJumpTrigger = false;
        } else
        {
            animator.ResetTrigger("Jump");
        }

        if (setFallTrigger)
        {
            animator.SetTrigger("Fall");
            setFallTrigger = false;
        }
        else
        {
            animator.ResetTrigger("Fall");
        }

        if (setLandTrigger)
        {
            animator.SetTrigger("Land");
            setLandTrigger = false;
        }
        else
        {
            animator.ResetTrigger("Land");
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
        moveSpeed.y = rigidbody2D.velocity.y;
        if (isGrounded)
        {
            if (isFalling)
            {
                setLandTrigger = true;
                isFalling = false;
            } else if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce);
                setJumpTrigger = true;
                isGrounded = false;
            }
        } else
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

    // Metodo para traçar um raio na direção do obj de referencia
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
        } else
        {
            color = Color.red;
        }

        Debug.DrawLine(originPosition, originPosition + direction * rayLength, color, 0f, false);
#endif


        return hit2d;
    }
}
