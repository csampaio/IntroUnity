using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesManager : MonoBehaviour {
    [Header("References")]
    public GameObject pipePrefab;

    [Header("Pipes")]
    public int maxPipes;
    public float pipesOffset;
    public float pipesStartY = 0.2f;
    public float pipesEndY = 0.7f;
    private float worldOffset;

    private Transform[] pipes;
    private int firstPipeIndex;

	// Use this for initialization
	void Start () {
        pipes = new Transform[maxPipes];

        Vector3 offsetPos = new Vector3(pipesOffset, 0, 0);
        Vector3 refPos = Vector3.zero;
        worldOffset = (Camera.main.ViewportToWorldPoint(offsetPos) - Camera.main.ViewportToWorldPoint(refPos)).x;

        Vector3 pipePosition;
        for (int i = 0; i < maxPipes; i++)
        {
            pipePosition = Camera.main.ViewportToWorldPoint(new Vector3(
                1f + pipesOffset * i, 
                Random.Range(pipesStartY,pipesEndY)
                ));
            pipePosition.z = 0f;
            pipes[i] = Instantiate(pipePrefab.transform) as Transform;
            pipes[i].parent = this.transform;
            pipes[i].position = pipePosition;
        }
        firstPipeIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(pipes[firstPipeIndex].position);
        
        if (screenPos.x < -0.5f)
        {
            int lastPipeIndex = (pipes.Length + firstPipeIndex - 1) % pipes.Length;
            pipes[firstPipeIndex].localPosition = pipes[lastPipeIndex].localPosition + new Vector3(worldOffset, 0, 0);
            Vector3 pipePos = pipes[firstPipeIndex].localPosition;
            pipePos.y = Camera.main.ViewportToWorldPoint(new Vector3(
                0, 
                Random.Range(pipesStartY, pipesEndY), 
                0
                )).y;
            pipes[firstPipeIndex].position = pipePos;

            firstPipeIndex = (firstPipeIndex + 1) % pipes.Length;
        }
    }
}
