using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 50;
    [SerializeField] bool canRespawn = false;
    [SerializeField] float respawnTimer = 0f;
    Transform healImage;
    float currentTimer = 0f;
    bool isActive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        healImage = transform.GetChild(0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Jeepney")
        {
            Parameters param = new Parameters();
            param.PutExtra(ParamNames.HEALTH_PICKUP_HEAL, healAmount);
            EventBroadcaster.Instance.PostEvent(EventNames.HEALTH_PICKUP, param);

            healImage.gameObject.SetActive(false);
            isActive = false;

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
            healImage.gameObject.SetActive(true);
            isActive = true;
        }
    }
}
