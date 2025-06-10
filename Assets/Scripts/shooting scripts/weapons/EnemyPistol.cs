using UnityEngine;

public class EnemyPistol : EnemyWeaponClass
{
    protected override void shootBullet()
    {
        currentInterval = 0f;
        GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

        Vector2 direction = GetInterceptDirection();

        Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
        booletBody.linearVelocity = bulletSpeed * direction.normalized;

        boolet.transform.position = (Vector2)enemy.position + (enemyOffset * direction.normalized);

        boolet.GetComponent<BulletScript>().setDamage(damage);

        Destroy(boolet, 1f);
    }
}
