using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleEnemy : EnemyInterface
{
    public enum Axis
    {
        horizontal,
        vertical,
    };

    [SerializeField]
    private Axis movementAxis;
    private Vector3 direction;
    private float moveSpeed = 100;

    void Start()
    {
        subscribe(); //TODO find a way to call Start of EnemyInterface
        //TODO change sprite based on direction
        if (movementAxis == Axis.horizontal)
            direction = Vector3.right;
        else
            direction = Vector3.up;
    }

    public override void Move()
    {
        RaycastHit2D wallIsInFront = Physics2D.Raycast(
            transform.position,
            direction,
            1f,
            LayerMask.GetMask("Wall")
        );
        if (wallIsInFront)
            //TODO flip sprite
            direction *= -1;
        //TODO separate logic for deciding where to move from logic for moving, for better synchronization
        // transform.position += direction;
        Vector3 targetPosition = transform.position + direction;
        //FIXME synchronize this method with EventHandler and player
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Killed Player. Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
