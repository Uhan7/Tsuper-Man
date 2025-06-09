using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    // Constants
    private const int NUMBER_OF_PASSENGERS = 4;

    [SerializeField] private float minimumTime;
    [SerializeField] private float maximumTime;

    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private float[] spawnTimers;
    [SerializeField] private GameObject[] passengers;

    private void Start()
    {
        SetAllTimers();
    }

    private void Update()
    {
        for (int i = 0; i < NUMBER_OF_PASSENGERS; i++)
        {
            spawnTimers[i] -= Time.deltaTime;

            if (spawnTimers[i] <= 0)
            {
                SpawnPassenger(i, Random.Range(0, spawnLocations.Length));
                SetTimer(i);
            }
        }
    }

    // Helper Functions --------------------------------------------------------

    void SetAllTimers()
    {
        for (int i = 0; i < NUMBER_OF_PASSENGERS; i++)
        {
            SetTimer(i);
        }
    }

    void SetTimer(int index)
    {
        spawnTimers[index] = Random.Range(minimumTime, maximumTime);
    }

    void SpawnPassenger(int passengerIndex, int locationIndex)
    {
        Instantiate(passengers[passengerIndex], spawnLocations[locationIndex].position, spawnLocations[locationIndex].rotation);
    }
}
