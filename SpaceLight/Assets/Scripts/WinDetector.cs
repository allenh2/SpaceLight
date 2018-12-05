using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetector : MonoBehaviour {

    public GameObject endLevel;
    public PlayerControls player;
    public PackCollected packTracker;
    public GameObject shipEntry;
    private Dialogue dialogue;
    public Animator animator;
    // Use this for initialization
    void Start () {
        shipEntry = GameObject.FindWithTag("Goal");
        //dialogue.name = "Captain";
        //dialogue.sentences = new string[] { "Congratulations on collecting all energy packs! Now return to the ship! ","Hurry Up!! We have to leave this planet!!" };
	}

    // Update is called once per frame
    void Update() {
       

        if (player.packsCollected == packTracker.packsNeeded)
        {
            FindObjectOfType<DialogueManager>().ReopenDialogueBox();
            shipEntry.SetActive(true);
            //endLevel.SetActive(true);
        }
	}

}
