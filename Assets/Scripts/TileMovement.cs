using UnityEngine;                 // Importa le classi base di Unity (Transform, Input, ecc.)
using UnityEngine.Tilemaps;        // Importa le classi per lavorare con Tilemap

public class TileMovement : MonoBehaviour
{
    public Tilemap tilemap;         // Riferimento alla Tilemap su cui muoversi
    public float moveSpeed = 5f;    // Velocità di movimento tra le celle

    private Vector3Int currentCell; // La cella corrente in coordinate di griglia
    private Vector3 targetPosition; // La posizione nel mondo verso cui muoversi
    private bool isMoving = false;  // Flag che indica se il player sta già muovendo

    void Start()
    {
        // Converte la posizione iniziale del player in cella della Tilemap
        currentCell = tilemap.WorldToCell(transform.position);

        // Ottiene la posizione centrale della cella e posiziona il player lì
        targetPosition = tilemap.GetCellCenterWorld(currentCell);
        transform.position = targetPosition;
    }

    void Update()
    {
        // Gestisce il movimento ogni frame
        HandleMovement();
    }

    void HandleMovement()
    {
        // Se il player si sta muovendo verso targetPosition
        if (isMoving)
        {
            // Muove il player verso targetPosition in modo fluido
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Controlla se il player ha raggiunto il target
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // Assicura precisione
                isMoving = false;                     // Ferma il movimento
            }

            return; // Non gestire input finché sta muovendo
        }

        // Lettura dell'input, inizializza direzione a zero
        Vector3Int direction = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3Int.up;           // Su
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3Int.down;         // Giù
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector3Int.left;         // Sinistra
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector3Int.right;        // Destra

        // Se c'è input
        if (direction != Vector3Int.zero)
        {
            // Calcola la cella verso cui vogliamo muoverci
            Vector3Int nextCell = currentCell + direction;

            // Controlla se esiste una tile in quella cella
            if (tilemap.HasTile(nextCell))
            {
                currentCell = nextCell;                       // Aggiorna la cella corrente
                targetPosition = tilemap.GetCellCenterWorld(currentCell); // Aggiorna la posizione target
                isMoving = true;                              // Inizia il movimento verso la nuova cella
            }
        }
    }
}