using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnableState(PlayerController player);

    public abstract void UpdateState(PlayerController player);

    public abstract void OnCollision(PlayerController player);
}
