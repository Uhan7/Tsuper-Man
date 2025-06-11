using UnityEngine;

public class PistolWeapon : WeaponClass
{
    protected override void setUpValues()
    {
        ID = 0;
        //weaponName = "Pistol";
        //holdToShoot = false;
        //shootInterval = 0.5f;
        //damage = 5f;
        currentAmmo = -69; //ID for infinite ammo, yes
    }

    protected override void shootBullet()
    {
        currentInterval = 0f;
        GameObject boolet = Instantiate(bulletPrefab); //maybe switch to pool

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = mouseWorldPos - player.position;

        Rigidbody2D booletBody = boolet.GetComponent<Rigidbody2D>();
        booletBody.linearVelocity = (bulletSpeed + getPlayerSpeed(direction)) * direction.normalized;

        boolet.transform.position = player.position + (Vector3)(playerOffset * direction.normalized);

        boolet.GetComponent<BulletScript>().setDamage(damage);

        Destroy(boolet, 1f);
    }
}
