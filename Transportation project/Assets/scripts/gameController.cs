using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle
{
    //Original location is recorded to determine cargo pickup.
    public int startingX;
    public int startingY;
    //Where the vehicle is.
    public int vehicleX;
    public int vehicleY;
    //Where the vehicle is going.
    public int destinationX;
    public int destinationY;
    //How much cargo the vehicle has/can have.
    public int cargo;
    public int maxCargo;
    //What the vehicle's color is and what the default color is. Currently used for showing active vehicle.
    public Color currentColor;
    public Color defaultColor;
    //Speed, effectively. 0 means moves every turn, 1 means moves once every other turn.
    public int turnsBetweenMovement;
    public int turnsUntilMovement;
    public vehicle(int x, int y, Color color, int cargoCapacity, int speed)
    {
        destinationX = x;
        vehicleX = x;
        startingX = x;
        vehicleY = y;
        destinationY = y;
        startingY = y;
        currentColor = color;
        defaultColor = color;
        cargo = 0;
        maxCargo = cargoCapacity;
        turnsBetweenMovement = speed;
        turnsUntilMovement = 0;
    }

}






public class gameController : MonoBehaviour {
    //Used for making cubes
    Vector3 cubePosition;
    public GameObject cubePrefab;
    GameObject currentCube;
    //Used for keeping track of active object
    vehicle activeVehicle = null;
    
    vehicle[] vehicles = new vehicle[10]; //Null slots are checked for and ignored.

    //Array of arrays that tracks cubes
    //[x,y] x==0 at left and y==0 at bottom
    public static GameObject[,] cubeArray = new GameObject[16, 9];

    //These variables track turn timers.
    public float turnTime = 1.5f;
    float timeToNextTurn = 1.5f;
    //Yes, I should have used an enum. But I couldn't figure out how to get them to work, and the internet didn't tell me how.
    

    //Score and cargo variables
    string onScreenScore="";
    int playerScore = 0;
    int cargoBayX;
    int cargoBayY;



    
    GameObject MakeCube(int xPos,int yPos)
    {
        cubePosition = new Vector3(xPos, yPos, 0);

        //                                    Quaternion, as of right now, is a magic word.
        currentCube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        currentCube.GetComponent<Renderer>().material.color = Color.white;
        currentCube.AddComponent<cubeScript>();
        
        return currentCube;
    }


    // Use this for initialization
    void Start () {
       // Creates 2d array with cubes in it and has each cube record their coordinates.
        for (int i=-8; i<8; i++)
        {
            for (int j = -4; j < 5; j++){
                currentCube = MakeCube(i, j);
                
                cubeArray[i + 8,j+4] = currentCube;
                
                currentCube.GetComponent<cubeScript>().xCoordinate = i + 8;
                currentCube.GetComponent<cubeScript>().yCoordinate = j + 4;
            }


        }
        vehicle airplane = new vehicle(0,8,Color.red,90,0);
        vehicles[0] = airplane;
        vehicle train = new vehicle(0, 0, Color.green, 200, 1);
        vehicles[1] = train;
        vehicle boat = new vehicle(15, 8, Color.blue, 550, 2);
        vehicles[2] = boat;

        cargoBayX = 15;
        cargoBayY = 0;
    }






    public void clickUpdate(cubeScript clickedCube)
    {
        bool squareIsVehicle = false;
        vehicle selectedVehicle = null;
        //A while loop would be more efficient, but also more convoluted and harder to read
        foreach (vehicle currentVehicle in vehicles)
        {

            if (currentVehicle != null && !squareIsVehicle)
            {
                if (currentVehicle.vehicleX == clickedCube.xCoordinate && currentVehicle.vehicleY == clickedCube.yCoordinate)
                {
                    squareIsVehicle = true;
                    selectedVehicle = currentVehicle;
                }
            }
        }
        //If there is an active vehicle
        if (activeVehicle != null)
        {
            //If the square is a vehicle but not the active vehivle, make it active
            if (squareIsVehicle && activeVehicle!=selectedVehicle)
            {
                activeVehicle.currentColor = activeVehicle.defaultColor;
                activeVehicle = selectedVehicle;
                activeVehicle.currentColor = Color.yellow;
            }
            //If that activated vehicle is the same as the clicked vehicle, deactivate it
            else if (selectedVehicle == activeVehicle)
            {
                activeVehicle.currentColor = activeVehicle.defaultColor;
                activeVehicle = null;
            }
            //If the square clicked isn't a vehicle and there isn't an active vehicle, the active vehicle's destination is set to here
            else
            {
                activeVehicle.destinationX = clickedCube.xCoordinate;
                activeVehicle.destinationY = clickedCube.yCoordinate;
            }
        }
        //If there isn't an active vehicle, and a vehicle was clicked
        else if (squareIsVehicle)
        {
            activeVehicle = selectedVehicle;
            activeVehicle.currentColor = Color.yellow;
        }
        //Otherwise, that means you clicked on an empty space without an active vehicle, do nothing.
    }





