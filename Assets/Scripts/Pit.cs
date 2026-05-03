using UnityEngine;

public class Pit : MonoBehaviour
{
    //TODO look if there's a method to use Grid to check if there's something above a Pit
    //TODO maybe synchronize this check with eventHandler
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Player"))
        {
            EventHandler eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
            if (eventHandler){
                eventHandler.unsubscribeEnemy();
            }
            else
                Debug.Log("Couldn't find EventHandler");
            Destroy(other.gameObject);
        }
    }
}
