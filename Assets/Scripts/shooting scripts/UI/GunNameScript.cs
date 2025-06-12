using UnityEngine;
using TMPro;

public class GunNameScript : MonoBehaviour
{
    ShooterController shooterController;
    TextMeshProUGUI gunName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        shooterController = GameObject.FindWithTag("Jeepney").GetComponent<ShooterController>();
        gunName = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        gunName.text = shooterController.getCurrentGunName();
    }
}
