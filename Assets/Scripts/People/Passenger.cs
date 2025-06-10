using UnityEngine;
using Unity.Cinemachine;

public class Passenger : MonoBehaviour
{
    public PassengerData passengerData;

    private int buildingsTouched;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Building") buildingsTouched++;

        if (buildingsTouched >= 1) Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Building") buildingsTouched--;
    }
}
