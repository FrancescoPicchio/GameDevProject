using UnityEngine;

public class Pit : MonoBehaviour
{
    //TODO look if there's a method to use Grid to check if there's something above a Pit
    //TODO maybe synchronize this check with eventHandler
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("YOU FELL FOR IT FOOL!");
        // if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Player"))
        // {
        //     Debug.Log("YOU FELL FOR IT FOOL!");
        // }
    }
}
