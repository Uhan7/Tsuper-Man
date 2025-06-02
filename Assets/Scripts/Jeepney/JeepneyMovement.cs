using UnityEngine;

public class JeepneyMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;

    // Values
    [SerializeField] private float normalSpeed;
    [SerializeField] private float normalSpeedMax;
    [SerializeField] private float normalTurningSpeed;

    [SerializeField] private float boostSpeed;
    [SerializeField] private float boostSpeedMax;
    [SerializeField] private float boostTurningSpeed;

    [SerializeField] private float deceleratingSpeed;
    [SerializeField] private float deceleratingSpeedMax;

    [SerializeField] private float friction;

    // Inputs
    [SerializeField] private KeyCode accelerateKey;
    [SerializeField] private KeyCode decelerateKey;
    [SerializeField] private KeyCode boostKey;

    [SerializeField] private KeyCode debugKey;

    // Flags
    private bool accelerating;
    private bool decelerating;
    private bool boosting;

    private bool canGoForward;
    private bool canGoBackward;

    // Functions ---------------------------------------------------------------

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        InputCheck();

        if (Input.GetKeyDown(debugKey))PrintDebugs();
    }

    private void FixedUpdate()
    {
        if (decelerating) Decelerate();
        else if (accelerating) Accelerate();
        else ApplyFriction();
        LimitSpeed();
    }

    // Update Functions --------------------------------------------------------

    void InputCheck()
    {
        accelerating = Input.GetKey(accelerateKey);
        decelerating = Input.GetKey(decelerateKey);
        boosting = Input.GetKey(boostKey);
    }

    // FixedUpdate Functions ---------------------------------------------------

    void Accelerate()
    {
        float speed = normalSpeed; // Temporarily

        if (canGoForward) rb.AddForce(transform.up * normalSpeed, ForceMode2D.Force);
    }

    void Decelerate()
    {
        ApplyFriction();
        rb.AddForce(-transform.up * deceleratingSpeed, ForceMode2D.Force);
    }

    void ApplyFriction()
    {
        rb.linearVelocity *= friction;
    }

    void LimitSpeed()
    {
        float maxSpeed = normalSpeedMax; // Temporarily

        if (rb.linearVelocity.magnitude > maxSpeed) canGoForward = false;
        else canGoForward = true;

        if (rb.linearVelocity.magnitude > deceleratingSpeedMax) canGoBackward = false;
        else canGoBackward = true;
    }

    // Debug Help --------------------------------------------------------------

    void PrintDebugs()
    {
        Debug.Log(rb.transform.up);
    }
}
