using UnityEngine;
using UnityEngine.Events;

public class ObjectCollect : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlayerEnterEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _onPlayerEnterEvent.Invoke();

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _onPlayerEnterEvent.Invoke();

            Destroy(gameObject);
        }
    }
}
