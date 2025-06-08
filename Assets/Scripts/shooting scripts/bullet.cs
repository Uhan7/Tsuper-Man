using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] int dmg = 1;

    public void setDamage(int damage)
    {
        dmg = damage;
    }
    public int getDamage()
    {
        return dmg;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
