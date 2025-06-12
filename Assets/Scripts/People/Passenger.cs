using UnityEngine;
using Unity.Cinemachine;

public class Passenger : MonoBehaviour
{
    public PassengerData passengerData;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GameObject.Find("SFX Source").GetComponent<AudioSource>().PlayOneShot(passengerData.deadSound);
            Destroy(gameObject);
        }
    }
}
