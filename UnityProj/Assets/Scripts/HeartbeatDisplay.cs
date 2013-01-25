using UnityEngine;
using System.Collections;

public class HeartbeatDisplay : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localPosition = transform.localPosition;
		localPosition.y = Mathf.Sin(Time.time);
		//localPosition.x = Time.time;
		transform.localPosition = localPosition;
	}
	
}
