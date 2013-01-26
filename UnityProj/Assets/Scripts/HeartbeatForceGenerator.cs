using UnityEngine;
using System.Collections;

public class HeartbeatForceGenerator : MonoBehaviour {
	
	public HeartbeatHistory heartbeat;
	public Rigidbody targetBody;
	public float forceScale = 1f;
	public float minJumpHeight = 0.3f;
	
	public Transform referenceTransform;
	
	private float sphereRadius;
 
	// Use this for initialization
	void Start () {
		sphereRadius = targetBody.GetComponent<SphereCollider>().radius;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		HeartBeat.BeatSize beatSize = heartbeat.PastBeat;
		if (beatSize == null) {
			beatSize = HeartBeat.ZERO_BEAT_SIZE;
		}
		float pulse = beatSize.MechanicalPulse;
		
		Vector3 localUp = referenceTransform.TransformDirection(Vector3.up);
		Vector3 localDown = referenceTransform.TransformDirection(Vector3.down);
		
		if (Physics.Raycast(targetBody.position, localDown, (float)(sphereRadius + 0.1 * sphereRadius)) && pulse > 0 ){
			Debug.Log("Force at time " + Time.time + " : " + pulse);
			targetBody.AddForce(localUp * pulse * forceScale);
		}
		
	}
}
