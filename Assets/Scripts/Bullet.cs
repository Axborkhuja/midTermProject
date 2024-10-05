using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Bullet speed
    public ParticleSystem destroyParticle;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Optional: Add damage logic to target here or play animation of destroying
        destroyParticle.Play();
            // Destroy the bullet
        Destroy(gameObject);
    }
}
