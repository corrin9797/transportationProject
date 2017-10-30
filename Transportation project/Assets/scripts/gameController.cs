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
    

    //Array of arrays that tracks cubes
    //[x,y] x==0 at left and y==0 at bottom
    public static GameObject[,] cubeArray = new GameObject[16, 9];

    //These variables track turn timers.
    public float turnTime = 1.5f;
    float timeToNextTurn = 1.5f;
    //Yes, I should have used an enum. But I couldn't figure out how to get them to work, and the internet didn't tell me how.
    string selectedMovement = "";

    //Score and cargo variable
    string onScreenScore="";
    public int airplaneCargo = 0;
    int playerScore = 0;



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
	//Directional movement. Assumes you aren't letting it run off the grid.
    void moveUp()
    {
        if (airplaneY < 8)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX, airplaneY + 1];
            airplaneY++;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    void moveLeft()
    {
        if (airplaneX > 0)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX - 1, airplaneY];
            airplaneX--;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    void moveRight()
    {
        if (airplaneX < 15)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX + 1, airplaneY];
            airplaneX++;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    void moveDown()
    {
        if (airplaneY > 0)
        {
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            redCube = cubeArray[airplaneX, airplaneY - 1];
            airplaneY--;
            cubeArray[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.red;
        }
    }

	// Update is called once per frame
	void Update () {
        //Rules did not specify if cargo was earned at start of turn, end of turn, or only if you haven't moved.
        //I made it so as the airplane only collects cargo if it hasn't moved.
        if (airplaneX==0 && airplaneY == 8 && timeToNextTurn < Time.time && selectedMovement=="")
        {
            airplaneCargo += 10;
            if (airplaneCargo > 90)
            {
                airplaneCargo = 90;
            }
        }
        if (airplaneX == 15 && airplaneY == 0 && timeToNextTurn < Time.time && selectedMovement == "")
        {
            playerScore += airplaneCargo;
            airplaneCargo = 0;
        }
            if (timeToNextTurn < Time.time)
        {
            print("Moo");
            timeToNextTurn += turnTime;
            if (selectedMovement=="up")
            {
                moveUp();
            }
            if (selectedMovement=="down")
            {
                moveDown();
            }
            if (selectedMovement=="right")
            {
                moveRight();
            }
            if (selectedMovement=="left")
            {
                moveLeft();
            }
            selectedMovement = "";
        }
        if (Input.GetKeyDown("down"))
        {
            selectedMovement="down";
        }
        if (Input.GetKeyDown("up"))
        {
            selectedMovement="up";
        }
        if (Input.GetKeyDown("left"))
        {
            selectedMovement="left";
        }
        if (Input.GetKeyDown("right"))
        {
            selectedMovement="right";
        }
        cubeArray[15, 0].GetComponent<Renderer>().material.color = Color.black;
        
    }
    //NOTE TO SELF: it's OnGUI with a CAPITAL O. That's why it's not showing up and not throwing an error.
    void OnGUI()
    {
        onScreenScore ="Score: " + playerScore;

        GUI.Box(new Rect(4,4,100,20), onScreenScore);
    }
}
