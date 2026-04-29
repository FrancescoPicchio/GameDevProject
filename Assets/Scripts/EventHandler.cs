using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    //TODO add synchronization between enemies and player
    //TODO add a way for enemies to declare where they want to go and then check if there are collision, resolving them with visitor
    //TODO add a way to support collision between more than 2 enemies
    public UnityEvent enemyTurn;
    public UnityEvent playerTurn;
    private int numberOfEnemies = 0;
    private int finishedEnemies = 0;

    void Start() { }

    void Update() { }

    public void subscribeEnemy(ObjectWithCollision enemy)
    {
        enemyTurn.AddListener(enemy.LookForObjectWithCollision);
        numberOfEnemies++;
    }

    //to make sure that subscriber count is consistent
    public void unsubscribeEnemy()
    {
        numberOfEnemies--;
    }
    //TODO synchronize this method to not let player move while enemies move
    public void callEnemies()
    {
        enemyTurn.Invoke();
    }

    //keeps tracks of how many enemies have finished their turn
    public void finishEnemyTurn()
    {
        finishedEnemies++;
        if(finishedEnemies == numberOfEnemies){
            playerTurn.Invoke();
            finishedEnemies = 0;
        }
    }
}
