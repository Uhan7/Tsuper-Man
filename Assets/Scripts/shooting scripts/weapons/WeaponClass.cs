using UnityEngine;

public abstract class WeaponClass : MonoBehaviour
{
    protected GameObject bulletPrefab;
    protected Transform player;
    protected int ID;
    public string weaponName;
    public bool holdToShoot;
    public float shootInterval;
    protected float currentInterval = 0f;
    public float bulletSpeed = 40f;
    public float playerOffset = 0.6f;
    public float damage;
    public int maxAmmo;
    protected int currentAmmo;

    public virtual void make(GameObject bulletPrefabRef, Transform playerRef)
    {
        this.bulletPrefab = bulletPrefabRef;
        this.player = playerRef;
        setUpValues();
    }

    public virtual int getWeaponID()
    {
        return ID;
    }

    public virtual void shoot()
    {
        currentInterval += Time.deltaTime;
        if (holdToShoot && Input.GetMouseButton(0) && currentInterval >= shootInterval && (currentAmmo == -69 || currentAmmo > 0))
            shootBullet();
        else if (!holdToShoot && Input.GetMouseButtonDown(0) && currentInterval >= shootInterval && (currentAmmo == -69 || currentAmmo > 0))
            shootBullet();
    }

    public virtual string getWeaponName()
    {
        return weaponName;
    }

    public virtual int getCurrentAmmo()
    {
        return currentAmmo;
    }

    protected abstract void shootBullet();
    protected abstract void setUpValues();
}
