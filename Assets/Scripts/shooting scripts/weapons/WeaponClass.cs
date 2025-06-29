using UnityEngine;

public abstract class WeaponClass : MonoBehaviour
{
    public AudioClip shootSFX;

    protected GameObject bulletPrefab;
    protected Transform player;
    protected int ID;
    public string weaponName;
    public bool holdToShoot;
    public bool addPlayerVelocity = false;
    public float shootInterval;
    protected float currentInterval = 0f;
    public float bulletSpeed = 40f;
    public float playerOffset = 0.6f;
    public int damage;
    public int maxAmmo;
    protected int currentAmmo;

    public virtual void make(GameObject bulletPrefabRef, Transform playerRef)
    {
        this.bulletPrefab = bulletPrefabRef;
        this.player = playerRef;
        setUpValues();
    }

    protected virtual float getPlayerSpeed(Vector2 direction)
    {
        if (!addPlayerVelocity)
            return 0f;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float speedInDirection = Vector2.Dot(rb.linearVelocity, direction.normalized);

        return Mathf.Max(0f, speedInDirection);
    }

    public virtual int getWeaponID()
    {
        return ID;
    }

    public virtual void shoot()
    {
        currentInterval += Time.deltaTime;
        if (holdToShoot && Input.GetMouseButton(0) && currentInterval >= shootInterval && (currentAmmo == -69 || currentAmmo > 0))
        {
            shootBullet();
            GameObject.Find("SFX Source").GetComponent<AudioSource>().PlayOneShot(shootSFX);
        }
        else if (!holdToShoot && Input.GetMouseButtonDown(0) && currentInterval >= shootInterval && (currentAmmo == -69 || currentAmmo > 0))
        {
            shootBullet();
            GameObject.Find("SFX Source").GetComponent<AudioSource>().PlayOneShot(shootSFX);
        }

        //rotate weapon
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public virtual string getWeaponName()
    {
        return weaponName;
    }

    public virtual int getCurrentAmmo()
    {
        return currentAmmo;
    }

    public virtual void addToCurrentAmmo(int moreAmmo)
    {
        currentAmmo += moreAmmo;
    }

    public virtual int getMaxAmmo()
    {
        return maxAmmo;
    }

    protected abstract void shootBullet();
    protected abstract void setUpValues();
}
