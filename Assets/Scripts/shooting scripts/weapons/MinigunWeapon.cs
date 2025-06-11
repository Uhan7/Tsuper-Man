using UnityEngine;

public class MinigunWeapon : WeaponClass
{
    protected override void setUpValues()
    {
        ID = 2;
        //weaponName = "Minigun";
        //holdToShoot = true;
        //shootInterval = 0.1f;
        //damage = 2f;
        //maxAmmo = 50;
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
        float rand = Random.Range(-15, 15);
        Vector2 spreadDirection = Quaternion.Euler(0, 0, rand) * direction;
        booletBody.linearVelocity = (bulletSpeed + getPlayerSpeed(direction)) * spreadDirection.normalized;

        boolet.transform.position = player.position + (Vector3)(playerOffset * direction.normalized);

        boolet.GetComponent<BulletScript>().setDamage(damage);

        Destroy(boolet, 1f);
    }
}
