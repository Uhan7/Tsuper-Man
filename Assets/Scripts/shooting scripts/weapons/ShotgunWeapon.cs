using UnityEngine;

public class ShotgunWeapon : WeaponClass
{
    protected override void setUpValues()
    {
        ID = 3;
        weaponName = "Shotgun";
        holdToShoot = true;
        shootInterval = 1f;
        damage = 5f;
        maxAmmo = 10;
        currentAmmo = maxAmmo;
    }

    protected override void shootBullet()
    {
        currentInterval = 0f;
        currentAmmo--;

        for (int i = -3; i < 3; i++)
        {
            GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mouseWorldPos - player.position;

            Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
            float fixspread = 3;
            Vector2 spreadDirection = Quaternion.Euler(0, 0, fixspread * i) * direction;
            booletBody.linearVelocity = bulletSpeed * spreadDirection.normalized;

            boolet.transform.position = player.position + (Vector3)(playerOffset * direction.normalized);

            boolet.GetComponent<BulletScript>().setDamage(damage);

            Destroy(boolet, 1f);
        }
    }
}
