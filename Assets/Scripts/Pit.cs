using UnityEngine;

public class Pit : MonoBehaviour
{
    //TODO look if there's a method to use Grid to check if there's something above a Pit
    //TODO maybe synchronize this check with eventHandler
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            EventHandler eventHandler = GameObject
                .FindGameObjectWithTag("Logic")
                .GetComponent<EventHandler>();
            EnemyInterface enemy = other.gameObject.GetComponent<EnemyInterface>();
            enemy.unsubscribe();
        }
        else if (other.transform.CompareTag("Player"))
            EventHandler.killPlayer();
    }
}
