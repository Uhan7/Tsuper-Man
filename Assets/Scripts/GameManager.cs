using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Constants
    private const int NUMBER_OF_LOCATIONS = 4;

    // Variables
    [SerializeField] private GameObject[] counters;
    [SerializeField] private TextMeshProUGUI[] counterTexts;

    [SerializeField] private int[] counterValues;

    private int score;

    // Functions
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER, OnPickPassenger);
        EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER, OnDropPassenger);

        EventBroadcaster.Instance.AddObserver(EventNames.JEEP_DEAD, OnJeepDead);
    }

    private void Start()
    {
        score = 0;

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

        score++;
        Debug.Log(score);
    }

    void OnJeepDead()
    {
        print("RIP");
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
    }
}
