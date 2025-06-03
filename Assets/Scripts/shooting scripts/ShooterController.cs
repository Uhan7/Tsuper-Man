using UnityEngine;
using System.Collections.Generic;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] weaponEmpties;
    [SerializeField] float Hp = 100f;
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
            SwitchTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponsList[1].getCurrentAmmo() > 0)
        {
            currentWeapon = weaponsList[1];
            SwitchTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponsList[2].getCurrentAmmo() > 0)
        {
            currentWeapon = weaponsList[2];
            SwitchTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && weaponsList[3].getCurrentAmmo() > 0)
        {
            currentWeapon = weaponsList[3];
            SwitchTimer = 0f;
        }
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
            Debug.Log("player hp: " + Hp);
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
