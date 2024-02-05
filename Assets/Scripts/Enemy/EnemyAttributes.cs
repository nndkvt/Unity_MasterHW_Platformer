using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EnemyAttributes : MonoBehaviour, IHaveHealth
{
    [Header("Attributes")]
    [SerializeField] private int _maxHealth = 75;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHitEvent;
    [SerializeField] private UnityEvent _onDeathEvent;

    public int health { get; set; }

    private void Start()
    {
        health = _maxHealth;
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        _onHitEvent.Invoke();

        if (health <= 0)
        {
            _onDeathEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
