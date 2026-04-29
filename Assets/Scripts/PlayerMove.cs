using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.Mathematics;

public class PlayerMove : MonoBehaviour
{
    private InputAction moveAction,attackInput;
    private Rigidbody rb;
    private Vector2 moveVector;
    private Vector3 startPos;
    public float speed;
    public float attackcd = 10f;
    public float previousAttack = -10f;
    public float attackradius = 1.5f;
    public float attackrange = 1f;
    public bool useForce = false;
    public TextMeshProUGUI attackready;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackInput = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        print(moveVector);
        cooldown();

        if (attackInput.WasReleasedThisFrame())
        {
            monkeyAttack();
        }

        

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
    void cooldown()
    {
        float timeleft = attackcd - (Time.time - previousAttack);
        if(timeleft > 0)
        {
            attackready.text = math.ceil(timeleft).ToString();
        }
        else
        {
            attackready.text = "Ready";
        }
    }
}
