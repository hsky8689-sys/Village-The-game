using UnityEngine;

public class item_weapon : Item
{
    [SerializeField] protected bool forAttack;
    [SerializeField] protected float damage;
    [SerializeField] protected float cooldown;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

}
