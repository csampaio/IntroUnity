using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDestroyer : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        DestroyObj(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        DestroyObj(collision.gameObject);
    }

    private void DestroyObj(GameObject obj)
    {
        string layerName = LayerMask.LayerToName(obj.layer);
        if (!(layerName.Equals("Bullet") || layerName.Equals("EnemyBullet")))
        {
            Destroy(obj.gameObject);
        }
    }

}
