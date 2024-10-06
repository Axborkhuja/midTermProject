using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem destroy;
    public GameObject bulletPrefab; // Bullet prefab for shooting
    public Transform[] firePoints; // Multiple fire points
    public float speed = 2f; // Speed of the enemy's movement
    public float xRange = 2f; // X-axis movement range
    public float shootDelay = 1.5f; // Delay between shots
    public float bulletSpeed = 5f; // Speed of the bullet

    private AudioSource destroyAudio;
    private AudioSource onShoot;
    private GameObject canvas;
    private float nextShootTime;
    private RectTransform rt;
    private GameController logic;

    void Start()
    {
        destroyAudio = GameObject.Find("DestroyAudio").GetComponent<AudioSource>();
        onShoot = GameObject.Find("Shoot").GetComponent<AudioSource>();
        nextShootTime = Time.time + shootDelay;
        canvas = GameObject.Find("Canvas");
        rt = GetComponent<RectTransform>();
        logic = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void FixedUpdate()
    {
        if (rt.anchoredPosition.y > 500 || rt.anchoredPosition.y < -600)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // Move enemy down in Y-axis
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Move enemy smoothly in X-axis between two constants
        float newX = Mathf.PingPong(Time.time * speed, xRange * 2) - xRange;
        transform.position = new Vector2(newX, transform.position.y);

        // Shoot at intervals
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootDelay;
        }
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            // Instantiate bullet at each fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, canvas.transform);
            bullet.tag = "EnemyBullet";
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            onShoot.Play();

            if (bulletRb != null)
            {
                bulletRb.linearVelocity=new Vector2(0, -1) * bulletSpeed; // Set the velocity of the bullet
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the enemy on collision with player's bullet
        if (collision.CompareTag("PlayerBullet")|| collision.CompareTag("Obstacle") ||collision.CompareTag("Player"))
        {
            logic.addScore(1);
            destroy.Play();destroyAudio.Play();
            Destroy(gameObject);    
        }
    }
}
