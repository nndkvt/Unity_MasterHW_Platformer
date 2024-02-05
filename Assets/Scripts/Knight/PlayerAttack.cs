using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private GameObject _fireBallSpawnPoint;

    private void LaunchFireball()
    {
        Instantiate(_fireBall, _fireBallSpawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
    }
}
