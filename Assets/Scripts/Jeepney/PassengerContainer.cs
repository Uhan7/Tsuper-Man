using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PassengerContainer : MonoBehaviour
{
    [SerializeField] private int maxPassengers;

    [SerializeField] private GameObject[] passengerIndicators;
    [SerializeField] private GameObject passengerIndicatorHolder;

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

            Instantiate(passengerIndicators[passenger.passengerData.ID-1], passengerIndicatorHolder.transform);

            Parameters updateParameters = new Parameters();
            updateParameters.PutExtra(ParamNames.PASSENGER_ID, passenger.passengerData.ID);
            EventBroadcaster.Instance.PostEvent(EventNames.PICK_PASSENGER, updateParameters);

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
            PassengerData passengerData = passengerQueue.Dequeue();
            if (passengerData.ID == dropID)
            {
                foreach (Indicator indicator in passengerIndicatorHolder.GetComponentsInChildren<Indicator>())
                {
                    if (dropID == indicator.passengerData.ID) Destroy(indicator);
                }

                Parameters updateParameters = new Parameters();
                updateParameters.PutExtra(ParamNames.PASSENGER_ID, passengerData.ID);
                EventBroadcaster.Instance.PostEvent(EventNames.DROP_PASSENGER, updateParameters);

                Debug.Log($"Dropped off passenger ID: {passengerData.ID}");
            }
            else
            {
                newQueue.Enqueue(passengerData);
            }
        }

        passengerQueue = newQueue;

        if (originalCount == passengerQueue.Count)
            Debug.Log("No matching passengers to drop off.");

    }
}
