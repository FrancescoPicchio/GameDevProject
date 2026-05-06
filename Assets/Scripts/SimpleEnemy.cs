using UnityEngine;

public class SimpleEnemy : EnemyInterface
{
    public enum Axis
    {
        horizontal,
        vertical,
    };

    [SerializeField]
    private Axis movementAxis;
    private float moveSpeed = 50;
    private bool isMoving = false;
    void Start()
    {
        subscribe(); //TODO find a way to call Start of EnemyInterface
        //TODO change sprite based on direction
        if (movementAxis == Axis.horizontal)
            direction = Vector3.right;
        else
            direction = Vector3.up;
    }

    void Update()
    {
        if (isMoving && Vector3.Distance(transform.position, targetPosition) > 0f)
        {
            //TODO use LERP instead of speed to make movement smoother
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            if (transform.position == targetPosition)
            {
                finishedTurn.Invoke();
                isMoving = false;
            }
        }
    }

    public override void Move()
    {
        Collider2D wallIsInFront = Physics2D.OverlapPoint(
            transform.position + direction,
            LayerMask.GetMask("Wall")
        );
        Collider2D enemyIsInFront = Physics2D.OverlapPoint(
            transform.position + direction,
            LayerMask.GetMask("Enemy")
        );
        if (wallIsInFront || enemyIsInFront)
        {
            //TODO flip sprite
            direction *= -1;
            finishedTurn.Invoke();
            return;
        }
        //TODO separate logic for deciding where to move from logic for moving, for better synchronization
        targetPosition = transform.position + direction;
        isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Killed Player. Game Over!");
            EventHandler.killPlayer();
        }
    }

    public override void unsubscribe()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
        {
            //Passes the old position of the enemy
            eventHandler.unsubscribeEnemy(CoordinatesUtil.convert(getOldPosition()));
        }
        else
            Debug.Log("Couldn't find EventHandler");
    }
}
