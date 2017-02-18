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
        RaycastAgainstLayer("Ground", groundCheck);

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
