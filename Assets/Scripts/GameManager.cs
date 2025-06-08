using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER, OnPickPassenger);
        EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER, OnDropPassenger);

        EventBroadcaster.Instance.AddObserver(EventNames.JEEP_DEAD, OnJeepDead);
    }

    void OnPickPassenger()
    {
        print("skrrt");
    }

    void OnDropPassenger()
    {
        print("brrra");
    }

    void OnJeepDead()
    {
        print("RIP");
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PICK_PASSENGER);
        EventBroadcaster.Instance.RemoveObserver(EventNames.DROP_PASSENGER);
    }
}
