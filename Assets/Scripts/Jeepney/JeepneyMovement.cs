using UnityEngine;

public class JeepneyMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;

    // Checks
    [SerializeField] private float minimumSpeedForTurn;

    // Values
    [SerializeField] private float normalSpeed;
    public float normalSpeedMax; // Used in JeepneyCamera.cs
    [SerializeField] private float normalTurningSpeed;
    [SerializeField] private float turningSpeedMax;

    //[SerializeField] private float boostSpeed;
    //[SerializeField] private float boostSpeedMax;
    //[SerializeField] private float boostTurningSpeed;

    [SerializeField] private float brakeFriction;

    [SerializeField] private float deceleratingSpeed;
    [SerializeField] private float deceleratingSpeedMax;

    [SerializeField] private float friction;
    [SerializeField] private float turningFriction;

    private float directionFactor;
    [SerializeField] private float turningDecay;

    // Inputs
    [SerializeField] private KeyCode accelerateKey;
    [SerializeField] private KeyCode decelerateKey;
    [SerializeField] private KeyCode turnRightKey;
    [SerializeField] private KeyCode turnLeftKey;

    [SerializeField] private KeyCode boostKey;

    [SerializeField] private KeyCode debugKey;

    // Regarding Time
    [HideInInspector] public float accelerateInputTimer; // To be used in JeepneyCamera.cs

    [SerializeField] private float decelerateInputTime;
    private float decelerateInputTimer;

    // Flags
    private bool accelerating;
    private bool decelerating;
    private bool turningRight;
    private bool turningLeft;

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
        TimerCheck();

        if (Input.GetKeyDown(debugKey)) PrintDebugs();
    }

    private void FixedUpdate()
    {
        if (decelerating)
        {
            if (decelerateInputTimer > decelerateInputTime) Decelerate();
            else Brake();
        }
        else if (accelerating) Accelerate();
        else ApplyFriction();

        if (turningRight) Turn('R');
        else if (turningLeft) Turn('L');
        else directionFactor = 0;

        ApplyTurningFriction();
        if (turningLeft || turningRight) ApplyLateralFriction(2f);

        LimitSpeed();
    }

    // Update Functions --------------------------------------------------------

    void InputCheck()
    {
        decelerating = Input.GetKey(decelerateKey);
        accelerating = Input.GetKey(accelerateKey);
        if (decelerating) accelerating = false;
        boosting = Input.GetKey(boostKey);

        turningRight = Input.GetKey(turnRightKey);
        turningLeft = Input.GetKey(turnLeftKey);
    }

    void TimerCheck()
    {
        if (decelerating) decelerateInputTimer += Time.deltaTime;
        else decelerateInputTimer = 0;

        if (accelerating) accelerateInputTimer += Time.deltaTime;
        else accelerateInputTimer = 0;
    }

    // FixedUpdate Functions ---------------------------------------------------

    void Accelerate()
    {
        float speed = normalSpeed; // Temporarily

        if (canGoForward) rb.AddForce(rb.transform.up * normalSpeed, ForceMode2D.Force);
    }

    void Brake()
    {
        ApplyFriction();
        rb.linearVelocity *= brakeFriction;
    }

    void Decelerate()
    {
        ApplyFriction();
        rb.AddForce(-rb.transform.up * deceleratingSpeed, ForceMode2D.Force);
    }

    void Turn(char direction)
    {
        float turnSpeed = normalTurningSpeed; // Temporarily
        float maxSpeed = normalSpeedMax; // Temporarily

        if (direction == 'R' && directionFactor > 0 || direction == 'L' && directionFactor < 0) directionFactor = 0;

        if (direction == 'R') directionFactor -= turningDecay;
        else if (direction == 'L') directionFactor += turningDecay;

        if (decelerateInputTimer > decelerateInputTime)
        {
            if (decelerating && directionFactor > 0) directionFactor = -1;
            else if (decelerating && directionFactor < 0) directionFactor = 1;
        }

        directionFactor = Mathf.Clamp(directionFactor, -1, 1);

        // Guard Clause, so we can retain the wheel turn but not actually turn it
        if (rb.linearVelocity.magnitude < minimumSpeedForTurn) return;

        float turnScale = Mathf.Clamp(rb.linearVelocity.magnitude * 1.5f, -turningSpeedMax, turningSpeedMax) / turningSpeedMax;

        rb.angularVelocity = (30f + turnSpeed * turnScale) * directionFactor;

        if (turningLeft || turningRight) rb.linearVelocity *= turningFriction;
    }

    void ApplyFriction()
    {
        rb.linearVelocity *= friction;
    }

    void ApplyTurningFriction()
    {
        Vector2 forward = rb.transform.up;
        float forwardSpeed = Vector2.Dot(rb.linearVelocity, forward);
        rb.linearVelocity = forward * forwardSpeed;
    }

    void ApplyLateralFriction(float intensity)
    {
        Vector2 right = rb.transform.right;
        float lateralSpeed = Vector2.Dot(rb.linearVelocity, right);
        Vector2 lateralVelocity = right * lateralSpeed;

        rb.AddForce(-lateralVelocity * intensity, ForceMode2D.Force);
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
        Debug.Log(rb.angularVelocity);
    }
}
