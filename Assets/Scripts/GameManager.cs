using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Constants
    private const int NUMBER_OF_LOCATIONS = 4;

    // Variables
    [SerializeField] private GameObject mainCam;

    // Spawning
    [SerializeField] private GameObject[] spawners;

    // Top Section
    [SerializeField] private GameObject hpSection;

    // Bottom Right
    [SerializeField] private GameObject[] counters;
    [SerializeField] private TextMeshProUGUI[] counterTexts;
    [SerializeField] private int[] counterValues;

    [SerializeField] private GameObject enemiesIcon;
    [SerializeField] private TextMeshProUGUI enemiesText;

    [SerializeField] private GameObject passengersIcon;
    [SerializeField] private TextMeshProUGUI passengersText;

    private int passengersDropped;
    private int enemiesKilled;

    // Functions
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER, OnPickPassenger);
        EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER, OnDropPassenger);

        EventBroadcaster.Instance.AddObserver(EventNames.JEEP_HURT, OnJeepHurt);
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
        passengersIcon.GetComponent<Animator>().Play("pulse");
        passengersText.text = passengersDropped.ToString();

    }

    void OnJeepHurt()
    {
        mainCam.GetComponent<camera_shake>().ScreenShakeWrapper();
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
        enemiesIcon.GetComponent<Animator>().Play("pulse");
        enemiesText.text = enemiesKilled.ToString();
    }

    void ChangeCounters(int index, int appendValue)
    {
        counterValues[index] += appendValue;
        counters[index].GetComponent<Animator>().Play("pulse");
        counterTexts[index].text = counterValues[index].ToString();
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PICK_PASSENGER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.DROP_PASSENGER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.JEEP_HURT);
        EventBroadcaster.Instance.RemoveObserver(EventNames.JEEP_DEAD);
        EventBroadcaster.Instance.RemoveObserver(EventNames.KILL_ENEMY);
    }
}
