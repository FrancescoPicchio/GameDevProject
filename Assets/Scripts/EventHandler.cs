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
    private int finishedEnemies = 0;
    private SortedList<float, EnemyInterface> enemies = new SortedList<float, EnemyInterface>();
    private SortedList<float, EnemyInterface> enemiesNewPosition =
        new SortedList<float, EnemyInterface>();
    private List<float> enemiesToDelete = new List<float>();
    private bool processNextEnemy = false;
    private bool lastEnemyDied = false;

    void Start() { }

    void Update() { }

    public void subscribeEnemy(EnemyInterface enemy)
    {
        enemyTurn.AddListener(enemy.Move);
        enemies.Add(CoordinatesUtil.convert(enemy.transform.position), enemy);
    }

    //to make sure that subscriber count is consistent
    public void unsubscribeEnemy(float enemyPosition)
    {
        enemiesToDelete.Add(enemyPosition);
        if (enemies.Count == 0)
            playerTurn.Invoke();
        lastEnemyDied = true;
        finishedEnemyTurn();
    }

    //TODO synchronize this method to not let player move while enemies move
    public void callEnemies()
    {
        if (enemies.Count == 0)
        {
            playerTurn.Invoke();
        }
        else
        {
            StartCoroutine(callEnemiesInOrder());
        }
    }

    public void finishedEnemyTurn()
    {
        processNextEnemy = true;
    }

    private System.Collections.IEnumerator callEnemiesInOrder()
    {
        foreach (var enemy in enemies.Values)
        {
            lastEnemyDied = false;
            Debug.Log(enemy.name);
            processNextEnemy = false;
            enemy.Move();
            yield return new WaitUntil(() => processNextEnemy);
            if (lastEnemyDied)
                continue;
            enemiesNewPosition.Add(CoordinatesUtil.convert(enemy.transform.position), enemy);
        }

        foreach (var positionToDelete in enemiesToDelete)
        {
            Debug.Log("KILL KILL KILL");
            EnemyInterface enemyToDestroy = enemies[positionToDelete];
            //Uses the old position of the enemy
            bool result = enemies.Remove(positionToDelete);
            Debug.Log(result);
            Destroy(enemyToDestroy.gameObject);
        }

        enemies = enemiesNewPosition;
        //Can't do .Clear because enemies has become a shallow copy of enemiesNewPosition
        enemiesNewPosition = new SortedList<float, EnemyInterface>();
        enemiesToDelete.Clear();
        playerTurn.Invoke();
    }

    //keeps tracks of how many enemies have finished their turn
    public void finishEnemyTurn(EnemyInterface enemy)
    {
        finishedEnemies++;
        float positionKey = CoordinatesUtil.convert(enemy.transform.position);
        enemiesNewPosition.Add(positionKey, enemy);
        if (finishedEnemies >= enemies.Count)
        {
            playerTurn.Invoke();
            finishedEnemies = 0;
            enemies.Clear();
            enemies = enemiesNewPosition;
        }
        else
        {
            enemies.Values[finishedEnemies].Move();
        }
    }

    //static so it can be called without having a reference to eventHandler
    public static void killPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //TODO maybe also synchronize Pit and traps
}
