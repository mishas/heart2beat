using UnityEngine;
using System.Collections;

public class WinConditionCollider : MonoBehaviour {
	
	public Collider targetCollider;
	public bool hasWon;
	public Rect textRect = new Rect(50,50,300,50);
	public GUIStyle textStyle;
	
	// Use this for initialization
	void Start () {
		hasWon = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other == targetCollider) {
			hasWon = true;
			StartCoroutine(DelayedGoToMenu());
		}
	}
	
	void OnGUI() {
		if (hasWon) {
			GUI.Label(textRect, "You Win!", textStyle);
		}
	}
	
	IEnumerator DelayedGoToMenu() {
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("IntroScene");
	}
		
}
