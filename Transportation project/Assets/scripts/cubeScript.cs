using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnMouseDown()
    {
        gameController.clickUpdate(this.gameObject);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
