using UnityEngine;

public class AliveForSeconds : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _aliveSeconds = 3f;

    private float _awakeTime;

    private void Awake()
    {
        _awakeTime = Time.time;
    }

    private void Update()
    {
        if (_awakeTime + _aliveSeconds < Time.time)
        {
            gameObject.SetActive(false);
        }
    }
}
