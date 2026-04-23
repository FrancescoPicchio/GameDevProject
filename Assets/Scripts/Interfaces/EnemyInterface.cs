using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyInterface : MonoBehaviour
{
    private EventHandler eventHandler;
    public UnityEvent finishedTurn;

    //TODO find a way to put this inside Start() and make derived classes call it automatically
    protected virtual void subscribe()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler){
            eventHandler.subscribeEnemy(this);
            finishedTurn.AddListener(eventHandler.finishEnemyTurn);
        }
        else
            Debug.Log("Couldn't find EventHandler");
    }

    public abstract void Move();

    ~EnemyInterface(){
        eventHandler.unsubscribeEnemy();
    }
}
