using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyInterface : MonoBehaviour
{
    protected EventHandler eventHandler;

    public UnityEvent finishedTurn;

    //TODO find a way to put this inside Start() and make derived classes call it automatically
    protected virtual void subscribe()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
        {
            eventHandler.subscribeEnemy(this);
            finishedTurn.AddListener(eventHandler.finishedEnemyTurn);
        }
        else
            Debug.Log("Couldn't find EventHandler");
    }

    //NEED to call this before destroying an enemy
    public virtual void unsubscribe()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
        {
            eventHandler.unsubscribeEnemy(CoordinatesUtil.convert(this.transform.position));
        }
        else
            Debug.Log("Couldn't find EventHandler");
    }

    public abstract void Move();
}
