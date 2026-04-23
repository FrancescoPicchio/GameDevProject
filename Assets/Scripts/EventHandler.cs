using UnityEngine;
using UnityEngine.Events;

//TODO Maybe there's a way to make this static?
public class EventHandler : MonoBehaviour
{
    public UnityEvent enemyTurn;

    void Start() { }

    void Update() { }

    public void subscribeEnemy(EnemyInterface enemy)
    {
        enemyTurn.AddListener(enemy.Move);
    }

    //TODO synchronize this method to not let player move while enemies move
    public void callEnemies()
    {
        enemyTurn.Invoke();
    }

    //TODO maybe also synchronize Pit and traps
}
