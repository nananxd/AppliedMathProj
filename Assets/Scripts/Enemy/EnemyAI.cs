using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Detection")]
    public float attackRange = 3f;

    [Header("Combat")]
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;
    public int maxHealth = 100;

    [Header("Animation")]
    public Animator animator;

    private int currentHealth;
    private float nextAttackTime;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (player == null || currentHealth <= 0)
            return;

        // Find the distance to the player.
        Vector3 offset = player.position - transform.position;

        // Ignore height differences.
        offset.y = 0f;

        float distance = offset.magnitude;

        // Player is outside the attack range.
        if (distance > attackRange)
            return;

        // Turn toward the player.
        if (offset.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(offset);
        }

        // Attack only when the cooldown has finished.
        if (Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void AttackPlayer()
    {
        if (animator != null)
            animator.SetTrigger("Attack");

        PlayerHealth health =
            player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy defeated!");
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}
