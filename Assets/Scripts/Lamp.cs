using UnityEngine;
using UnityEngine.Events;

public class Lamp: MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlayerEnterEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _onPlayerEnterEvent.Invoke();
        }
    }
}
