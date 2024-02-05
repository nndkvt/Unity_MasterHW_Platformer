using UnityEngine;

public class AttackZoneController : MonoBehaviour, IApplyDamage, IPushAway
{
    [SerializeField, Range(5, 30)] private int _startDamage = 15;
    [SerializeField, Range(50, 75)] private float _startPushForce = 75f;

    public int damage { get; set; }
    public float pushForce { get; set; }

    private void Start()
    {
        damage = _startDamage;
        pushForce = _startPushForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
}
