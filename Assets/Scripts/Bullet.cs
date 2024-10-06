using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Bullet speed
    public ParticleSystem destroyParticle;
    public GameObject bulletPrefab; // Array of different bullet prefabs for variety
    public GameObject bulletManager; // Reference to the Canvas where UI bullets will be instantiated
    public float bulletSpeed = 500f; // Speed for moving the bullet (in UI coordinates)

    private Rigidbody2D rb;
    private RectTransform rt;
    private Vector2 deviation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityY = speed * Time.deltaTime;
        if (rt.anchoredPosition.y > 200 || rt.anchoredPosition.y > -200)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Transform firePoint)
    {
        // Instantiate the bulletPrefab as a child of the canvas
        GameObject bullet = Instantiate(bulletPrefab, bulletManager.transform);

        // Get the RectTransform component of the bulletPrefab
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Set the position relative to the canvas (firePoint position in this case)
        if (bulletRb != null)
        {
            bulletRb.position = firePoint.position; // Set its position to the firePoint
            //bulletRb.linearVelocity = transform.down * bulletSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Optional: Add damage logic to target here or play animation of destroying
        destroyParticle.Play();
            // Destroy the bullet
        Destroy(gameObject);
    }
}
