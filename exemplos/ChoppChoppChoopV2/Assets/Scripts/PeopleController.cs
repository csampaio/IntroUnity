using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour {

    private new BoxCollider2D collider;
    private new SpriteRenderer renderer;
    private new Rigidbody2D rigidbody;
    private new Animation animation;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animation>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerMask = LayerMask.GetMask("Player", "Enemies", "Bullet");
        Debug.Log("Hit: " + collision.transform.name);
        if (collider.IsTouchingLayers(layerMask))
        {
            layerMask = LayerMask.GetMask("Enemies", "Bullet");
            if (collider.IsTouchingLayers(layerMask))
            {
                renderer.color = Color.red;
                rigidbody.bodyType = RigidbodyType2D.Static;
                collider.enabled = false;
                animation.Play("blink_people");
                Invoke("DestroyMe", 2f);
            }
        }
    }


    void DestroyMe()
    {
        DestroyImmediate(gameObject);
    }
}
