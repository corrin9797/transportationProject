using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {
    public int xCoordinate;
    public int yCoordinate;
    gameController theGameController;
	// Use this for initialization
	void Start () {
        
        theGameController = GameObject.Find("gameControllerObject").GetComponent<gameController>();
	}
    void OnMouseDown()
    {
        theGameController.clickUpdate(this);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
