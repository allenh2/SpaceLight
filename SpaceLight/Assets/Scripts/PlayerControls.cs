﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour {
    [SerializeField]
    private float speed;

    private Vector2 direction;

    public GameObject projectile;
    public int packsCollected;
    private Rigidbody2D player;
    public GameObject goal;
    private bool isMoving;

    public int maxAmmo = 5;
    public Text ammoTracker;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Transform spriteMask;

    //Initial variables needed
    private Animator animator;
    private int STILL_ANIM = 0;
    private int WALK_ANIM = 1;
    private int SHOOT_ANIM = 2;
    private int curAnim = 0;
    private SpriteRenderer spriteRenderer;
    private bool gameStarted = false;
    public bool gameOver = false;
    public static AudioClip reloadSound;
    static AudioSource source;
    private bool soundPlayed;
    private bool cheatsOn;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        reloadSound = Resources.Load<AudioClip>("reload");
        player = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        packsCollected = 0;
        currentAmmo = maxAmmo;
        ammoTracker.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        goal.SetActive(false);
        cheatsOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted) return;
        curAnim = STILL_ANIM;
        getMovement();
        Move();

        //movement code
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        getShot();
        animator.SetInteger("ActionState", curAnim);
	}
    public void startControl(){
        gameStarted = true;
    }
    public void Move()
    {
        //player.velocity = speed * Time.deltaTime * direction;
        player.velocity = speed * direction;
    }

    private void getMovement()
    {
        direction = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            curAnim = WALK_ANIM;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            curAnim = WALK_ANIM;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo)
        {
            if (currentAmmo != 0)
            {
                currentAmmo = 0;
                soundPlayed = true;
                StartCoroutine(Reload());
                soundPlayed = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (cheatsOn)
            {
                spriteMask.localScale = new Vector3(13, 13, 1);
                cheatsOn = false;
            }
            else
            {
                spriteMask.localScale += new Vector3(1000, 1000, 0);
                cheatsOn = true;
            }
        }
        direction.Normalize();
        //direction *= 50;
    }

    private void getShot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Instantiate(projectile, player.position, Quaternion.identity);
            curAnim = SHOOT_ANIM;
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            } else
            {
                ammoTracker.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
            }
        }
    }

    IEnumerator Reload()
    {
        ammoTracker.text = "Reloading...";
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        ammoTracker.text = "Full";
    }
}
