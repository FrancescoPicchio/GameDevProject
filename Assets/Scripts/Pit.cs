using UnityEngine;

public class Pit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name +" è caduto nello sburro");
    }
}
