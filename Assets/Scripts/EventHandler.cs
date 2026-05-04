using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//TODO Maybe there's a way to make this static?
public class EventHandler : MonoBehaviour
{
    public UnityEvent enemyTurn;
    public UnityEvent playerTurn;

    //keeps track of the enemies, with their key being associated with their current position before their turn
    private SortedList<float, EnemyInterface> enemies = new SortedList<float, EnemyInterface>();

    //keeps track of the enemies, with their key being associated with their next position
    private SortedList<float, EnemyInterface> enemiesNewPosition =
        new SortedList<float, EnemyInterface>();

    //Can't delete during a loop, so it notes which enemies will have to be deleted
    private List<float> enemiesToDelete = new List<float>();

    //Used to enunciate the loops in the coroutine
    private bool processNextEnemy = false;

    //Used to keep track if we should add or not the current enemy to enemiesNewPosition.
    //It'd be hard to delete that enemy from enemiesNewPosition otherwise
    private bool lastEnemyDied = false;

    public void subscribeEnemy(EnemyInterface enemy)
    {
        enemyTurn.AddListener(enemy.Move);
        enemies.Add(CoordinatesUtil.convert(enemy.transform.position), enemy);
    }

    //to make sure that subscriber count is consistent
    public void unsubscribeEnemy(float enemyPosition)
    {
        enemiesToDelete.Add(enemyPosition);
        //Check so player can still move if alone
        if (enemies.Count == 0)
            playerTurn.Invoke();
        lastEnemyDied = true;
        //Calling this function is equivalent to the enemy invoking the event
        finishedEnemyTurn();
    }

    public void callEnemies()
    {
        //Condition so player can move even if there are no enemies
        if (enemies.Count == 0)
        {
            playerTurn.Invoke();
        }
        else
        {
            StartCoroutine(callEnemiesInOrder());
        }
    }

    //Will be called by enemies invoking their event when they end their turn
    public void finishedEnemyTurn()
    {
        processNextEnemy = true;
    }

    //Need to use System.Collections because otherwise C# compiler thinks it's IEnumerator<T>
    private System.Collections.IEnumerator callEnemiesInOrder()
    {
        foreach (var enemy in enemies.Values)
        {
            lastEnemyDied = false;
            processNextEnemy = false;
            enemy.Move();
            yield return new WaitUntil(() => processNextEnemy);
            if (lastEnemyDied)
                continue;
            enemiesNewPosition.Add(CoordinatesUtil.convert(enemy.transform.position), enemy);
        }

        foreach (var positionToDelete in enemiesToDelete)
        {
            EnemyInterface enemyToDestroy = enemies[positionToDelete];
            //Uses the old position of the enemy
            bool result = enemies.Remove(positionToDelete);
            Destroy(enemyToDestroy.gameObject);
        }

        //Updates enemies list to the enemies with their new position
        enemies = enemiesNewPosition;
        //Can't do enemiesNewPosition.Clear() because enemies has become a shallow copy of enemiesNewPosition
        enemiesNewPosition = new SortedList<float, EnemyInterface>();
        enemiesToDelete.Clear();
        playerTurn.Invoke();
    }

    //static so it can be called without having a reference to eventHandler
    public static void killPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //TODO maybe also synchronize Pit and traps
}
