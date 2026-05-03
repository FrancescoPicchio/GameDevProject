using UnityEngine;
using UnityEngine.Events;

//TODO Maybe there's a way to make this static?
public class EventHandler : MonoBehaviour
{
    public UnityEvent enemyTurn;
    public UnityEvent playerTurn;
    private int numberOfEnemies = 0;
    private int finishedEnemies = 0;

    void Start() { }

    void Update() { }

    public void subscribeEnemy(EnemyInterface enemy)
    {
        enemyTurn.AddListener(enemy.Move);
        numberOfEnemies++;
    }

    //to make sure that subscriber count is consistent
    public void unsubscribeEnemy()
    {
        Debug.Log(numberOfEnemies);
        numberOfEnemies--;
        if (numberOfEnemies == 0)
            playerTurn.Invoke();
    }

    //TODO synchronize this method to not let player move while enemies move
    public void callEnemies()
    {
        if (numberOfEnemies == 0)
        {
            playerTurn.Invoke();
        }
        else
        {
            enemyTurn.Invoke();
        }
    }

    //keeps tracks of how many enemies have finished their turn
    public void finishEnemyTurn()
    {
        finishedEnemies++;
        if (finishedEnemies >= numberOfEnemies)
        {
            playerTurn.Invoke();
            finishedEnemies = 0;
        }
    }
    //TODO maybe also synchronize Pit and traps
}
