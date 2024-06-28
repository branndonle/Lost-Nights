using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;  
public class EnemyScript : MonoBehaviour
{
    public AudioSource gameover;
    public Transform player;  // Reference to the player GameObject
    private NavMeshAgent agent;
    public float moveSpeed = 5f;  // Movement speed of the NPC

    private int currentWaypoint = 0;  // Current waypoint index

    private Animator anim;



    private void Start()
    {
        gameover = GetComponent<AudioSource>();
        player = GameObject.Find("Peasant Nolant(Free Version)").transform;
        anim = GetComponent<Animator>();
        agent = this.gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        agent.speed = moveSpeed;


    }

    private void Update()
    {
        // Move the NPC towards the current waypoint
        agent.SetDestination(player.position);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Z_Idle"))
        {
            agent.speed = 0;

        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Z_Walk"))
        {
            agent.speed = moveSpeed;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Z_Run"))
        {
            agent.speed = moveSpeed * 1.5f;
        }
     



    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger Entered by: " + other.name);
            SceneManager.LoadScene("LoseScreen");
            gameover.Play();
        }
    }

}
