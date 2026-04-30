using UnityEngine;
using UnityEngine.Events;

public class SimpleEnemy : ObjectWithCollision
{
    public enum Axis
    {
        horizontal,
        vertical,
    };

    [SerializeField]
    private Axis movementAxis;
    private Vector3 movementDirection;
    private float moveSpeed = 30;
    private bool isMoving = false;
    private EventHandler eventHandler;
    public UnityEvent finishedTurn;

    void Start()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler)
        {
            //TODO make abstract class so that only enemies can subscribe
            eventHandler.subscribeEnemy(this);
            finishedTurn.AddListener(eventHandler.finishEnemyTurn);
        }
        else
            Debug.Log("Couldn't find EventHandler");

        visitor = new SimpleEnemyVisitor();

        if (movementAxis == Axis.horizontal)
            movementDirection = Vector3.right;
        else
            movementDirection = Vector3.up;
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
            if (targetPosition == transform.position)
            {
                isMoving = false;
                finishedTurn.Invoke();
            }
        }
    }

    public void Move()
    {
        targetPosition = transform.position + movementDirection;
        LookForObjectWithCollision();
        isMoving = true;
    }

    public override void Accept(Visitor v)
    {
        v.SimpleEnemyVisit(this);
    }

    public void FlipMovementDirection()
    {
        movementDirection *= -1;
        targetPosition += movementDirection * 2;
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("Enemy found collision");
    // }
}
