using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public Vector2 velocity;
	public float horizontalSpeed { get; set; }
	[Header("MachineGun Config")]
	public int maxMachineGunBullets = 10;
	public Transform machineGun;
	public GameObject bulletsPrefab;
	private Queue<GameObject> bulletsLoaded;
	private List<GameObject> bulletsShooted;

	private const float ROTATE_VELOCITY = 30f;
	private const int BULLET_SPEED = 150;
	private const float GRAVITY = 0.2f;
	private const float DELTA_GRAVITY = 2f;

	void Start () {
		bulletsLoaded = new Queue<GameObject> (maxMachineGunBullets);
		for (int i = 0; i < maxMachineGunBullets; i++) {
            GameObject bullet = Instantiate(bulletsPrefab, machineGun.position, Quaternion.identity, machineGun.transform);
            bullet.GetComponent<BulletController>().BulletHits += ResetBullet;
            bulletsLoaded.Enqueue(bullet);
            
        }
        bulletsShooted = new List<GameObject>(maxMachineGunBullets);
	}
	

	void Update () {
		HandleGravity ();
		HandleHorizontalMoviment ();
		HandleVerticalMoviment ();
		FireMachineGun ();
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

	void FireMachineGun() {
		if (bulletsLoaded.Count > 0 && Input.GetKey(KeyCode.Space)) {
			GameObject bullet = bulletsLoaded.Dequeue ();
			Rigidbody2D bulletRgb = bullet.GetComponent<Rigidbody2D> ();
			bullet.gameObject.SetActive (true);
			bulletRgb.AddRelativeForce ( Vector2.right * BULLET_SPEED);
			bullet.transform.parent = null;
			bulletsShooted.Add (bullet);

		}
	}

	
	private void ResetBullet (object sender, EventArgs args) {
        GameObject bullet = sender as GameObject;
		bullet.SetActive(false);
		bullet.transform.position = machineGun.position;
		bullet.transform.parent = machineGun;
		bulletsShooted.Remove(bullet);
		bulletsLoaded.Enqueue(bullet);
	}
	
}
