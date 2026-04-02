using UnityEngine;

public class Boulder : MonoBehaviour, IMovable
{
    public bool TryMove(Vector3 direction)
    {
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);
        
        if (col != null && col.TryGetComponent<IMovable>(out IMovable other))
        {
            if (other.TryMove(direction))
            {
                transform.position+=direction;
                return true;
            }
            return false;
        }
        transform.position+=direction;
        return true;
    }
}