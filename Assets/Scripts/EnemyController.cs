using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem destroyObject;
    public GameObject bulletPrefab; // Array of different bullet prefabs
    public Transform[] firePoints; // Array of fire points where bullets will spawn
    public Transform player; // Reference to the player's transform
    public float speed = 2f; // Speed of the enemy's movement
    public float deviationAmount = 0.5f; // Deviation in movement
    public float shootDelay = 1.5f; // Delay between shots
    public float bulletSpeed = 10f; // Speed of the bullet
    public RectTransform bulletManager; // Reference to the Canvas for UI bullets (if needed)

    private Vector2 deviation; // Random deviation for movement
    private float nextShootTime; // Time for the next shoot action
    private float score;
    private RectTransform rt;
    void Start()
    {
        // Calculate initial deviation
        deviation = new Vector2(Random.Range(-deviationAmount, deviationAmount), Random.Range(-deviationAmount, deviationAmount));
        nextShootTime = Time.time + shootDelay; // Set the initial shooting time
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Move towards the player with some deviation
        MoveTowardsPlayer();
        if (rt.anchoredPosition.y < -500)
        {
            DestroyImmediate(gameObject);
        }
            Debug.Log("shOOT");
            Shoot();
            Invoke("Shoot delay",shootDelay); // Reset shoot delay
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("kill");
        destroyObject.Play();
        if (collision.CompareTag("Bullet"))
        {
            score += 2;
        }
        Destroy(gameObject);
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
        foreach (Transform firePoint in firePoints)
        {
            // Instantiate the bulletPrefab as a child of the canvas
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, bulletManager.transform);
            // Get the RectTransform component of the bulletPrefab
            RectTransform bulletRect = bullet.GetComponent<RectTransform>();
            // Set the position relative to the canvas (firePoint position in this case)
            bulletRect.Rotate(0, 0, 180);
            if (bulletRect != null)
            {
                bulletRect.position = firePoint.position; // Set its position to the firePoint
            }

            // You can add bullet velocity logic here if needed (depends on UI-based behavior)
        }
    }
}