	//Directional movement. Technically it doesn't have to check if the movement is out of bounds but it's nice to have
    //the functionality just in case
    void moveUp(vehicle currentVehicle)
    {
        if (currentVehicle.vehicleY < 8)
        {
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.white;
            
            currentVehicle.vehicleY++;
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    void moveLeft(vehicle currentVehicle)
    {
        if (currentVehicle.vehicleX > 0)
        {
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.white;

            currentVehicle.vehicleX--;
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    void moveRight(vehicle currentVehicle)
    {
        if (currentVehicle.vehicleX < 15)
        {
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.white;

            currentVehicle.vehicleX++;
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.red;
        }
    }
    void moveDown(vehicle currentVehicle)
    {
        if (currentVehicle.vehicleY > 0)
        {
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.white;

            currentVehicle.vehicleY--;
            cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.red;
        }
    }






	// Update is called once per frame
	void Update () {
        //Rules did not specify if cargo was earned at start of turn, end of turn, or only if you haven't moved.
        //I made it so as the airplane only collects cargo if it hasn't moved.
        if (timeToNextTurn < Time.time)
        {
            timeToNextTurn += turnTime;
            print("Turn taken!");
            //Movement
            foreach (vehicle currentVehicle in vehicles)
            {
                if (currentVehicle != null)
                {
                    //This is the cargo check. Cargo is added if the vehicle is in its satarting location
                    if(currentVehicle.vehicleX==currentVehicle.startingX && currentVehicle.vehicleY == currentVehicle.startingY)
                    {
                        currentVehicle.cargo += 10;
                        if (currentVehicle.cargo > currentVehicle.maxCargo)
                        {
                            currentVehicle.cargo = currentVehicle.maxCargo;
                        }
                    }
                    //The below statements handle movement.
                    if (currentVehicle.turnsUntilMovement > 0)
                    {
                        currentVehicle.turnsUntilMovement--;
                    }
                    else
                    {
                        currentVehicle.turnsUntilMovement = currentVehicle.turnsBetweenMovement;
                        if (currentVehicle.destinationY > currentVehicle.vehicleY)
                        {
                            moveUp(currentVehicle);
                        }
                        if (currentVehicle.destinationY < currentVehicle.vehicleY)
                        {
                            moveDown(currentVehicle);
                        }
                        if (currentVehicle.destinationX > currentVehicle.vehicleX)
                        {
                            moveRight(currentVehicle);
                        }
                        if (currentVehicle.destinationX < currentVehicle.vehicleX)
                        {
                            moveLeft(currentVehicle);
                        }
                    }
                    //The below statement handles cargo dropoff and scoring.
                    if (currentVehicle.vehicleX==cargoBayX && currentVehicle.vehicleY == cargoBayY)
                    {
                        playerScore += currentVehicle.cargo;
                        currentVehicle.cargo = 0;
                    }
                }
            }
           
            

        }
        foreach (vehicle currentVehicle in vehicles)
        {
            if (currentVehicle != null)
            {
                cubeArray[currentVehicle.vehicleX, currentVehicle.vehicleY].GetComponent<Renderer>().material.color = currentVehicle.currentColor;
            }
        }
        //Sets the cargo bay to black.

        cubeArray[cargoBayX, cargoBayY].GetComponent<Renderer>().material.color = Color.black;

        //Active vehicle color takes precedence over all other colors, so it's done last in the update method
        if (activeVehicle != null)
        {
            cubeArray[activeVehicle.vehicleX, activeVehicle.vehicleY].GetComponent<Renderer>().material.color = Color.yellow;
        }
    }





    //NOTE TO SELF: it's OnGUI with a CAPITAL O. That's why it's not showing up and not throwing an error.
    void OnGUI()
    {
        onScreenScore ="Score: " + playerScore;

        GUI.Box(new Rect(4,4,100,20), onScreenScore);
    }
}
