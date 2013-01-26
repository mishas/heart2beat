using UnityEngine;
using System.Collections;

public class HeartbeatForceGenerator : MonoBehaviour {
	
	public HeartBeat heartbeat;
	public Rigidbody targetBody;
	public float forceScale = 1f;
	
	public Transform referenceTransform;
 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		HeartBeat.BeatSize beatSize = heartbeat.getBeatAtTime(Time.time);
		float pulse = beatSize.MechanicalPulse;
		
		targetBody.AddForce(Vector3.up * pulse * forceScale);
	}
}
