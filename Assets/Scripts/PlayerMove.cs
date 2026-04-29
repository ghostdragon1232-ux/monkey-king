
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private InputAction moveAction;
    private Rigidbody rb;
    private Vector2 moveVector;
    private Vector3 startPos;
    public float speed;
    public float attackcd = 10f;
    public float previousAttack = -10f;
    public float attackradius = 1.5f;
    public float attackrange = 1f;
    
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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            monkeyAttack();
        }
    }

    void FixedUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // rb.AddForce((moveVector.y * new Vector3(cameraForward.x, 0, cameraForward.z)) * speed);
        rb.AddForce((moveVector.y * cameraForward) * speed);
        rb.AddForce((moveVector.x * cameraRight) * speed);
        //rb.linearVelocity += new Vector3(moveVector.x * speed, 0, moveVector.y * speed);
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
    void monkeyAttack()
    {
      if  (Time.time - previousAttack < attackcd)
        return;
        Vector3 bitespawn = transform.position + transform.forward * attackrange;
        Collider[] hits = Physics.OverlapSphere(bitespawn,attackradius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("King"))
            {
                Vector3 kingLocation = (hit.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(transform.forward, kingLocation);
                if (dot > 0.5f)
                {
                    kingScript king = hit.GetComponent<kingScript>();
                    if (king != null)
                    {
                        king.kingBit(1);
                        previousAttack = Time.time;
                        break;
                    }
                }
            }
        }
    }
}
