using UnityEngine;
using TMPro;

public class AmmoCountScript : MonoBehaviour
{
    ShooterController shooterController;
    TextMeshProUGUI ammo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        shooterController = GameObject.FindWithTag("Jeepney").GetComponent<ShooterController>();
        ammo = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooterController.getCurrentGunID() == 0)
            ammo.text = "Infinite";
        else
            ammo.text = shooterController.getCurrentGunAmmo().ToString();
    }
}
