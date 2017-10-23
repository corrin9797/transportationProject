using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
    //Used for making cubes
    Vector3 cubePosition;
    public GameObject cubePrefab;
    GameObject currentCube;

    //Used for tracking which cube user has clicked if any, and whether the airplane is active
    public static GameObject redCube;
    public static bool redCubeExists = false;
    public static bool airplaneIsActive = true;

    // Use this for initialization
    GameObject MakeCube(int xPos,int yPos)
    {
        cubePosition = new Vector3(xPos, yPos, 0);

        //                                    Quaternion, as of right now, is a magic word.
        currentCube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        currentCube.GetComponent<Renderer>().material.color = Color.white;
        currentCube.AddComponent<cubeScript>();
        return currentCube;
    }
    
    void Start () {
        for (int i=-8; i<8; i++)
        {
            for (int j = 4; j > -5; j--){
                currentCube = MakeCube(i, j);
                if (!redCubeExists)
                {
                    currentCube.GetComponent<Renderer>().material.color = Color.red;
                    redCubeExists = true;
                    redCube = currentCube;
                }
            }
            
            
        }
        
    }
    public static void clickUpdate(GameObject clickedCube)
    {
        if (airplaneIsActive)
        {
            if (redCubeExists) //We don't need this anymore because the game starts with a red cube
                                              //But I'm keeping it in case it doesn't later
                                              //Come to think of it, does C sharp have try/catch?
                                              //Or, as the TA's suggested, check if redCube is null.
                                              //Really, there were a lot of better ways to do this.
            {
                if (redCube == clickedCube) //Checking if you're clicking on what is already the airplane
                {
                    clickedCube.GetComponent<Renderer>().material.color = Color.green;
                    airplaneIsActive = false;
                    //There's no way to reactivate the airplane, but a way wasn't specified in the outline.
                }
                else
                {
                    redCube.GetComponent<Renderer>().material.color = Color.white;
                }
            }
            if (redCube != clickedCube)
            {
                redCube = clickedCube;
                redCubeExists = true;
               redCube.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
