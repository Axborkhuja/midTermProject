using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem particleSystem1;
    public ParticleSystem destroyObject;
    public ParticleSystem particleSystem2;
    public GameObject bulletPrefab; // Prefab of the bullet (UI element)
    public Transform[] firePoints; // Array of positions where the bullets spawn
    public float shoootingDelay=5f;
    public float bulletSpeed = 10f; // Speed of the bullet
    public GameObject bulletManager; // Reference to the Canvas
    private float speed = 10f;
    private Rigidbody2D rb;
    private float previousVerticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousVerticalInput = 0f; // Initial value
    }

    void FixedUpdate()
    {
        float currentVerticalInput = Input.GetAxis("Vertical");

        // Change particle system gravity modifier based on input condition
        if (currentVerticalInput > previousVerticalInput)
        {
            var main1 = particleSystem1.main;
            var main2 = particleSystem2.main;

            // Set gravity modifier to -0.5
            main1.gravityModifier = -0.5f;
            main2.gravityModifier = -0.5f;
        }
        else if (currentVerticalInput < previousVerticalInput)
        {
            // Disable the emission and adjust gravity modifier
            var main1 = particleSystem1.main;
            var main2 = particleSystem2.main;

            // Set gravity modifier to 0.3
            main1.gravityModifier = 0.3f;
            main2.gravityModifier = 0.3f;
        }

        // Update the velocity
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal"), currentVerticalInput) * speed;

        // Update the previous vertical input value for the next frame
        previousVerticalInput = currentVerticalInput;
    }

    void Update()
    {
        // Check if the spacebar is pressed for shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("Shooting delay", shoootingDelay);
            Shoot();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            destroyObject.Play();
            Invoke("Player destroyed", 2);
            Time.timeScale = 0;
        }
    }
    void Shoot()
    {
        // Loop through each fire point and shoot a bullet
        foreach (Transform firePoint in firePoints)
        {

            // You can add bullet velocity logic here if needed (depends on UI-based behavior)
        }
    }
}
