using UnityEngine;

public abstract class ReactingObject : MovingObject
{
    private void OnEnable()
    {
        Player.PlayerActionDone+= Reaction;
    }
    private void OnDisable()
    {
        Player.PlayerActionDone-=Reaction;
    }

    protected abstract void Reaction();
}
