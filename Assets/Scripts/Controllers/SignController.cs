using UnityEngine;

public class SignController : MonoBehaviour
{
    [SerializeField] private GameObject _signText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _signText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _signText.SetActive(false);
        }
    }
}
