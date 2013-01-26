using UnityEngine;
using System.Collections;

public class ChaseTransform : MonoBehaviour {
	
	public Transform transformToChase;
	public float smoothFactor = 1;
		
	private Vector3 targetDistance;
	private Vector3 smoothVelocity;
	
	// Use this for initialization
	void Start () {
		targetDistance = transformToChase.position - transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = transformToChase.position - targetDistance;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, Time.deltaTime * smoothFactor);
	}
}
