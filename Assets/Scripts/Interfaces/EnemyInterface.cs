using UnityEngine;

public abstract class EnemyInterface : MonoBehaviour
{
    private EventHandler eventHandler;

    protected virtual void subscribe()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
            eventHandler.subscribeEnemy(this);
        else
            Debug.Log("Couldn't find EventHandler");
    }

    public abstract void Move();
}
