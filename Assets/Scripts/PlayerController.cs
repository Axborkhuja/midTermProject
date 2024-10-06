using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource destroyAudio;
    public AudioSource onShoot;
    public AudioSource engine;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public ParticleSystem destroy;
    public GameObject bulletPrefab; // Bullet prefab to shoot
    public Transform firePoint; // Fire point for shooting
    public Canvas canvas;
    public float bulletSpeed = 10f; // Speed of the bullet
    public float moveSpeed = 5f; // Movement speed of the player
    private Rigidbody2D rb;
    public int health = 100;
    public GameController logic;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        engine.Play();
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        var main1 = particle1.main;
        var main2 = particle2.main;

        if (rb.linearVelocity.x != 0 || rb.linearVelocity.y != 0)
        {
            main1.gravityModifier = 0.3f;
            main2.gravityModifier = 0.3f;
        }
        else
        {
            main1.gravityModifier = -0.5f;
            main2.gravityModifier = -0.5f;
        }

        // Set linear velocity based on input
        rb.linearVelocity = new Vector2(moveX, moveY) * moveSpeed;

        // Shoot when spacebar is pressed
        if (Input.GetMouseButtonUp(0))
        {
            Invoke("buttonClick", 1);
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, canvas.transform);
        bullet.tag = "PlayerBullet";
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        onShoot.Play();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = new Vector2(0, 10) * bulletSpeed; // Move the bullet in the direction of firePoint
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            if (health - 150 > 0)
            {
                health -= 150;
                logic.updateHealth(150);
            }
            else
            {
                Die();
            }
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        // Stop the player from moving and interacting
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;  // Makes the player non-interactive with physics

        // Play the particle effect for destruction
        destroy.Play(); // Play the destruction particle effect

        // Play audio for destruction
        destroyAudio.Play();

        // Disable player visuals (e.g., sprite renderer) if you want to make them invisible during the particle effect
        GetComponent<SpriteRenderer>().enabled = false;

        // Delay destruction of the player object to allow the particle effect to play fully
        Destroy(gameObject, destroy.main.duration); // This destroys the gameObject after the particle effect duration

        // Optionally, freeze the game or slow down time
        Time.timeScale = 0.5f; // Slow motion effect
    }
}
