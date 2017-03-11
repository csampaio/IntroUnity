using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public GameObject explosionCenter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        		
	}


    public void Kabommm()
    {
        explosionCenter.GetComponent<Rigidbody>().AddExplosionForce(200, explosionCenter.transform.position, 30);
    }
}
