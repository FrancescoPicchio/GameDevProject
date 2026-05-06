using UnityEngine;

public class SimpleEnemy : EnemyInterface
{
    public enum InitialDirection
    {
        north,
        south,
        west,
        east,
    };

    [SerializeField]
    private InitialDirection initialDirection = InitialDirection.east;
    private float moveSpeed = 80;
    private bool isMoving = false;

    void Start()
    {
        subscribe(); //TODO find a way to call Start of EnemyInterface
        //TODO change sprite based on direction
        if (initialDirection == InitialDirection.north)
            direction = Vector3.up;
        if (initialDirection == InitialDirection.south)
            direction = Vector3.down;
        if (initialDirection == InitialDirection.west)
            direction = Vector3.left;
        if (initialDirection == InitialDirection.east)
            direction = Vector3.right;
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
            //We could use Rigidbody2D.MoveTowards instead of waiting for FixedUpdate
            if (transform.position == targetPosition)
            {
                isMoving = false;
                finishedTurn.Invoke();
            }
        }
    }

    public override void Move()
    {
        Collider2D wallIsInFront = Physics2D.OverlapCircle(
            transform.position + direction,
            0.4f,
            LayerMask.GetMask("Wall")
        );
        Collider2D enemyIsInFront = Physics2D.OverlapCircle(
            transform.position + direction,
            0.4f,
            LayerMask.GetMask("Enemy")
        );
        // bool enemyIsInFront = eventHandler.isEnemyInThisPosition(transform.position + direction);
        if (wallIsInFront || enemyIsInFront)
        {
            //TODO flip sprite
            targetPosition = transform.position;
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
