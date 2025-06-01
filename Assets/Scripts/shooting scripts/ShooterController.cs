using UnityEngine;
using System.Collections.Generic;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] weaponEmpties;
    [SerializeField] float Hp = 100f;

    //0 - pistol, 1 - rifle
    List<WeaponClass> weaponsList;
    WeaponClass currentWeapon;


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
    }

    // Update is called once per frame
    void Update()
    {
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
}
