using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : EnemyInterface
{
    public enum Axis
    {
        horizontal,
        vertical,
    };

    [SerializeField]
    private Axis movementAxis;
    private Vector3 direction;

    void Start()
    {
        subscribe();
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
        transform.position += direction;
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
