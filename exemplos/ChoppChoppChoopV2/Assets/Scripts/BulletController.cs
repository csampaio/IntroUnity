using System;
using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject explosionPrefab;

    public event EventHandler BulletHits;
    private new Collider2D collider;
    private bool hit = false;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        int layerMask = LayerMask.GetMask("Default");
        if ( collider.IsTouchingLayers(layerMask) && !hit)
        {
            hit = true;
            StartCoroutine(Kaboom());
        }
	}

    IEnumerator Kaboom()
    {
        Rigidbody2D rgbody = GetComponent<Rigidbody2D>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        rgbody.bodyType = RigidbodyType2D.Static;
        renderer.enabled = false;
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform, false);
        
        yield return new WaitForSeconds(2);
        rgbody.bodyType = RigidbodyType2D.Kinematic;
        renderer.enabled = true;
        EventHandler handle = BulletHits;
        if (handle != null)
            handle(gameObject, EventArgs.Empty);

    }
}
