using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float dmg = 1f;

    public void setDamage(float damage)
    {
        dmg = damage;
    }
    public float getDamage()
    {
        return dmg;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
