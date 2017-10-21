using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
    //Used for making cubes
    Vector3 cubePosition;
    public GameObject cubePrefab;
    GameObject currentCube;

    //Used for tracking which cube user has clicked if any
    public static GameObject redCube;
    public static bool redCubeExists = false;

    // Use this for initialization
    void MakeCube(int xPos,int yPos)
    {
        cubePosition = new Vector3(xPos, yPos, 0);

        //                                    Quaternion, as of right now, is a magic word.
        currentCube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        currentCube.GetComponent<Renderer>().material.color = Color.white;
        currentCube.AddComponent<cubeScript>();
    }
    
    void Start () {
        for (int i=-8; i<8; i++)
        {
            MakeCube(i, 0);
            //This is somewhat inefficient, but also much easier to work with.
            //I could just have the last cube be called the red cube to avoid null references,
            //but I know we're going to rewrite this later and this way makes rewriting easier.
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
