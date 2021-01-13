using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stage : Map
{
    private string stageTag = "Stage";
    private string performerTag = "Performer";


    // Start is called before the first frame update
    void Start()
    {
        maxNumberOfCharactersOnMap = 4;             // See Script: Map
        referenceTile.tag = stageTag;               // See Script: Map
        referenceCharacter.tag = performerTag;      // See Script: Map
        GenerateMap();                              // See Script: Map
        GeneratePerformers();
    }

    // Generate up to 4 performer on each different corner of the map
    private void GeneratePerformers()
    {
        GameObject referencePerformer = referenceCharacter;
        GameObject[] tiles = GameObject.FindGameObjectsWithTag(stageTag);
        List<GameObject> corners = new List<GameObject>();
        List<GameObject> availableCorners = new List<GameObject>();

        // Find all 4 corners of the stage
        foreach (GameObject tile in tiles)
        {
            if (tile.transform.GetComponent<Tile>().row == 0 || tile.transform.GetComponent<Tile>().row == rows - 1)
            {
                if (tile.transform.GetComponent<Tile>().col == 0 || tile.transform.GetComponent<Tile>().col == cols - 1)
                {
                    corners.Add(tile);
                }
            }
        }


        // Adds charaters to the corners of the stage
        if (numberOfCharactersOnMap < maxNumberOfCharactersOnMap + 1)
        {
            for (int i = 0; i < numberOfCharactersOnMap; i++)
            {
                // Instantiates the performer
                GameObject performer = (GameObject)Instantiate(referencePerformer, transform);
                performer.GetComponent<SpriteRenderer>().color = new Color(0, i * 0.2f, i * 0.35f, 1);

                // Checks corners without any performer on it
                foreach (GameObject corner in corners)
                {
                    if (corner.transform.childCount == 0)
                    {
                        availableCorners.Add(corner);
                    }
                }

                // Adds the performer to one of the availabe corners
                performer.transform.SetParent(availableCorners[0].transform);
                performer.transform.localPosition = new Vector3(0, 0, 0);
                performer.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                performer.transform.name = i.ToString();
                availableCorners.Clear();
            }
        }
    }
}


