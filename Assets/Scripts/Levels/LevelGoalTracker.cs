using UnityEngine;

public class LevelGoalTracker : MonoBehaviour
{
    [SerializeField] private LevelDefinition levelDefinition;

    private int passengersDropped;
    private int enemiesKilled;
    private bool levelCompleted;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER, OnDropPassenger);
        EventBroadcaster.Instance.AddObserver(EventNames.KILL_ENEMY, OnKillEnemy);
    }

    private void Start()
    {
        if (levelDefinition == null)
        {
            Debug.LogWarning("LevelGoalTracker has no LevelDefinition assigned.");
            return;
        }

        Debug.Log(
            "Level Start: " +
            levelDefinition.SceneName +
            " | Passengers Required: " +
            levelDefinition.RequiredPassengersDropped +
            " | Enemies Required: " +
            levelDefinition.RequiredEnemiesKilled
        );
    }

    void OnDropPassenger(Parameters parameters)
    {
        if (levelCompleted || levelDefinition == null) return;

        passengersDropped++;
        Debug.Log(
            "Level Progress | Passengers: " +
            passengersDropped +
            "/" +
            levelDefinition.RequiredPassengersDropped +
            " | Enemies: " +
            enemiesKilled +
            "/" +
            levelDefinition.RequiredEnemiesKilled
        );

        CheckLevelCompletion();
    }

    void OnKillEnemy()
    {
        if (levelCompleted || levelDefinition == null) return;

        enemiesKilled++;
        Debug.Log(
            "Level Progress | Passengers: " +
            passengersDropped +
            "/" +
            levelDefinition.RequiredPassengersDropped +
            " | Enemies: " +
            enemiesKilled +
            "/" +
            levelDefinition.RequiredEnemiesKilled
        );

        CheckLevelCompletion();
    }

    void CheckLevelCompletion()
    {
        if (
            passengersDropped < levelDefinition.RequiredPassengersDropped ||
            enemiesKilled < levelDefinition.RequiredEnemiesKilled
        ) return;

        levelCompleted = true;
        Debug.Log("Level Complete: " + levelDefinition.SceneName);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.DROP_PASSENGER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.KILL_ENEMY);
    }
}
