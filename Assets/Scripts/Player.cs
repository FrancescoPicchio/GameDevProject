using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tilemap tilemap;

    public float moveSpeed = 15f;
    private Vector3 targetPosition;

    void Start()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);  //Ti dice in quale cella si trova il player
        Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);      //Restituisce il centro della cella
        targetPosition = cellCenter;                                        //Muove il player nel centro della cella
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
            if (CheckLegality(direction))
            {
                targetPosition += direction;
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

    bool CheckLegality(Vector3 direction)
    {
        if (tilemap.HasTile(tilemap.WorldToCell(transform.position + direction)))
        {
            return true;
        }
        return false;
    }
}
