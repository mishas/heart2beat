using UnityEngine;
using System.Collections;

public class LoseCondition : MonoBehaviour {
	public Transform ball;
	public bool hasLost;
	
	public Rect textRect = new Rect(50,50,300,50);
	public GUIStyle textStyle;

	// Use this for initialization
	void Start () {
		hasLost = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ball.position.y < -25) {
			// We've Lost :(
			hasLost = true;
			StartCoroutine(DelayedGoToMenu());
		}
	}
	
		
	void OnGUI() {
		if (hasLost) {
			GUI.Label(textRect, "Looser!!", textStyle);
		}
	}
	
	IEnumerator DelayedGoToMenu() {
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("IntroScene");
	}
}
