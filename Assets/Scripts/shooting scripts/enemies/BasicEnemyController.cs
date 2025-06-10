using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float Hp = 20f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float shootInterval = 0.5f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int damage = 1;
    GameObject player;
    Rigidbody2D enemyBody;
    Vector3 finalDirection;
    float currentInterval = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        enemyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        deathCheck();

        finalDirection = player.transform.position - transform.position;

        currentInterval += Time.deltaTime;
        if (currentInterval >= shootInterval)
        {
            currentInterval = 0f;

            GameObject boolet = Instantiate(bulletPrefab);

            Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
            booletBody.linearVelocity = bulletSpeed * finalDirection.normalized;

            float offset = 0.6f; //spawns the bullet outside the enemy 
            boolet.transform.position = transform.position + offset * finalDirection.normalized;

            boolet.GetComponent<BulletScript>().setDamage(damage);

            Destroy(boolet, 1f);
        }
    }

    void FixedUpdate()
    {
        enemyBody.linearVelocity = finalDirection.normalized * movementSpeed;
    }

    void deathCheck()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= collision.gameObject.GetComponent<BulletScript>().getDamage();
            Debug.Log("enemy hp: " + Hp);
        }
    }
}
