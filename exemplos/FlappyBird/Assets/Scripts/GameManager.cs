using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private PipesManager pipesManager;
    public GameObject backgroundObj;

    void Start()
    {
        pipesManager = FindObjectOfType<PipesManager>();
    }

    public void PlayerIsDead()
    {
        pipesManager.StopAllScroll();
        backgroundObj.BroadcastMessage(GameMessage.StopScroll);

    }
}
