using UnityEngine;

public class EnemyMinigun : EnemyWeaponClass
{
    protected override void shootBullet()
    {
        currentInterval = 0f;
        GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

        Vector2 direction = GetInterceptDirection();

        Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
        float rand = Random.Range(-15, 15);
        Vector2 spreadDirection = Quaternion.Euler(0, 0, rand) * direction;
        booletBody.linearVelocity = bulletSpeed * spreadDirection.normalized;

        boolet.transform.position = (Vector2)enemy.position + (enemyOffset * direction.normalized);

        boolet.GetComponent<BulletScript>().setDamage(damage);

        Destroy(boolet, 1f);
    }
}
