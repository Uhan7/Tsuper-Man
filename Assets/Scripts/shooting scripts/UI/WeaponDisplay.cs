using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] int weaponIndex = 0;
    Image gunImage;
    WeaponClass weaponRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gunImage = transform.GetComponent<Image>();

        Transform player = GameObject.FindWithTag("Player").transform;
        foreach (Transform obj in player)
            if (obj.GetComponent<RifleWeapon>() != null && weaponIndex == 1)
            {
                weaponRef = obj.GetComponent<RifleWeapon>();
                break;
            }
            else if (obj.GetComponent<MinigunWeapon>() != null && weaponIndex == 2)
            {
                weaponRef = obj.GetComponent<MinigunWeapon>();
                break;
            }
            else if (obj.GetComponent<ShotgunWeapon>() != null && weaponIndex == 3)
            {
                weaponRef = obj.GetComponent<ShotgunWeapon>();
                break;      
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponRef.getCurrentAmmo() <= 0)
        {
            Color c = gunImage.color;
            c.a = 0.3f;
            gunImage.color = c;
        }
        else
        { 
            Color c = gunImage.color;
            c.a = 1f;
            gunImage.color = c;
        }
    }
}
