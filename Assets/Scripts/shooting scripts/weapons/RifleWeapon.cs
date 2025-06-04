using UnityEngine;

public class RifleWeapon : WeaponClass
{
    protected override void setUpValues()
    {
        ID = 1;
        //weaponName = "Rifle";
        //holdToShoot = true;
        //shootInterval = 0.3f;
        //damage = 3f;
        //maxAmmo = 30;
        currentAmmo = maxAmmo; 
    }

    protected override void shootBullet()
    {
        currentInterval = 0f;
        currentAmmo--;
        GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - player.position;

        Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
        float rand = Random.Range(-5, 5);
        Vector2 spreadDirection = Quaternion.Euler(0, 0, rand) * direction;
        booletBody.linearVelocity = bulletSpeed * spreadDirection.normalized;

        boolet.transform.position = player.position + (Vector3)(playerOffset * direction.normalized);

        boolet.GetComponent<BulletScript>().setDamage(damage);

        Destroy(boolet, 1f);
    }
}
