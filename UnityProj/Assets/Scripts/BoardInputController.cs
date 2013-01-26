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
	
	// Use this for initialization
	void Start () {
	
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
		
		targetTransform.localEulerAngles = eulerDegrees;
	}
}
