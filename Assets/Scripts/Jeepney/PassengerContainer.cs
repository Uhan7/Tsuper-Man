using UnityEngine;

public class PassengerContainer : MonoBehaviour
{
    [SerializeField] private PassengerData passengerData;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Passenger":
                Passenger passenger = col.gameObject.GetComponent<Passenger>();
                passengerData = passenger.passengerData;
                Destroy(col.gameObject);
                break;

            case "Drop Location":
                if (passengerData != null)
                {
                    if (passengerData.ID == col.gameObject.GetComponent<DropLocation>().ID) DropPassenger(passengerData);
                    else Debug.Log("wrong");
                }
                break;

            default: break;
        }
    }

    // Update Functions --------------------------------------------------------

    void DropPassenger(PassengerData passengerData)
    {
        Debug.Log("Success");
    }
}
