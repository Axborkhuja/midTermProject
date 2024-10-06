using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50;
    private Rigidbody2D rb;
    private RectTransform rt;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rb.linearVelocity=new Vector2(speed * Time.deltaTime,transform.position.x);
    }
    private void FixedUpdate()
    {

        if(rt.anchoredPosition.y >500 || rt.anchoredPosition.y < -500)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the bullet upon collision
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
