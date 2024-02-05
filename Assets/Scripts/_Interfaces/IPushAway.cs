using UnityEngine;

public interface IPushAway
{
    float pushForce { get; set; }

    void PushAway(GameObject collisionObject);
}
