using UnityEngine;

public class Walls : MonoBehaviour, IMovable
{
    public bool TryMove(Vector3 direction)
    {
        return false;
    }
}