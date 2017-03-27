using System;
using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject explosionPrefab;

    public event EventHandler BulletHits;
    private new Collider2D collider;
    private bool hit = false;
    public int hitPoints = 1;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        int layerMask = LayerMask.GetMask("Default","Enemies","Peoples");
        if ( collider.IsTouchingLayers(layerMask) && !hit)
        {
            hit = true;
            StartCoroutine(Kaboom());
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Limits"))
        {
            if (BulletHits != null)
                BulletHits(gameObject, EventArgs.Empty);
        }
    }

    IEnumerator Kaboom()
    {
        Rigidbody2D rgbody = GetComponent<Rigidbody2D>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        rgbody.bodyType = RigidbodyType2D.Kinematic;
        rgbody.velocity = Vector2.zero;   
        renderer.enabled = false;
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform, false);
        
        yield return new WaitForSeconds(2);
        rgbody.bodyType = RigidbodyType2D.Dynamic;
        renderer.enabled = true;
        
        if (BulletHits != null)
            BulletHits(gameObject, EventArgs.Empty);

    }
}
