using UnityEngine;
using UnityEngine.AI;

public class kingScript : MonoBehaviour
{
    public int health =3;
    public float speed = 20f;
    public float distanceforEscape =15f;
    private Transform player;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        EscapefromMonkey();
    }
    void EscapefromMonkey()
    {
        if (player == null) 
        return;
        Vector3 direction =(transform.position - player.position).normalized;
        Vector3 target = transform.position + direction * distanceforEscape;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, 2f, NavMesh.AllAreas))
        {
            print("run");
            agent.SetDestination(hit.position);
        }
    }
    public void kingBit(int damage)
    {
        health -= damage;
        GameManager.instance.UpdateKingHealth( damage);
        speed += 10f;
        agent.speed = speed;
        print("King has" + health);
        if (health <= 0)
        {
            kinghasdied();
        }
    }
    void kinghasdied()
    {
        print("The king has been killed");
        gameObject.SetActive(false);
    }
}
