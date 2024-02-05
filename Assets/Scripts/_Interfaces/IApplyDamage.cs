using UnityEngine;

public interface IApplyDamage
{
    int damage { get; set; }

    void ApplyDamage(GameObject collisionObject);
}
