using System;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public event EventHandler BulletHits;

	void OnTriggerEnter2D(Collider2D other) {
        EventHandler handle = BulletHits;
        int layerMask = LayerMask.GetMask("Default");
        if ( other.IsTouchingLayers(layerMask) && handle != null)
        {
            handle(gameObject, EventArgs.Empty);
        }
	}
}
