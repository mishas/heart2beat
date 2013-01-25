using UnityEngine;
using System.Collections;

public abstract class HeartRhythm : MonoBehaviour {
	// Instead of BPM, maybe hold GetPPInterval?
	public abstract float GetBPM();
	
	public abstract HeartBeat.BeatSize GetPSize();
	public abstract HeartBeat.BeatSize GetQSize();
	public abstract HeartBeat.BeatSize GetRSize();
	public abstract HeartBeat.BeatSize GetSSize();
	public abstract HeartBeat.BeatSize GetTSize();
	
	public abstract float GetPRInterval();
	public abstract float GetPRSegment();
	public abstract float GetQRSComplex();
	public abstract float GetSTSegment();
	public abstract float GetQTInterval();
}
