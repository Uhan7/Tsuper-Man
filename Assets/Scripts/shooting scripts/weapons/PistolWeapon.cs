using UnityEngine;

public class PistolWeapon : WeaponClass
{
    protected override void setUpValues()
    {
        ID = 0;
        holdToShoot = false;
        shootInterval = 0.5f;
        damage = 2f;

    }

    protected override void shootBullet()
    {
        currentInterval = 0f;
        GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - player.position;

        Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
        booletBody.linearVelocity = bulletSpeed * direction.normalized;

        float offset = 0.6f; //spawns the bullet outside the player 
        boolet.transform.position = transform.position + (Vector3)(offset * direction.normalized);

        boolet.GetComponent<BulletScript>().setDamage(damage);

        Destroy(boolet, 1f);
    }
}
