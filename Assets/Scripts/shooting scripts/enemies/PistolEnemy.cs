using UnityEngine;

public class PistolEnemy : EnemyBase
{
    [SerializeField] GameObject bulletFab;

    void Awake()
    {
        Rigidbody2D playerRB = GameObject.FindWithTag("Jeepney").GetComponent<Rigidbody2D>();

        // Just to prevent nullrefernce
        if (playerRB == null) playerRB = GetComponent<Rigidbody2D>();

        Rigidbody2D enemyRB = GetComponent<Rigidbody2D>();

        EnemyPistol pistolWeapon = transform.GetChild(2).GetComponent<EnemyPistol>();
        pistolWeapon.make(bulletFab, playerRB, transform);

        base.make(bulletFab, playerRB, enemyRB, pistolWeapon);
    }

    void Update()
    {
        base.shootWeapon();
    }

    void FixedUpdate()
    {
        base.fixedFollowPlayer();
    }
}