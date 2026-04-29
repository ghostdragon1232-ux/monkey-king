
using UnityEngine;
using UnityEngine.InputSystem;


public class FirstPersonMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created    private InputAction moveAction;
    private InputAction moveAction;

    private Rigidbody rb;
    private Vector2 moveVector;
    private Vector3 startPos;
    public bool useForce = true;
    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();

    }

    void FixedUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        if (useForce)
        {
            rb.AddForce((moveVector.y * cameraForward) * speed);
            rb.AddForce((moveVector.x * cameraRight) * speed);
        }
        else
        {
            if (moveVector.x == 0 && moveVector.y == 0)
            {
                rb.linearVelocity = new Vector3(0, 0, 0);
            }
            else
            {
                Vector3 moveR = moveVector.x * cameraRight;
                Vector3 moveF = moveVector.y * cameraForward;
                rb.linearVelocity = (moveR + moveF) * speed;
            }
        }

        transform.rotation = Camera.main.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            print("you win");
        }

        if (other.CompareTag("Trap"))
        {
            transform.position = startPos;
        }
    }
}
