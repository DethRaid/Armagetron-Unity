using UnityEngine;
using System.Collections;

[AddComponentMenu( "Lightcycle/Trail" )]
public class LightcycleTrailSpawner : MonoBehaviour {
    public GameObject trailMesh;
    public float scaleFactor;

    private LightcycleMovement movement;
    private GameObject curTrailMesh;

	void Awake () {
        movement = GetComponent<LightcycleMovement>();
        movement.onTurnLeft += onLightcycleTurnLeft;
        movement.onTurnRight += onLightcycleTurnRight;
        newSegment();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 oldPosition = curTrailMesh.transform.localPosition;
        oldPosition += transform.forward * movement.currentSpeed * Time.deltaTime * 0.5f;
        oldPosition.x = transform.position.x;
        curTrailMesh.transform.localPosition = oldPosition;

        //curTrailMesh.transform.Translate( transform.forward * movement.currentSpeed * Time.deltaTime * 0.5f, Space.World );
        //curTrailMesh.transform.rotation = transform.rotation;

        Vector3 oldScale = curTrailMesh.transform.localScale;
        curTrailMesh.transform.localScale = oldScale + (Vector3.forward * scaleFactor * movement.currentSpeed * Time.deltaTime);
	}

    void newSegment() {
        curTrailMesh = Instantiate<GameObject>( trailMesh );
        curTrailMesh.transform.position = transform.position;
    }

    void onLightcycleTurnRight() {
        newSegment();
    }

    void onLightcycleTurnLeft() { 
        newSegment();
    }
}
