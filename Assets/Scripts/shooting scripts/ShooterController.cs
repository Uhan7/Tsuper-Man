using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float Hp = 100f;
    [SerializeField] float shooterInterval = 0.3f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float damage = 1f;
    float currentInterval = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentInterval += Time.deltaTime;
        if (Input.GetMouseButton(0) && currentInterval >= shooterInterval)
        {
            currentInterval = 0f;
            GameObject boolet = Instantiate(bulletPrefab);

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mouseWorldPos - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //boolet.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
            booletBody.linearVelocity = bulletSpeed * direction.normalized;

            float offset = 0.6f; //spawns the bullet outside the player 
            boolet.transform.position = transform.position + (Vector3)(offset * direction.normalized);

            boolet.GetComponent<BulletScript>().setDamage(damage);

            Destroy(boolet, 3f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= collision.gameObject.GetComponent<BulletScript>().getDamage();
            Debug.Log("player hp: " + Hp);
        }
    }
}
