//using UnityEngine;

//public class JeepneyGeneral : MonoBehaviour
//{
//    [SerializeField] private int HP;

//    private void OnTriggerEnter2D(Collider2D col)
//    {
//        switch (col.gameObject.tag)
//        {
//            case "Enemy Bullet":
//                HP -= col.GetComponent<BulletScript>().getDamage();
//                break;

//            default:
//                break;
//        }

//        if (HP <= 0)
//        {
//            EventBroadcaster.Instance.PostEvent(EventNames.JEEP_DEAD);
//            gameObject.SetActive(false);
//        }
//    }
//}
