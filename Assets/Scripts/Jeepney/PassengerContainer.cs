using UnityEngine;
using System.Collections.Generic;

public class PassengerContainer : MonoBehaviour
{
    [SerializeField] private int maxPassengers;

    private Queue<PassengerData> passengerQueue = new();

    // Functions ---------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Passenger"))
        {
            if (passengerQueue.Count >= maxPassengers)
            {
                Debug.Log("Passenger queue full!");
                return;
            }

            Passenger passenger = col.GetComponent<Passenger>();
            passengerQueue.Enqueue(passenger.passengerData);
            Destroy(col.gameObject);
            UpdateUI();
        }
        else if (col.CompareTag("Drop Location"))
        {
            if (passengerQueue.Count == 0)
            {
                Debug.Log("No passengers to drop off!");
                return;
            }

            DropLocation drop = col.GetComponent<DropLocation>();
            DropAllMatchingPassengers(drop.ID);
        }
    }

    // Other Functions ---------------------------------------------------------

    void DropAllMatchingPassengers(int dropID)
    {
        int originalCount = passengerQueue.Count;
        Queue<PassengerData> newQueue = new();

        while (passengerQueue.Count > 0)
        {
            PassengerData passenger = passengerQueue.Dequeue();
            if (passenger.ID == dropID)
            {
                Debug.Log($"Dropped off passenger: {passenger.ID}");
                // Event Broadcast this part, I want to use it for game manager to store the data
            }
            else
            {
                newQueue.Enqueue(passenger);
            }
        }

        passengerQueue = newQueue;

        if (originalCount == passengerQueue.Count)
            Debug.Log("No matching passengers to drop off.");

        UpdateUI();
    }

    void UpdateUI()
    {
        // update the queue UI if needed
    }
}
