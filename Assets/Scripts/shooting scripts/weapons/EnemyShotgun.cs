using UnityEngine;

public class EnemyShotgun : EnemyWeaponClass
{
    protected override void shootBullet()
    {
        currentInterval = 0f;

        for (int i = -3; i < 3; i++)
        {
            GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

            Vector2 direction = GetInterceptDirection();

            Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
            float fixspread = 3;
            Vector2 spreadDirection = Quaternion.Euler(0, 0, fixspread * i) * direction;
            booletBody.linearVelocity = bulletSpeed * spreadDirection.normalized;

            boolet.transform.position = (Vector2)enemy.position + (enemyOffset * direction.normalized);

            boolet.GetComponent<BulletScript>().setDamage(damage);

            Destroy(boolet, 1f);
        }
    }
}
