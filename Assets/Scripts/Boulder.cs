using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private int moveSpeed = 30;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 nextPosition = Vector2.MoveTowards(
                rb.position,
                targetPosition,
                moveSpeed * Time.fixedDeltaTime
            );
            rb.MovePosition(nextPosition);
            if (Vector2.Distance(rb.position, targetPosition) < 0.01f)
            {
                rb.MovePosition(targetPosition);
                isMoving = false;
            }
        }
    }

    public bool Move(Vector3 direction)
    {
        Collider2D isSomethingBlocking = Physics2D.OverlapCircle(
            transform.position + direction,
            0.4f,
            LayerMask.GetMask("Wall")
        );
        if (isSomethingBlocking)
        {
            targetPosition = transform.position;
            return false;
        }
        targetPosition = transform.position + direction;
        isMoving = true;
        return true;
    }
}
