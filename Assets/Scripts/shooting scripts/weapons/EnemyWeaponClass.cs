using UnityEngine;

public abstract class EnemyWeaponClass : MonoBehaviour
{
    public AudioClip shootSFX;
    protected GameObject bulletPrefab;
    protected Rigidbody2D player;
    protected Transform enemy;
    public float shootInterval;
    protected float currentInterval = 0f;
    public float bulletSpeed = 40f;
    public float enemyOffset = 0.6f;
    public int damage;

    public virtual void make(GameObject bulletPrefabRef, Rigidbody2D playerRB, Transform enemyRef)
    {
        this.bulletPrefab = bulletPrefabRef;
        this.player = playerRB;
        this.enemy = enemyRef;
    }

    public virtual void shoot()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval >= shootInterval)
        {
            shootBullet();
            GameObject.Find("SFX Source").GetComponent<AudioSource>().PlayOneShot(shootSFX);
        }

        //rotate weapon
        Vector2 direction = player.position - (Vector2)enemy.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 scale = transform.localScale;
        if(angle < -90 || angle > 90)
            scale.y = -1;
        else 
            scale.y = 1;
        transform.localScale = scale;
    }

    protected virtual Vector2 GetInterceptDirection() //thx gpt lmao
    {
        Vector2 displacement = player.position - (Vector2)enemy.position;
        float a = Vector2.Dot(player.linearVelocity, player.linearVelocity) - bulletSpeed * bulletSpeed;
        float b = 2 * Vector2.Dot(player.linearVelocity, displacement);
        float c = Vector2.Dot(displacement, displacement);

        float discriminant = b * b - 4 * a * c;

        // No valid solution
        if (discriminant < 0 || Mathf.Approximately(a, 0f))
            return (player.position - (Vector2)enemy.position).normalized;

        float t = (-b - Mathf.Sqrt(discriminant)) / (2 * a); // time to impact

        if (t < 0)
            t = 0;

        Vector2 predictedPosition = player.position + player.linearVelocity * t;
        return (predictedPosition - (Vector2)enemy.position).normalized;
    }

    protected abstract void shootBullet();
}
