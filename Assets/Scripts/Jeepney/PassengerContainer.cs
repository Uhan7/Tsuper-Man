using UnityEngine;

public class PassengerContainer : MonoBehaviour
{
    [SerializeField] private Passenger passenger;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Drop Location") return;

        if (passenger.ID == col.gameObject.GetComponent<DropLocation>().ID)
        {
            DropPassenger(passenger);
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }

    // Update Functions --------------------------------------------------------

    void DropPassenger(Passenger passenger)
    {
        Debug.Log("Success");
    }
}
