using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {
	
	
	public float turnsPerSecond = 1f;
	public KeyCode turnMoreKey = KeyCode.D;
	public KeyCode turnLessKey = KeyCode.A;
	public Rect labelPosition = new Rect(50,50,300,30);
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(turnMoreKey)) {
			turnsPerSecond += 1 * Time.deltaTime;
		} else if (Input.GetKey(turnLessKey)) {
			turnsPerSecond -= 1 * Time.deltaTime;
		}
		transform.RotateAroundLocal (Vector3.up,Time.deltaTime * turnsPerSecond / ( 2 * Mathf.PI) );
	}
	
	void OnGUI() {
		if (GUI.Button(labelPosition, "Speed : " + turnsPerSecond.ToString())) {
			turnsPerSecond *= -1;
		}
	}
	
}
