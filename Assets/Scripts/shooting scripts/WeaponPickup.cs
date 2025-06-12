using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSFX;

    [SerializeField] int weaponId = 0;
    [SerializeField] bool canRespawn = false;
    [SerializeField] float respawnTimer = 0f;
    Transform weaponImage;
    float currentTimer = 0f;
    bool isActive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        weaponImage = transform.GetChild(0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Jeepney")
        {
            Parameters param = new Parameters();
            param.PutExtra(ParamNames.WEAPON_PICKUP_ID, weaponId);
            EventBroadcaster.Instance.PostEvent(EventNames.WEAPON_PICKUP, param);

            weaponImage.gameObject.SetActive(false);
            isActive = false;

            GameObject.Find("SFX Source").GetComponent<AudioSource>().PlayOneShot(pickupSFX);

            // Jus gon put this here
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canRespawn || isActive)
            return;

        currentTimer += Time.deltaTime;
        if (currentTimer >= respawnTimer)
        {
            currentTimer = 0f;
            weaponImage.gameObject.SetActive(true);
            isActive = true;
        }
    }
}
