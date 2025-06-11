using UnityEngine;

public class MinigunEnemy : EnemyBase
{
    [SerializeField] GameObject bulletFab;

    void Awake()
    {
        Rigidbody2D playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        Rigidbody2D enemyRB = GetComponent<Rigidbody2D>();

        EnemyMinigun minigunWeapon = transform.GetChild(2).GetComponent<EnemyMinigun>();
        minigunWeapon.make(bulletFab, playerRB, transform);

        base.make(bulletFab, playerRB, enemyRB, minigunWeapon);
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