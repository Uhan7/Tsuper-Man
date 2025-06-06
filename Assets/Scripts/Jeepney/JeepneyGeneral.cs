using UnityEngine;

public class JeepneyGeneral : MonoBehaviour
{
    [SerializeField] private int HP;

    private void Awake()
    {
        //EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER_TRUE);
        //EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER_TRUE);
        //EventBroadcaster.Instance.AddObserver(EventNames.PICK_PASSENGER_FALSE);
        //EventBroadcaster.Instance.AddObserver(EventNames.DROP_PASSENGER_FALSE);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.PICK_PASSENGER_TRUE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.DROP_PASSENGER_TRUE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.PICK_PASSENGER_FALSE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.DROP_PASSENGER_FALSE);
    }
}
