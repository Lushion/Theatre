using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int rows = 2;
    public int cols = 2;
    
    public float orientation;

    public int numberOfCharactersOnMap;
    protected int maxNumberOfCharactersOnMap;

    public GameObject referenceTile;
    public GameObject referenceCharacter;

    // Generates a Map based on the indicated map Size
    public void GenerateMap()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Instantiatse a Tile
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                // Places the Tile on the map
                Vector2 size = tile.GetComponent<SpriteRenderer>().bounds.size;
                float posX = (col - cols / 2) * size.x;
                float posY = Mathf.Sign(orientation) * (row + 1) * size.y;
                tile.transform.position = new Vector3(posX, posY, 0);

                // Affects parameters to the Tile object
                tile.transform.GetComponent<Tile>().row = row;
                tile.transform.GetComponent<Tile>().col = col;
                tile.transform.GetComponent<Tile>().orientation = orientation;
                tile.transform.name = "O" + orientation.ToString() + " R" + row.ToString() + " C" + col.ToString();
            }
        }
    }
}

