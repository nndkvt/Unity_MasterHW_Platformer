using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class PlayerAtributes : MonoBehaviour, IHaveHealth
{
    [Header("Events")]
    [SerializeField] private UnityEvent _onHitEvent;
    [SerializeField] private UnityEvent _onDeathEvent;

    [Header("Player Variables")]
    public int maxHealth = 100;

    private Animator _animator;

    public int health { get; set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        health = maxHealth;
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        _animator.SetInteger("Health", health);
        _onHitEvent.Invoke();

        if (health <= 0)
        {
            _onDeathEvent.Invoke();
        }
    }

    public void InstantDeath()
    {
        ReceiveDamage(maxHealth);
    }
}
