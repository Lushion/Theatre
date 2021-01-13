using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCount : MonoBehaviour
{
    public int numberOfActs = 2;
    public int numberOfScenes = 2;

    private int act = 1;
    private int scene = 1;
    public int turn = 0;

    public Text turnCountText;
    public Text sceneCountText;
    public Text actCountText;
    public Text turnButtonText;



    // Start is called before the first frame update
    void Start()
    {
        Prepare();
    }

    void Update()
    {
        turnCountText.text = "Turn: " + turn + "/10";
        sceneCountText.text = "Scene: " + scene + "/" + numberOfScenes;
        actCountText.text = "Act: " + act + "/" + numberOfActs;
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (turnButtonText.text == "Play finished - Restart?")
        {
            Reset();
        }

        else if (turn == 0)
        {
            Initialize();
        }

        else if (turn < 10)
        {
            IncrementTurn();
        }

        else if (turn == 10)
        {
            if (scene < numberOfScenes)
            {
                IncrementScene();
            }

            else if (scene == numberOfScenes)
            {
                if (act < numberOfActs)
                {
                    IncrementAct();
                }

                else if (act == numberOfActs)
                {
                    turnButtonText.text = "Play finished - Restart?";
                }
            }
        }
    }

    void Prepare()
    {
        turnButtonText.text = "Start scene?";
        turn = 0;
    }

    void Initialize()
    {
        turnButtonText.text = "End Turn?";
        turn = 1;
    }

    void IncrementTurn()
    {
        turn++;
    }

    void IncrementScene()
    {
        scene++;
        Prepare();
    }

    void IncrementAct()
    {
        act++;
        scene = 1;
        Prepare();
    }

    void Reset()
    {
        act = 1;
        scene = 1;
        Prepare();
    }
}
