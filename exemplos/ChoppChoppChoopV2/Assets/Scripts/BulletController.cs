using System;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public event EventHandler BulletHits;
    private new Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        EventHandler handle = BulletHits;
        int layerMask = LayerMask.GetMask("Default");
        if ( collider.IsTouchingLayers(layerMask) && handle != null)
        {
            handle(gameObject, EventArgs.Empty);
        }
	}
}
