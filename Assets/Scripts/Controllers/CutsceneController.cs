using System.Collections;
using UnityEngine;

public class CutsceneController: MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _cutsceneTime;

    private PlayerController _player;
    private IEnumerator _cutsceneCoroutine;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _cutsceneCoroutine = PlayCutscene(_cutsceneTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(_cutsceneCoroutine);
        }
    }

    private IEnumerator PlayCutscene(float cutsceneTime)
    {
        _animator.SetBool("cutscenePlay", true);
        _player.ChangeInputAccess();

        yield return new WaitForSeconds(cutsceneTime);

        _animator.SetBool("cutscenePlay", false);
        _player.ChangeInputAccess();
        Destroy(gameObject);
    }
}
