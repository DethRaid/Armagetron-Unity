using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {
    private LightcycleMovement cycle;

	// Use this for initialization
	void Start() {
        cycle = GetComponent<LightcycleMovement>();
	}
	
	// Update is called once per frame
	void Update() {
        if( Input.GetButtonDown( "right" ) ) {
            cycle.turnRight();
        } else if( Input.GetButtonDown( "left" ) ) {
            cycle.turnLeft();
        }
	}
}
