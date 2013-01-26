using UnityEngine;
using System.Collections;

public class HeartbeatShaker : MonoBehaviour {
	
	public HeartbeatHistory heartbeatSource;
	public float shakeScale = 1;
	
	private float nodeVelocity = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HeartBeat.BeatSize beatSize = heartbeatSource.PastBeat;
		Vector3 localPosition = transform.localPosition;
		float targetY = beatSize.MechanicalPulse * shakeScale;
		localPosition.y = Mathf.SmoothDamp(localPosition.y, targetY, ref nodeVelocity, Time.deltaTime);
		transform.localPosition = localPosition;
	}
}
