using UnityEngine;

public class DestroyOnBadSpawn : MonoBehaviour
{
    [HideInInspector] public int buildingsTouched; // To be used in EnemyBase to check for the killer

    [SerializeField] private float timeForCheck = .2f;

    private void Start()
    {
        timeForCheck = .2f;
    }

    private void Update()
    {
        if (timeForCheck < 0) return;
        timeForCheck -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (timeForCheck <= 0) return;

        if (col.gameObject.tag == "Building") buildingsTouched++;

        if (buildingsTouched >= 1) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (timeForCheck <= 0) return;

        if (col.gameObject.tag == "Building") buildingsTouched++;

        if (buildingsTouched >= 1) Destroy(gameObject);
    }
}
