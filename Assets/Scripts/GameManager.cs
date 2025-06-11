using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Constants
    private const int NUMBER_OF_LOCATIONS = 4;

    // Variables
    [SerializeField] private GameObject[] spawners;

    [SerializeField] private GameObject[] counters;
    [SerializeField] private TextMeshProUGUI[] counterTexts;

    [SerializeField] private int[] counterValues;

    [SerializeField] private Image scoreImage;
    [SerializeField] private Sprite[] scoreImages;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int passengersDropped;
    private int enemiesKilled;

    // Functions
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER, OnPickPassenger);
        EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER, OnDropPassenger);

        EventBroadcaster.Instance.AddObserver(EventNames.JEEP_DEAD, OnJeepDead);

        EventBroadcaster.Instance.AddObserver(EventNames.KILL_ENEMY, OnKillEnemy);
    }

    private void Start()
    {
        passengersDropped = 0;

        for (int i = 0; i < NUMBER_OF_LOCATIONS; i++)
        {
            counterTexts[i].text = "0";
            //counterValues[i] = 0;
        }
    }

    void OnPickPassenger(Parameters parameters)
    {
        int passengerID = parameters.GetIntExtra(ParamNames.PASSENGER_ID, -1);
        ChangeCounters(passengerID - 1, 1);
    }

    void OnDropPassenger(Parameters parameters)
    {
        int passengerID = parameters.GetIntExtra(ParamNames.PASSENGER_ID, -1);
        ChangeCounters(passengerID - 1, -1);

        passengersDropped++;
        Debug.Log(passengersDropped);
    }

    void OnJeepDead()
    {
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }

        print("RIP");
    }

    void OnKillEnemy()
    {
        enemiesKilled++;
        Debug.Log(enemiesKilled);
    }

    void ChangeCounters(int index, int appendValue)
    {
        counterValues[index] += appendValue;
        counterTexts[index].text = counterValues[index].ToString();
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PICK_PASSENGER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.DROP_PASSENGER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.JEEP_DEAD);
        EventBroadcaster.Instance.RemoveObserver(EventNames.KILL_ENEMY);
    }
}
