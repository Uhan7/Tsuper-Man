using UnityEngine;

public class RifleEnemy : EnemyBase
{
    [SerializeField] GameObject bulletFab;

    void Awake()
    {
        Rigidbody2D playerRB = GameObject.FindWithTag("Jeepney").GetComponent<Rigidbody2D>();

        // Just to prevent nullrefernce
        if (playerRB == null) playerRB = GetComponent<Rigidbody2D>();

        Rigidbody2D enemyRB = GetComponent<Rigidbody2D>();

        EnemyRifle rifleWeapon = transform.GetChild(2).GetComponent<EnemyRifle>();
        rifleWeapon.make(bulletFab, playerRB, transform);

        base.make(bulletFab, playerRB, enemyRB, rifleWeapon);
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