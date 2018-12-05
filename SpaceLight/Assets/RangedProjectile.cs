using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{

    static AudioSource source;
    public AudioClip shoot;
    private Transform player;
    private Vector2 direction;
    public float speed;
    public static AudioClip shootSound;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        shootSound = Resources.Load<AudioClip>("enemyshoot");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector2 distance = player.transform.position - transform.position;
        if (distance.magnitude <= 15)
        {
            if (distance.magnitude >= 10)
            {
                source.volume = 0.1f;
            }
            else if (distance.magnitude >= 5)
            {
                source.volume = 0.4f;
            }
            else
            {
                source.volume = 1;
            }
            source.PlayOneShot(shootSound);
        }
        direction = player.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destructible" && collision.gameObject != this.parent)
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            PlayerHealth.health -= 10;
        }
    }
}
