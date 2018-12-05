using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note that will need to start at least triggerDistance from player to not shoot during dialogue.
public class RangedEnemy : MonoBehaviour {

    public RangedProjectile projectile;
    public bool triggered;
    public bool turnedOn;
    private Transform enemy;
    private Transform player;
    private Vector2 direction;
    public float triggerDistance;
    public float fireRate = 1;
	// Use this for initialization
    

    void Start() {
        enemy = GetComponent<Rigidbody2D>().GetComponent<Transform>(); ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        triggered = false;
        turnedOn = false;
        // Update is called once per frame
    }
	void Update () {
        direction = player.position - enemy.position;
        if (direction.magnitude <= triggerDistance && !turnedOn)
        {
            triggered = true;
            turnedOn = true;
            turnOn();
        }
	}

    void turnOn()
    {
        InvokeRepeating("Shoot", 1f, fireRate);
    }

    void Shoot()
    {
        
        RangedProjectile bullet = Instantiate(projectile, enemy.position, Quaternion.identity);
        projectile.parent = this.gameObject;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
