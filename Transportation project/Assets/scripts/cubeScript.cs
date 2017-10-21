using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnMouseDown()
    {
        if (gameController.redCubeExists)
        {
            gameController.redCube.GetComponent<Renderer>().material.color = Color.white;
        }
        gameController.redCube = this.gameObject;
        gameController.redCubeExists = true;
        gameController.redCube.GetComponent<Renderer>().material.color = Color.red;


    }
    // Update is called once per frame
    void Update () {
		
	}
}
