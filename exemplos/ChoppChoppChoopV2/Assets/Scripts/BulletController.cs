using System;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public event EventHandler BulletHits;

    private new Collider2D collider2D;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

	void OnTriggerEnter2D(Collider2D other) {
        EventHandler handle = BulletHits;
        if ( other.IsTouching(collider2D) && handle != null)
        {
            handle(gameObject, EventArgs.Empty);
        }
	}
}
