using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem destroyObject;
    public Transform[] firePoints; // Array of fire points where bullets will spawn
    public Transform player; // Reference to the player's transform
    public float speed = 2f; // Speed of the enemy's movement
    public float deviationAmount = 0.5f; // Deviation in movement
    public float shootDelay = 1.5f; // Delay between shots
    private Vector2 deviation; // Random deviation for movement
    private float nextShootTime; // Time for the next shoot action
    private float score;

    void Start()
    {
        // Calculate initial deviation
        deviation = new Vector2(Random.Range(-deviationAmount, deviationAmount), Random.Range(-deviationAmount, deviationAmount));
        nextShootTime = Time.time + shootDelay; // Set the initial shooting time
    }

    void Update()
    {
        // Move towards the player with some deviation
        MoveTowardsPlayer();

        // Check if it's time to shoot
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootDelay; // Reset shoot delay
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play particle effect and destroy enemy when hit by a bullet
        if (collision.CompareTag("Bullet"))
        {
            score += 2;
            destroyObject.Play();
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate direction towards player with deviation
            Vector2 direction = ((Vector2)player.position + deviation - (Vector2)transform.position).normalized;

            // Move enemy in the calculated direction
            transform.Translate(direction * speed * Time.deltaTime);

            // Recalculate deviation occasionally for erratic movement
            deviation = new Vector2(0, Random.Range(-deviationAmount, deviationAmount));
        }
    }

    void Shoot()
    {
        Bullet bullet = new Bullet();
        foreach (Transform firePoint in firePoints)
        {
            bullet.Shoot(firePoint);
        }
    }
}
