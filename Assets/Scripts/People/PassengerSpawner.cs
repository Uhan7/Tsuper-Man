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

    [SerializeField] private float spawnArea;
    [SerializeField] private bool disableCertainSpawns;

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

    void SpawnPassenger(int passengerIndex, int transformIndex)
    {
        if (disableCertainSpawns)
        {
            while (transformIndex == passengerIndex)
            {
                transformIndex = Random.Range(0, NUMBER_OF_PASSENGERS);
            }
        }

        Instantiate(passengers[passengerIndex], GetRandomPointInSquare(spawnLocations[transformIndex], spawnArea, spawnArea), transform.rotation);
    }

    public Vector2 GetRandomPointInSquare(Transform center, float width, float height)
    {
        float x = Random.Range(center.position.x - width / 2f, center.position.x + width / 2f);
        float y = Random.Range(center.position.y - height / 2f, center.position.y + height / 2f);
        return new Vector2(x, y);
    }

}
