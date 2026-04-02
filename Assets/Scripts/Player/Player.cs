using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour, IMovable
{

    public float moveSpeed = 15f;
    public Tilemap tilemap;
    private Vector3 targetPosition;

    void Start()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);  //Ti dice in quale cella si trova il player
        Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);      //Restituisce il centro della cella
        transform.position = cellCenter;
        targetPosition = transform.position;
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 direction = GetDirection();
            TryMove(direction);
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

    public bool TryMove(Vector3 direction)
    {
        Collider2D col = Physics2D.OverlapPoint(transform.position + direction);

        if (col != null && col.TryGetComponent<IMovable>(out IMovable other))
        {
            if (other.TryMove(direction))
            {
                targetPosition += direction;
                return true;
            }
            return false;
        }
        targetPosition += direction;
        return true;
    }
}
