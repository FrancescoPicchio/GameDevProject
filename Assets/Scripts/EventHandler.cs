using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public UnityEvent enemyTurn;

    void Start() { }

    void Update() { }

    public void subscribeEnemy(EnemyInterface enemy)
    {
        enemyTurn.AddListener(enemy.Move);
    }

    public void callEnemies()
    {
        enemyTurn.Invoke();
    }
}
