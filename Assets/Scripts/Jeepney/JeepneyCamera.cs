using UnityEngine;
using Unity.Cinemachine;

public class JeepneyCamera : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private CinemachineCamera jeepneyCamera;

    [SerializeField] private float minimumLensSize;
    [SerializeField] private float maximumLensSize;

    private float currentCameraLensSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Start at minimum lens size
        jeepneyCamera.Lens.OrthographicSize = minimumLensSize;
    }

    void Update()
    {
        ChangeLensSize();
    }

    // Update Functions --------------------------------------------------------

    void ChangeLensSize()
    {
        float scaleValue = 2 * (rb.linearVelocity.magnitude + minimumLensSize-1) * (maximumLensSize-1) / GetComponent<JeepneyMovement>().normalSpeedMax;

        jeepneyCamera.Lens.OrthographicSize = Mathf.Clamp(scaleValue, minimumLensSize, maximumLensSize);
    }
}
