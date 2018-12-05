using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ShipEntryControl : MonoBehaviour {
    public GameObject endLevel;
    public float startTime;
    public float healthLeft;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startTime = FindObjectOfType<timer>().startTime;
            healthLeft = FindObjectOfType<PlayerHealth>().healthPercentage;
            endLevel.SetActive(true);

            Analytics.CustomEvent("Game Won", new Dictionary<string, object>
            {
                { "timeLeft", 5*60-(Time.time-startTime) },
                { "healthLeft", healthLeft },
                {"packsCollected", FindObjectOfType<PlayerControls>().packsCollected}
            });
            FindObjectOfType<PlayerControls>().gameOver = true;
        }
    }
}
