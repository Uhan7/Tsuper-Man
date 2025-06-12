using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Constants
    private const int NUMBER_OF_LOCATIONS = 4;

    // Audio
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip pickPassengerSFX;
    [SerializeField] private AudioClip dropPassengerSFX;

    [SerializeField] private AudioClip[] pickPassengerVoiceSFXs;
    [SerializeField] private AudioClip[] dropPassengerVoiceSFXs;

    [SerializeField] private AudioClip jeepHurtSFX;
    [SerializeField] private AudioClip jeepDeadSFX;
    [SerializeField] private AudioClip personDeadSFX;

    // Other GameObjects
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject jeepney;
    private ShooterController jeepneyShooterScript;

    [SerializeField] private GameObject deathMenu;

    // Spawning
    [SerializeField] private GameObject[] spawners;

    // Top Section
    [SerializeField] private Image hpFill;
    [SerializeField] private TextMeshProUGUI hpPercentText;

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
        // Broadcasting
        EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER, OnPickPassenger);
        EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER, OnDropPassenger);

        EventBroadcaster.Instance.AddObserver(EventNames.JEEP_HURT, OnJeepHurt);
        EventBroadcaster.Instance.AddObserver(EventNames.JEEP_DEAD, OnJeepDead);

        EventBroadcaster.Instance.AddObserver(EventNames.KILL_ENEMY, OnKillEnemy);

        // Component Initialization
        jeepneyShooterScript = jeepney.GetComponent<ShooterController>();
    }

    private void Start()
    {
        deathMenu.SetActive(false);

        passengersDropped = 0;

        for (int i = 0; i < NUMBER_OF_LOCATIONS; i++)
        {
            counterTexts[i].text = "0";
            //counterValues[i] = 0;
        }
    }

    private void Update()
    {
        hpFill.fillAmount = jeepneyShooterScript.Hp / 100f;
        hpPercentText.text = jeepneyShooterScript.Hp.ToString() + "%";
    }

    void OnPickPassenger(Parameters parameters)
    {
        int passengerID = parameters.GetIntExtra(ParamNames.PASSENGER_ID, -1);
        ChangeCounters(passengerID - 1, 1);

        sfxSource.PlayOneShot(pickPassengerSFX);

        sfxSource.PlayOneShot(pickPassengerVoiceSFXs[Random.Range(0, pickPassengerVoiceSFXs.Length-1)]);
    }

    void OnDropPassenger(Parameters parameters)
    {
        int passengerID = parameters.GetIntExtra(ParamNames.PASSENGER_ID, -1);
        ChangeCounters(passengerID - 1, -1);

        passengersDropped++;
        passengersIcon.GetComponent<Animator>().Play("pulse");
        passengersText.text = passengersDropped.ToString();

        sfxSource.PlayOneShot(dropPassengerSFX);

        sfxSource.PlayOneShot(dropPassengerVoiceSFXs[Random.Range(0, dropPassengerVoiceSFXs.Length-1)]);
    }

    void OnJeepHurt()
    {
        mainCam.GetComponent<camera_shake>().ScreenShakeWrapper();
        sfxSource.PlayOneShot(jeepHurtSFX);
    }

    void OnJeepDead()
    {
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }

        GameObject[] passengers = GameObject.FindGameObjectsWithTag("Passenger");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        foreach (GameObject passenger in passengers)
        {
            Destroy(passenger);
        }

        deathMenu.SetActive(true);
        sfxSource.PlayOneShot(jeepDeadSFX);
    }

    void OnKillEnemy()
    {
        enemiesKilled++;
        if (enemiesIcon != null) enemiesIcon.GetComponent<Animator>().Play("pulse");
        enemiesText.text = enemiesKilled.ToString();

        if (sfxSource != null) sfxSource.PlayOneShot(personDeadSFX);
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
