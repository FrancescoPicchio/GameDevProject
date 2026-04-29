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
    private Vector3 direction;
    private float moveSpeed = 30;
    private bool isMoving = false;
    private EventHandler eventHandler;
    public UnityEvent finishedTurn;

    void Start()
    {
        eventHandler = GameObject.FindGameObjectWithTag("Logic").GetComponent<EventHandler>();
        if (eventHandler){
            eventHandler.subscribeEnemy(this);
            finishedTurn.AddListener(eventHandler.finishEnemyTurn);
        }
        else
            Debug.Log("Couldn't find EventHandler");

        targetPosition = transform.position;
        visitor = new SimpleEnemyVisitor();

        if (movementAxis == Axis.horizontal)
            direction = Vector3.right;
        else
            direction = Vector3.up;
    }

    void Update() { }

    public override void Accept(Visitor v)
    {
        v.SimpleEnemyVisit(this);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("Enemy found collision");
    // }
}
