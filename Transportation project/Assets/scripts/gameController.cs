using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
    //Used for making cubes
    Vector3 cubePosition;
    public GameObject cubePrefab;
    GameObject currentCube;
    int airplaneX;
    int airplaneY;

    //Used for tracking which cube user has clicked if any, and whether the airplane is active
    public static GameObject redCube;
    public static bool redCubeExists = false;
    public static bool airplaneIsActive = true;

    public static GameObject[,] cubeArray = new GameObject[16,9];
    //Array of arrays that tracks cubes
    //[x,y] x==0 at left and y==0 at bottom


    

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
            for (int j = -4; j < 5; j++){
                currentCube = MakeCube(i, j);
                
                cubeArray[i + 8,j+4] = currentCube;
            }
            
            
        }
        cubeArray[0,8].GetComponent<Renderer>().material.color = Color.red;
        redCubeExists = true;
        redCube = cubeArray[0, 8];
        airplaneX = 0;
        airplaneY = 8;

    }
    public static void clickUpdate(GameObject clickedCube)
    {
        if (airplaneIsActive)
        {
            if (redCubeExists)                //We don't need this anymore because the game starts with a red cube
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
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("down") && airplaneY > 0)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX, airplaneY-1];
            airplaneY--;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown("up") && airplaneY < 8)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX, airplaneY + 1];
            airplaneY++;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown("left") && airplaneX > 0)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX-1, airplaneY];
            airplaneX--;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown("right") && airplaneX < 15)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX+1, airplaneY];
            airplaneX++;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    
}
