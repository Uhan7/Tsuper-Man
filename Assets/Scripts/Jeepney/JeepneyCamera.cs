using UnityEngine;
using Unity.Cinemachine;

public class JeepneyCamera : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private CinemachineCamera jeepneyCamera;
    [SerializeField] private JeepneyMovement jeepneyMoveScript;

    [SerializeField] private float minimumLensSize;
    [SerializeField] private float maximumLensSize;

    private float currentCameraLensSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        jeepneyMoveScript = GetComponent<JeepneyMovement>();
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
        // Calculate target size based on input
        float targetSize = Mathf.Clamp(jeepneyMoveScript.accelerateInputTimer + minimumLensSize - 0.2f, minimumLensSize, maximumLensSize);

        // Smoothly interpolate toward target size
        float lerpSpeed = 5f; // Adjust this value for faster/slower zoom
        jeepneyCamera.Lens.OrthographicSize = Mathf.Lerp(
            jeepneyCamera.Lens.OrthographicSize,
            targetSize,
            Time.deltaTime * lerpSpeed
        );
    }
}
