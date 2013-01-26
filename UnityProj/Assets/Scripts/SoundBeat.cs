using UnityEngine;

public class SoundBeat : MonoBehaviour {
	
	public HeartbeatHistory heartBeatHistory;
	
	public long lastPlayedId;
	
	public AudioSource targetClip;
	
	void Start () {
		
	}
	
	void FixedUpdate () {
		if (heartBeatHistory.PastBeatId != lastPlayedId){
			targetClip.Play();
			lastPlayedId = heartBeatHistory.PastBeatId;
		}
	}
}
