﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetector : MonoBehaviour {

    public GameObject endLevel;
    public PlayerControls player;
    public PackCollected packTracker;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (player.packsCollected == packTracker.packsNeeded)
        {
            endLevel.SetActive(true);
        }
	}

}
