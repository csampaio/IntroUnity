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

    private int verticalDirection = 0;

    private float fixedXPos;
    private BoxCollider2D collider2d;
    private new SpriteRenderer renderer;
    private bool isDead = false;

    public event EventHandler PlayerDied;

	void Start () {
        fixedXPos = transform.position.x;
        collider2d = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
	}
	

	void Update () {
        if (!isDead)
        {
            HandleHorizontalMoviment();
            HandleVerticalMoviment();
        }
        HandleGravity();
    }

	void HandleHorizontalMoviment() {
#if UNITY_ANDROID || UNITY_IOS
        float h = 0;
        if (Input.acceleration.x > 0.15 || Input.acceleration.x < -0.15)
        {
            h = Input.acceleration.x;
        }
        horizontalSpeed = h * velocity.x;
#else
        horizontalSpeed = Input.GetAxis ("Horizontal") * velocity.x;
#endif
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
#if UNITY_ANDROID || UNITY_IOS
        float verticalSpeed = verticalDirection * velocity.y;
#else
        float verticalSpeed = Input.GetAxis ("Vertical") * velocity.y;
#endif

        transform.Translate (Vector2.up * verticalSpeed * Time.deltaTime);
	}

    public void MoveVertically(int direction)
    {
        verticalDirection = direction;
    }

	void HandleGravity() {
		float g = GRAVITY;
		if (horizontalSpeed != 0) {
			g *= DELTA_GRAVITY;
		}
		transform.Translate (Vector2.down * g * Time.deltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BlackHankIsDown();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlackHankIsDown();
    }

    private void BlackHankIsDown()
    {
        int layerMask = LayerMask.GetMask("EnemyBullet", "Enemies");
        if (collider2d.IsTouchingLayers(layerMask) && !isDead)
        {
            GetComponent<AudioSource>().Stop();
            isDead = true;
            renderer.enabled = false;
            collider2d.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            horizontalSpeed = 0;
            GunController[] guns = GetComponentsInChildren<GunController>();
            foreach (GunController gun in guns)
            {
                gun.gameObject.SetActive(false);
            }
            Instantiate(explosionPrefab, transform, false);
            Invoke("DeathEvent", 2);
        }
    }

    private void DeathEvent()
    {
        if (PlayerDied != null)
        {
            PlayerDied(gameObject, EventArgs.Empty);
        }
    }
    

}
