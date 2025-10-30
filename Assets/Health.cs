using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float max_health;
    private StageManager center;
    void Awake()
    {
        health = max_health;
        center = GetComponent<StageManager>();
    }

    public void heal(float amount)
    {
        health = Mathf.Max(max_health, health + amount);
    }

    public void damage(float amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
