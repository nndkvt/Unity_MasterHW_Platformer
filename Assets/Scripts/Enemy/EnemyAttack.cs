using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour, IApplyDamage, IPushAway
{
    [SerializeField] private GameObject _attackZone;

    [Header("Enemy attributes")]
    [SerializeField, Range(750f, 1500f)] private float _startPushForce = 1000f;
    [SerializeField, Range(5, 30)] private int _startDamage = 25;

    private Animator _animator;

    public int damage { get; set; }
    public float pushForce { get; set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        damage = _startDamage;
        pushForce = _startPushForce;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("IsAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(collision.gameObject);
            PushAway(collision.gameObject);
        }
    }

    public void ApplyDamage(GameObject collisionObject)
    {
        collisionObject.GetComponent<IHaveHealth>().ReceiveDamage(damage);
        collisionObject.GetComponent<PlayerController>().GetHit();
    }

    public void PushAway(GameObject collisionObject)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 collisionPosition = new Vector2(collisionObject.transform.position.x, collisionObject.transform.position.y);
        collisionObject.GetComponent<Rigidbody2D>().AddForce((collisionPosition - currentPosition) * pushForce);
    }

    // Методы для аниматора
    private void ActivateAttackZone()
    {
        _attackZone.SetActive(true);
    }
    private void DisableAttackZone()
    {
        _attackZone.SetActive(false);
    }

}
