using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    [Header("Scene Reference")]
    public Material material;

    [Header("Velocity")]
    public Vector2 scrollVelocity;
	public Vector2 constantScroolingVelocity;

	[Header("Player")]
	public PlayerController player;

    private Vector2 scrollOffset;

	// Use this for initialization
	void Start () {
        scrollOffset = Vector2.zero;
        material.mainTextureOffset = scrollOffset;
	}
	
	// Update is called once per frame
	void Update () {
		scrollOffset += (scrollVelocity * player.horizontalSpeed + constantScroolingVelocity) * Time.deltaTime;
		material.mainTextureOffset = scrollOffset;
	}
}
