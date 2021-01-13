using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject cursor;
    public GameObject turnManager;
    private List<GameObject> listOfSelections;
    private float waitTimeDoubleClickMinThreshold = 0.25f;
    private float waitTimeDoubleClickMaxThreshold = 1f;
    private float timerDoubleClick = 0f;

    // Start is called before the first frame update
    void Start()
    {
        listOfSelections = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f);         // Get cursor position from mouse position 
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePos);                              // Convert by using the camera

        cursor.transform.position = newPosition;                                                     // Set new position

        // After a mouse double click, checks if the selected an whether or not there is a character in it
        if (Input.GetMouseButtonDown(0))
        {
            timerDoubleClick = Time.time;

            if (Time.time - timerDoubleClick > waitTimeDoubleClickMinThreshold && Time.time - timerDoubleClick < waitTimeDoubleClickMaxThreshold)
            {

                // Creates a Ray
                Ray ray = Camera.main.ViewportPointToRay(Camera.main.ScreenToViewportPoint(Input.mousePosition));

                // The RayCastHit is the target of the ray when collision occurs
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Selects the Game Object
                    GameObject selection = hit.transform.gameObject;
                    SelectionManager(selection);
                }

                // If user doesn't double click on a character, cancels the last selection
                else
                {
                    if (listOfSelections.Count != 0)
                    {
                        Debug.Log("Nothing is selected, last selection has been undone");
                        listOfSelections.RemoveAt(listOfSelections.Count - 1);
                    }
                }
            }
        }
    }


    // Depending on the tag of the selection, the Selection Manager will do something different
    void SelectionManager(GameObject selection)
    {
        SelectCharacter(selection);

        if (listOfSelections.Count != 1)
        {
            // Cancels the selection
            if (listOfSelections[0] == listOfSelections[1])
            {
                Cancel();
            }

            // Swaps postion 
            else if (listOfSelections[1].tag == "Performer")
            {
                if (turnManager.GetComponent<TurnCount>().turn == 0)
                {
                    Swap(listOfSelections);
                }

                else
                {
                    Support();
                }
            }

            else if (listOfSelections[1].tag == "Public")
            {
                Target();
            }

            listOfSelections.Clear();
        }

    }


    void SelectCharacter(GameObject selectedCharacter)
    {

        Debug.Log("Selecting Character" + selectedCharacter.transform.name);
        if (selectedCharacter.tag == "Performer")
        {
            listOfSelections.Add(selectedCharacter);
        }

        if (selectedCharacter.tag == "Public")
        {

        }
    }

    void Cancel()
    {
        Debug.Log("oops, Cancel Selection");
    }

    void Swap(List<GameObject> listOfSelections)
    {
        Debug.Log("Swapping Characters");

        // Creates temporary parents to swap
        Transform tempParent0 = listOfSelections[0].transform.parent;

        // Swaps parents
        listOfSelections[0].transform.parent = listOfSelections[1].transform.parent;
        listOfSelections[1].transform.parent = tempParent0;

        // Swap positions
        listOfSelections[0].transform.localPosition = new Vector3(0, 0, 0);
        listOfSelections[1].transform.localPosition = new Vector3(0, 0, 0);
    }

    void Support()
    {
        Debug.Log("Supporting");
    }

    void Target()
    {
        Debug.Log("Bye, Target");
    }
}
