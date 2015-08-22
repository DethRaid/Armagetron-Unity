using UnityEngine;
using System.Collections;

[AddComponentMenu( "Lightcycle/Movement" )]
public class LightcycleMovement : MonoBehaviour {
    public float turnSpeed;
    public float moveSpeed;
    public float maxSpeed;
    public float acceleration;

    public float desiredSpeed;
    public float currentSpeed;

    public delegate void TurnRightCallback();
    public delegate void TurnLeftCallback();

    public TurnRightCallback onTurnRight;
    public TurnLeftCallback onTurnLeft;

    private Quaternion desiredRotation;

	// Use this for initialization
	void Start () {
        desiredRotation = transform.rotation;
        desiredSpeed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        performAcceleration();
        updateRotation();
        updatePosition();
	}

    Quaternion alignWithWorldAxis( Quaternion toAlign ) {
        // Ensure that the desired rotation points at one of the grid axeses
        Vector3 desiredRotationVector = toAlign * Vector3.forward;
        Debug.Log( "Desired rotation vector: " + desiredRotationVector );

        desiredRotationVector.x = snapValue( desiredRotationVector.x );
        desiredRotationVector.z = snapValue( desiredRotationVector.z );
        desiredRotationVector.y = 0.0f;

        Debug.Log( "Snapped rotation vector: " + desiredRotationVector );
        return Quaternion.LookRotation( desiredRotationVector );
    }

    float snapValue( float toSnap ) {
        if( toSnap > 0.5f ) {
            Debug.Log( "Snapping " + toSnap + " to 1.0" );
            return 1.0f;
        } else if( toSnap < -0.5f ) {
            Debug.Log( "Snapping " + toSnap + " to -1.0" );
            return -1.0f;
        } else {
            Debug.Log( "Snapping " + toSnap + " to 0.0" );
            return 0.0f;
        }
    }

    void performAcceleration() {
        if( currentSpeed < desiredSpeed ) {
            currentSpeed += acceleration * Time.deltaTime;
        } else if( currentSpeed > desiredSpeed ) {
            currentSpeed -= acceleration * Time.deltaTime;
        }
    }

    void updateRotation() {
        transform.rotation = Quaternion.Lerp( transform.rotation, desiredRotation, Time.deltaTime * turnSpeed );
        //transform.Rotate()
    }

    void updatePosition() {
        transform.Translate( Vector3.forward * currentSpeed * Time.deltaTime );
    }

    public void turnRight() {
        desiredRotation = Quaternion.LookRotation( transform.right, transform.up );
        desiredRotation = alignWithWorldAxis( desiredRotation );
        onTurnRight();
    }

    public void turnLeft() {
        desiredRotation = Quaternion.LookRotation( -transform.right, transform.up );
        desiredRotation = alignWithWorldAxis( desiredRotation );
        onTurnLeft();
    }

    public void startGlide() {

    }

    public void endGlide() {

    }
}
