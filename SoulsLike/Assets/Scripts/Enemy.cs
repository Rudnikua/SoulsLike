using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable {

    private const string AGENT_SPEED = "Speed";
    private const string AGENT_ATTACK = "Attack";

    public float health = 100f;

    [Header("Combat Stats")]
    [SerializeField] private float attackCD = 3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float aggroRange = 4f;
    [SerializeField] private GameObject player;
    
    private NavMeshAgent agent;
    private Animator animator;
    private float timePassedBetweenAttacks;
    private float newDestinationCD = 0.5f;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetFloat(AGENT_SPEED, agent.velocity.magnitude / agent.speed);

        if (timePassedBetweenAttacks >= attackCD) {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange) {
                animator.SetTrigger(AGENT_ATTACK);
                timePassedBetweenAttacks = 0;
            }
        }
        timePassedBetweenAttacks += Time.deltaTime;

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange) {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        //transform.LookAt(player.transform.position);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log($"Was hit! Remaining health: {health}");

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);   
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}