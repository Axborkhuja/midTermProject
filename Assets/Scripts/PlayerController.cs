using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        logic = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        var main1 = particle1.main;
        var main2 = particle2.main;

        if (rb.linearVelocity.x != 0 || rb.linearVelocity.y != 0)
        {
            main1.gravityModifier=0.3f;
            main2.gravityModifier=0.3f;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation,canvas.transform);
        bullet.tag = "PlayerBullet";
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.AddForce( new Vector2(0,1) * bulletSpeed); // Move the bullet in the direction of firePoint
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet")){
            if (health-150 > 0){
                health = health - 150;
                logic.updateHealth(150);
            }else{
                destroy.Play();
                Destroy(gameObject);
                Time.timeScale = 0;
            }
        }

        // Destroy the player when hit by something
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            destroy.Play();
            Invoke("destroy", 1);
            Time.timeScale = 0;
        }
    }
}
