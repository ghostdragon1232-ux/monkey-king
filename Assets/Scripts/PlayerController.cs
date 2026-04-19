using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private Vector2 moveVector;
    private Rigidbody rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the camera's forward direction
        moveVector = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        rb.AddForce((moveVector.y * new Vector3(cameraForward.x, 0, cameraForward.z)) * speed);
        rb.AddForce((moveVector.x * cameraRight) * speed);
        //rb.AddForce(new Vector3(moveVector.x * speed, 0, moveVector.y * speed));
    }

}
