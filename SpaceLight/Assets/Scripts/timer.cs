using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class timer : MonoBehaviour {
    public Text timerText;
    public float startTime;
    private float endTime;
    private bool finished = false;
    public GameObject endLevel;
    public bool countingStarted = false;
    public float healthLeft;
	// Use this for initialization
	void Start () {
        timerText.text = "";
	}
    public void startCounting(){
        Analytics.CustomEvent("Counting Started");
        FindObjectOfType<PlayerControls>().gameOver = false;
        startTime = Time.time;
        endTime = startTime + 5 * 60-1;
        countingStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!countingStarted) return;
        if (finished) return;
        float t = endTime - Time.time;
        if(t <= 0){
            finished = true;
            t = 0;
            timerText.text = "0:0";
            timerText.color = Color.red;
            endLevel.SetActive(true);
            if(!FindObjectOfType<PlayerControls>().gameOver){
                healthLeft = FindObjectOfType<PlayerHealth>().healthPercentage;
                Analytics.CustomEvent("Game Lost", new Dictionary<string, object>
                {
                    { "timeLeft", 5*60-(Time.time-startTime) },
                    { "healthLeft", healthLeft },
                    {"packsCollected", FindObjectOfType<PlayerControls>().packsCollected}
                });
                FindObjectOfType<PlayerControls>().gameOver = true;
            }

            return;
        }
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");
        if(seconds.Length == 1){
            seconds = "0" + seconds;
        }
        timerText.text = minutes + ":" + seconds;
	}

}
