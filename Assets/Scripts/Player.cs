using UnityEngine;

public class Player : MonoBehaviour, IObjectWithMovementCollision
{
    private float moveSpeed = 15f;
    private Vector3 targetPosition;

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 direction = GetDirection();
            if (direction != Vector3.zero)
            {
                Collider2D col = Physics2D.OverlapPoint(transform.position + direction);
                if (col != null && col.TryGetComponent<IObjectWithMovementCollision>(out IObjectWithMovementCollision other))
                {
                    if (CollisionSolver.A_MovesOver_B(this, other))
                    {
                        targetPosition=transform.position+direction;
                    }
                }
            }
        }
    }

    Vector3 GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector3.right;
        }

        return Vector3.zero;
    }
}

