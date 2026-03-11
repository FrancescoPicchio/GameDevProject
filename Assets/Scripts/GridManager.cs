using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Transform cam;

    void Start(){
        GenerateGrid();
    }
    void GenerateGrid(){
        for( int x=0; x<width; x++){
            for(int y=0; y<height; y++){
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";

                //Makes a checkerboard pattern by coloring tiles
                var isOffset = x%2 != y%2;
                spawnedTile.Init(isOffset);
            }
        }

        //Centers the camera on the middle of the center tile.
        // -10 is the standard position for the camera
        cam.position = new Vector3((float)width/2 - 0.5f, (float)height/2 -0.5f, -10);
    }

}
