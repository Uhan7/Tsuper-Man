using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected GameObject bulletPrefab;
    protected Rigidbody2D player;
    protected Rigidbody2D enemy;
    protected EnemyWeaponClass currentWeapon;

    public int Hp = 20;
    public float movementSpeed;
    public float stopDistance;
    public float stopDistanceBuffer;
    public float detectionDistance;

    public virtual void make(GameObject bulletPrefabRef, Rigidbody2D playerRB, Rigidbody2D enemyRB, EnemyWeaponClass currentWeaponRef)
    {
        this.bulletPrefab = bulletPrefabRef;
        this.player = playerRB;
        this.enemy = enemyRB;
        this.currentWeapon = currentWeaponRef;
    }

    public virtual void fixedFollowPlayer() //put in fixed update
    {
        Vector2 direction = player.position - enemy.position;

        float distance = Vector2.Distance(player.position, enemy.position);
        if (distance > stopDistance && distance < detectionDistance)
            enemy.linearVelocity = direction.normalized * movementSpeed;
        else if(distance < stopDistance - stopDistanceBuffer && distance < detectionDistance)
            enemy.linearVelocity = direction.normalized * -movementSpeed;
        else
            enemy.linearVelocity = Vector2.zero;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= collision.gameObject.GetComponent<BulletScript>().getDamage();
            //Debug.Log("enemy hp: " + Hp);

            if (Hp <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Killer")
        {
            col.gameObject.GetComponentInParent<Rigidbody2D>().linearVelocity *= 0.9f;
            Destroy(gameObject);
        }
    }

    public virtual void shootWeapon()
    {
        float distance = Vector2.Distance(player.position, enemy.position);
        if(distance >= stopDistance - stopDistanceBuffer && distance < detectionDistance)
            currentWeapon.shoot();
    }
}
