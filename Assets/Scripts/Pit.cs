using UnityEngine;

public class Pit : MonoBehaviour
{
    //TODO maybe synchronize this check with eventHandler
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            EventHandler eventHandler = GameObject
                .FindGameObjectWithTag("Logic")
                .GetComponent<EventHandler>();
            //TODO think if you can find a way wher you don't need to use GetComponent
            EnemyInterface enemy = other.gameObject.GetComponent<EnemyInterface>();
            enemy.unsubscribe();
        }
        else if (other.transform.CompareTag("Player"))
            EventHandler.killPlayer();
    }
}
