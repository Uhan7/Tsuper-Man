using UnityEngine;

public class ShotugnEnemy : EnemyBase
{
    [SerializeField] GameObject bulletFab;

    void Awake()
    {
        Rigidbody2D playerRB = GameObject.FindWithTag("Jeepney").GetComponent<Rigidbody2D>();

        // Just to prevent nullrefernce
        if (playerRB == null) playerRB = GetComponent<Rigidbody2D>();

        Rigidbody2D enemyRB = GetComponent<Rigidbody2D>();

        EnemyShotgun shotgunWeapon = transform.GetChild(2).GetComponent<EnemyShotgun>();
        shotgunWeapon.make(bulletFab, playerRB, transform);

        base.make(bulletFab, playerRB, enemyRB, shotgunWeapon);
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