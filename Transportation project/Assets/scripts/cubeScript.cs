using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnMouseDown()
    {
        if (gameController.airplaneIsActive)
        {
            if (gameController.redCubeExists) //We don't need this anymore because the game starts with a red cube
                                              //But I'm keeping it in case it doesn't later
                                              //Come to think of it, does C sharp have try/catch?
                                              //Or, as the TA's suggested, check if redCube is null.
                                              //Really, there were a lot of better ways to do this.
            {
                if (gameController.redCube == this.gameObject) //Checking if you're clicking on what is already the airplane
                {
                    this.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    gameController.airplaneIsActive = false;
                    //There's no way to reactivate the airplane, but a way wasn't specified in the outline.
                }
                else
                {
                    gameController.redCube.GetComponent<Renderer>().material.color = Color.white;
                }
            }
            if (gameController.redCube != this.gameObject)
            {
                gameController.redCube = this.gameObject;
                gameController.redCubeExists = true;
                gameController.redCube.GetComponent<Renderer>().material.color = Color.red;
            }
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
