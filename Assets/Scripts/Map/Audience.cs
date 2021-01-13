using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audience : Map
{
    private string audienceTag = "Audience";
    private string publicTag = "Public";

    // Start is called before the first frame update
    void Start()
    {
        referenceTile.tag = audienceTag;                    // See Script: Map
        referenceCharacter.tag = publicTag;                 // See Script: Map
        GenerateMap();                                      // See Script: Map
        GeneratePublic();
    }

    // Generates the Public Members in the Audience Map
    private void GeneratePublic()
    {
        GameObject referencePublicMember = referenceCharacter;
        GameObject[] tiles = GameObject.FindGameObjectsWithTag(audienceTag);
        maxNumberOfCharactersOnMap = tiles.Length;
        List<GameObject> availableTiles = new List<GameObject>();


        if (numberOfCharactersOnMap < maxNumberOfCharactersOnMap)
        {
            for (int i = 0; i < numberOfCharactersOnMap; i++)
            {
                // Instantiates a Public Member
                GameObject publicMember = (GameObject)Instantiate(referencePublicMember, transform);
                publicMember.GetComponent<SpriteRenderer>().color = new Color(0, i * 0.1f, i * 0.01f, 1);

                // Checks all tiles without a Public Member on it
                foreach (GameObject tile in tiles)
                {

                    if (tile.transform.childCount == 0)
                    {
                        availableTiles.Add(tile);
                    }

                }

                // Sets a Public Member to an avaiable tile
                int index = Random.Range(0, availableTiles.Count);
                publicMember.transform.SetParent(availableTiles[index].transform);
                publicMember.transform.localPosition = new Vector3(0, 0, 0);
                publicMember.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                availableTiles.Clear();
            }

        }

    }

}
