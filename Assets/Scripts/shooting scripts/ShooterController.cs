using UnityEngine;
using System.Collections.Generic;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] weaponEmpties;
    public float Hp = 100f;
    [SerializeField] float weaponSwitchCooldown = 1f;

    //0 - pistol, 1 - rifle, 2 - minigun, 3 - shotgun
    List<WeaponClass> weaponsList;
    WeaponClass currentWeapon;
    float SwitchTimer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        addWeapons();
        currentWeapon = weaponsList[0];
        activateWeapon(0);

        EventBroadcaster.Instance.AddObserver(EventNames.WEAPON_PICKUP, this.pickupWeapon);
        EventBroadcaster.Instance.AddObserver(EventNames.HEALTH_PICKUP, this.healPickup);
    }

    void healPickup(Parameters param)
    {
        int healAmount = param.GetIntExtra(ParamNames.HEALTH_PICKUP_HEAL, 0);
        Hp += healAmount;

        Hp = Mathf.Clamp(Hp, 0, 100);
    }

    void pickupWeapon(Parameters param)
    {
        int id = param.GetIntExtra(ParamNames.WEAPON_PICKUP_ID, 0);
        if (id == 0)
            return;

        weaponsList[id].addToCurrentAmmo(weaponsList[id].getMaxAmmo());
    }

    void addWeapons()
    {
        weaponsList = new List<WeaponClass>();

        PistolWeapon pistol = weaponEmpties[0].GetComponent<PistolWeapon>();
        pistol.make(bulletPrefab, transform);
        weaponsList.Insert(0, pistol);

        RifleWeapon rifle = weaponEmpties[1].GetComponent<RifleWeapon>();
        rifle.make(bulletPrefab, transform);
        weaponsList.Insert(1, rifle);

        MinigunWeapon mini = weaponEmpties[2].GetComponent<MinigunWeapon>();
        mini.make(bulletPrefab, transform);
        weaponsList.Insert(2, mini);

        ShotgunWeapon shot = weaponEmpties[3].GetComponent<ShotgunWeapon>();
        shot.make(bulletPrefab, transform);
        weaponsList.Insert(3, shot);
    }

    //update when adding more or less weapons
    void switchWeapon()
    {
        SwitchTimer += Time.deltaTime;
        if (SwitchTimer < weaponSwitchCooldown)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1) || (currentWeapon.getCurrentAmmo() <= 0 && currentWeapon.getWeaponID() != 0))
        {
            currentWeapon = weaponsList[0];
            activateWeapon(0);
            SwitchTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponsList[1].getCurrentAmmo() > 0)
        {
            currentWeapon = weaponsList[1];
            activateWeapon(1);
            SwitchTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponsList[2].getCurrentAmmo() > 0)
        {
            currentWeapon = weaponsList[2];
            activateWeapon(2);
            SwitchTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && weaponsList[3].getCurrentAmmo() > 0)
        {
            currentWeapon = weaponsList[3];
            activateWeapon(3);
            SwitchTimer = 0f;
        }
    }

    void activateWeapon(int index)
    {
        for(int i = 0; i < weaponEmpties.Length; i++)
            if(i != index)
                weaponEmpties[i].GetComponent<SpriteRenderer>().enabled = false;
            else
                weaponEmpties[i].GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switchWeapon();
        currentWeapon.shoot();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= collision.gameObject.GetComponent<BulletScript>().getDamage();

            EventBroadcaster.Instance.PostEvent(EventNames.JEEP_HURT);
        }

        if (Hp <= 0)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.JEEP_DEAD);
            gameObject.SetActive(false);
        }
    }

    public string getCurrentGunName()
    {
        return currentWeapon.getWeaponName();
    }

    public int getCurrentGunID()
    {
        return currentWeapon.getWeaponID();
    }

    public int getCurrentGunAmmo()
    {
        return currentWeapon.getCurrentAmmo();
    }
}
