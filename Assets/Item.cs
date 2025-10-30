using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected float price;
    [SerializeField] protected float usageCost;
    [SerializeField] protected Ability ability;
    [SerializeField] protected Sprite icon;
    protected Rigidbody2D rb;
    protected Health healthComponent;

    void Awake()
    {
        healthComponent = GetComponent<Health>();
    }
}
