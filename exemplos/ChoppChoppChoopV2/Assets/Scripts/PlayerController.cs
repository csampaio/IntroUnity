using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    [Header("Chopp Config")]
	public Vector2 velocity;
	public float horizontalSpeed { get; set; }
    public GameObject explosionPrefab;

    private Queue<GameObject> bulletsLoaded;
	private List<GameObject> bulletsShooted;

	private const float ROTATE_VELOCITY = 30f;
	private const float GRAVITY = 0.2f;
	private const float DELTA_GRAVITY = 2f;

    private float fixedXPos;
    private BoxCollider2D collider2d;
    private new SpriteRenderer renderer;
    private bool isDead = false;

	void Start () {
        fixedXPos = transform.position.x;
        collider2d = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
	}
	

	void Update () { 
		HandleHorizontalMoviment ();
		HandleVerticalMoviment ();
        HandleGravity();
    }

	void HandleHorizontalMoviment() {
		horizontalSpeed = Input.GetAxis ("Horizontal") * velocity.x;
		float rotationSpeed = Time.deltaTime * velocity.x * ROTATE_VELOCITY;
		Vector3 rotationDir = Vector3.one;

		if (horizontalSpeed != 0 && Mathf.Abs(transform.rotation.z) < 0.3f) {
			if (horizontalSpeed > 0) {
				rotationDir = Vector3.back;
			} else if (horizontalSpeed < 0) {
				rotationDir = Vector3.forward;
			}
			transform.Rotate (rotationDir, rotationSpeed);
		} else if (transform.rotation.z != 0) {
			if (transform.rotation.z > 0) {
				rotationDir = Vector3.back;
			} else {
				rotationDir = Vector3.forward;
			}
            transform.Rotate (rotationDir, rotationSpeed);
		}
        transform.position = new Vector3(fixedXPos, transform.position.y);
	}

	void HandleVerticalMoviment() {
		float verticalSpeed = Input.GetAxis ("Vertical") * velocity.y;
		transform.Translate (Vector2.up * verticalSpeed * Time.deltaTime);
	}

	void HandleGravity() {
		float g = GRAVITY;
		if (horizontalSpeed != 0) {
			g *= DELTA_GRAVITY;
		}
		transform.Translate (Vector2.down * g * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerMask = LayerMask.GetMask("EnemyBullet");
        if (collider2d.IsTouchingLayers(layerMask) && !isDead)
        {
            isDead = true;
            renderer.enabled = false;
            Instantiate(explosionPrefab, transform, false);
        }
    }
    

}
