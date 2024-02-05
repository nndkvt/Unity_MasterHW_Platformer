using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBall : MonoBehaviour, IApplyDamage, IPushAway
{
    [Header("Fireball attributes")]
    [SerializeField, Range(5, 30)] private int _startDamage = 20;
    [SerializeField, Range(100, 500)] private float _startPushForce = 100f;
    [SerializeField, Range(20, 75)] private float _launchForce = 50f;
    [SerializeField, Range(0, 0.525f)] private float _fireballSpawnOffsetY = 0.175f; 

    private GameObject _player;

    public int damage { get; set; }
    public float pushForce { get; set; }

    private void Awake()
    {
        damage = _startDamage;
        pushForce = _startPushForce;
        _player = GameObject.Find("Knight");

        LaunchFireball();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyDamage(collision.gameObject);
        PushAway(collision.gameObject);

        Destroy(gameObject);
    }

    public void ApplyDamage(GameObject collisionObject)
    {
        if (collisionObject.GetComponent<IHaveHealth>() != null)
        {
            collisionObject.GetComponent<IHaveHealth>().ReceiveDamage(damage);
        }
    }

    public void PushAway(GameObject collisionObject)
    {
        if (collisionObject.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 collisionPosition = new Vector2(collisionObject.transform.position.x, collisionObject.transform.position.y);
            collisionObject.GetComponent<Rigidbody2D>().AddForce((collisionPosition - currentPosition) * pushForce);
        }
    }

    private void LaunchFireball()
    {
        // _fireballSpawnOffsetY - костыль
        // Он меняет значение вектора положения фаербола
        // Без костыля фаербол будет лететь по диагонали
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y - _fireballSpawnOffsetY);
        Vector2 playerPosition = new Vector2(_player.transform.position.x, _player.transform.position.y);
        GetComponent<Rigidbody2D>().AddForce((currentPosition - playerPosition) * _launchForce);
    }
}
