using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour {

    private Animator anim;
    private Rigidbody2D rb;
    public float flappyVelocity = 300;
    private bool isDead = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 startPos = Camera.main.ViewportToWorldPoint(new Vector3(0.2f, 0.8f));
        startPos.z = transform.position.z;
        transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            return;
        }
		if (Input.GetKeyDown(KeyCode.Space))
        {
            FlyBirdFly();
        }
        Vector3 angles = transform.eulerAngles;
        angles.z = Mathf.Clamp(rb.velocity.y * 4f, -90f, 45f);
        transform.eulerAngles = angles;
	}

    void FlyBirdFly()
    {
        anim.SetTrigger("Flappy");
        rb.velocity = Vector2.up * flappyVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pipe"))
        {
            isDead = true;
            Time.timeScale = 0;
        }
    }
}
