using UnityEngine;
using System.Collections;

public class HeartbeatForceGenerator : MonoBehaviour {
	
	public HeartBeat heartbeat;
	public Rigidbody targetBody;
	public float forceScale = 1f;
	
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
		HeartBeat.BeatSize beatSize = heartbeat.GetBeatAtTime(Time.time);
		float pulse = beatSize.MechanicalPulse;
		if (referenceTransform.InverseTransformPoint(targetBody.position).y - sphereRadius < (0.1 * sphereRadius) && pulse != 0 ){
			Vector3 localUp = referenceTransform.TransformDirection(Vector3.up);
			targetBody.AddForce(localUp * pulse * forceScale);
		}
		
	}
}
