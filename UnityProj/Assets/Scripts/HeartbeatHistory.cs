using UnityEngine;
using System.Collections.Generic;

public class HeartbeatHistory : MonoBehaviour {
	
	public HeartBeat sourceHeartBeat;
	public float queueTimeSeconds = 2;
	
	internal struct HeartBeatEntry {
		public float time;
		public HeartBeat.BeatSize beatSize;
	}
	
	internal Queue<HeartBeatEntry> entries;
	
	// Use this for initialization
	void Start () {
		entries = new Queue<HeartBeatEntry>();
	}
	
	// Update is called once per frame
	void Update () {
		float curTime = Time.time;
		
		HeartBeatEntry entry = new HeartBeatEntry();
		entry.time = curTime;
		entry.beatSize = sourceHeartBeat.GetBeatAtTime(curTime);
		entries.Enqueue(entry);
		Debug.Log("History entry : " + entry.beatSize.MechanicalPulse);
		while (entries.Peek().time < curTime - queueTimeSeconds) {
			entries.Dequeue();
		}

	}
	
	public HeartBeat.BeatSize PastBeat {
		get {
			if (entries.Count == 0 || Time.time < queueTimeSeconds) {
				return new HeartBeat.BeatSize(0,0);
			}
			return entries.Peek().beatSize;
		}
	}
}
