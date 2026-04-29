using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class kingScript : MonoBehaviour
{
    public int health =3;
    public float speed = 20f;
    public float distanceforEscape =15f;
    public Transform player;
    private NavMeshAgent agent;
    public Transform [] kingRunPoints;
    public bool runningAway = false;
    public int maxHealth = 3;
    public Image healthbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        Healthbarchange();
    }

    // Update is called once per frame
    void Update()
    {
        EscapefromMonkey();
        if (Input.GetKeyDown(KeyCode.Space))
    {
        int i = Random.Range(0, kingRunPoints.Length);
        agent.SetDestination(kingRunPoints[i].position);
    }

    }
    void EscapefromMonkey()
    {
        if (player == null) 

        return;
                print(Vector3.Distance(transform.position, player.transform.position));


        if (!runningAway&&Vector3.Distance(transform.position, player.transform.position)<6)
        {
           int randomLocation = Random.Range(0,kingRunPoints.Length);
            runningAway=true;
            agent.SetDestination(kingRunPoints[randomLocation].position);
        }
        else if (agent.remainingDistance < 0.5f)
        {
            runningAway=false;
        }

        /*
        Vector3 direction =(transform.position - player.position).normalized;
        Vector3 target = transform.position + direction * distanceforEscape;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(target, out hit, 0.5f, NavMesh.AllAreas))
        {
            print("run");
            agent.SetDestination(hit.position);
        }
        */
    }
    public void kingBit(int damage)
    {
        health -= damage;
        GameManager.instance.UpdateKingHealth( damage);
        Healthbarchange();
        speed += 10f;
        agent.speed = speed;
        print("King has" + health);
        if (health <= 0)
        {
            kinghasdied();
        }
    }
    void Healthbarchange()
    {
        healthbar.fillAmount = (float)health / maxHealth;
    }
    void kinghasdied()
    {
        print("The king has been killed");
        gameObject.SetActive(false);
    }
}
