using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private GameObject enemyKillerObject;
    [SerializeField] private float velocityToActivate;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        enemyKillerObject.SetActive(rb.linearVelocity.magnitude >= velocityToActivate);
    }
}
