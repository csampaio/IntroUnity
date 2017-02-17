using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private Animator animCtrl;
    private Rigidbody2D rgbody;
    public bool isLeft = false;
    // Use this for initialization
    void Start () {
        animCtrl = GetComponent<Animator>();
        rgbody = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().flipX = isLeft;
    }

    void FixedUpdate()
    {
        Vector2 force = Vector2.right * 20 * Time.deltaTime;
        if (isLeft)
        {
            force *= -1;
        }
        rgbody.AddForce(force, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        animCtrl.SetTrigger("Destroy");
    }

    public void DestroyMe()
    {
        DestroyImmediate(this.gameObject);
    }


}
