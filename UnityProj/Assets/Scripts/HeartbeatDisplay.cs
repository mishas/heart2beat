using UnityEngine;
using System.Collections;

public class HeartbeatDisplay : MonoBehaviour {
	
	public HeartBeat heartbeat;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localPosition = transform.localPosition;
		HeartBeat.BeatSize beatSize = heartbeat.getBeatAtTime(Time.time);
		localPosition.y = beatSize.ElectricalPulse;
		//localPosition.y = Mathf.Sin(Time.time);
		//localPosition.x = Time.time;
		transform.localPosition = localPosition;
		
	}
	
}
