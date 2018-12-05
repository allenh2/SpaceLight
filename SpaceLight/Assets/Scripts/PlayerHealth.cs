using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
public class PlayerHealth : MonoBehaviour {

    private Image healthBar;
    private float maxHealth = 200f;
    public static float health;
    public float healthPercentage;
    public GameObject endLevel;
    public float startTime;
    // Use this for initialization
    void Start () {
        health = maxHealth;
        healthBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        healthPercentage = health / maxHealth;
        healthBar.fillAmount = healthPercentage;
        if (health <= 0) { //restart level
            //go to lose page
            endLevel.SetActive(true);
            if (!FindObjectOfType<PlayerControls>().gameOver){
                startTime = FindObjectOfType<timer>().startTime;
                Analytics.CustomEvent("Game Lost", new Dictionary<string, object>
                {
                    { "timeLeft", 5*60-(Time.time-startTime) },
                    { "healthLeft", healthPercentage },
                    {"packsCollected", FindObjectOfType<PlayerControls>().packsCollected}
                });
                FindObjectOfType<PlayerControls>().gameOver = true;
            }
        }
	}
}
