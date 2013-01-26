using UnityEngine;
using System.Collections;

public class BoardInputController : MonoBehaviour {
	
	public KeyCode rotateLeftButton = KeyCode.A;
	public KeyCode rotateRightButton = KeyCode.D;
	public KeyCode tiltUpButton = KeyCode.W;
	public KeyCode tildDownButton = KeyCode.S;
	
	public Vector2 rotationLimitDegrees = new Vector2(30f,30f);
	public float rotationSpeed = 30;
	
	public Transform targetTransform;
	
	private Vector3 lastVector3;
	
	// Use this for initialization
	void Start () {
		lastVector3 = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
	}
	
	float ProcessAngle(float currentAngle, int changeDirection, bool change, float angleLimit) {
		if (currentAngle > 180f) {
			currentAngle -= 360f;
		}
		if (change) {
			currentAngle += rotationSpeed * changeDirection * Time.deltaTime;
			if (Mathf.Abs(currentAngle) > angleLimit) {
				currentAngle = angleLimit * Mathf.Sign(currentAngle);
			}
		}
		return currentAngle;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector3 eulerDegrees = targetTransform.localEulerAngles;
		
		eulerDegrees.x = ProcessAngle(eulerDegrees.x, 1, Input.GetKey(tiltUpButton), rotationLimitDegrees.x);
		eulerDegrees.x = ProcessAngle(eulerDegrees.x, -1, Input.GetKey(tildDownButton), rotationLimitDegrees.x);
		eulerDegrees.z = ProcessAngle(eulerDegrees.z, 1, Input.GetKey(rotateLeftButton), rotationLimitDegrees.y);
		eulerDegrees.z = ProcessAngle(eulerDegrees.z, -1, Input.GetKey(rotateRightButton), rotationLimitDegrees.y);
		
		if (Input.acceleration.x != 0 || Input.acceleration.y != 0 || Input.acceleration.z != 0) {
			lastVector3 = Vector3.Lerp(lastVector3, Input.acceleration, 0.2f);
			eulerDegrees.x = lastVector3.y * 360 / (2 * Mathf.PI);
			if (eulerDegrees.x > rotationLimitDegrees.x) {
				eulerDegrees.x = rotationLimitDegrees.x;
			} else if (eulerDegrees.x < -rotationLimitDegrees.x) {
				eulerDegrees.x = -rotationLimitDegrees.x;
			}
			eulerDegrees.z = -lastVector3.x * 360 / (2 * Mathf.PI);
			if (eulerDegrees.z > rotationLimitDegrees.y) {
				eulerDegrees.z = rotationLimitDegrees.y;
			} else if (eulerDegrees.z < -rotationLimitDegrees.y) {
				eulerDegrees.z = -rotationLimitDegrees.y;
			}
		}
		
		targetTransform.localEulerAngles = eulerDegrees;
	}
}
