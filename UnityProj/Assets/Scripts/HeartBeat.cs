using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	
	/**
	 * BeatSize represents the electrical and mechanical beat of the heart in
	 * some point in time.
	 * 
	 * This class has 2 properties:
	 *   ElectricalSize: The strangth of the electrical pulse at given time in mV.
	 *   MechanicalSize: The strangth of the mecahnical beat at given time in Joule*1.6e-25
	 */
	public class BeatSize {
		public BeatSize(float electricalPulse, float mechanicalBeat) {
			this.electricalPulse = electricalPulse;
			this.mechanicalBeat = mechanicalBeat;
		}
		
		public float ElectricalPulse {
			get { return electricalPulse; }
		}
		
		public float MechanicalPulse {
			get { return mechanicalBeat; }
		}
		
		float electricalPulse;
		float mechanicalBeat;
	}
	
	private static BeatSize ZERO_BEAT_SIZE = new BeatSize(0f, 0f);
	
	private enum Segment {
		PP_INTERVAL = 0,
		P_WAVE = 1,
		PR_SEGMENT = 2,
		QRS_COMPLEX = 3,
		ST_SEGMENT = 4,
		T_WAVE = 5,
	}
	
	private HeartRhythm currentRhythm = new NormalSynusRhythm();
	private Segment currentSegment = Segment.PP_INTERVAL;
	private float segmentStartTime = 0f;
	private float segmentLength = -1f;
	private BeatSize[] segmentBeatSize = {ZERO_BEAT_SIZE};
	
	private BeatSize GetPTWaveAtTime(float timeWithinSegment, BeatSize beatSize) {
		float intensity = -Mathf.Pow(2*timeWithinSegment/segmentLength - 1, 2) + 1f;
		return new BeatSize(beatSize.ElectricalPulse * intensity, beatSize.MechanicalPulse * intensity);
	}
	
	private BeatSize GetQRSWaveAtTime(float timeWithinSegment, BeatSize qSize, BeatSize rSize, BeatSize sSize) {
		float quarterSegment = segmentLength / 4;
		float intensity = (timeWithinSegment % quarterSegment) / quarterSegment;
		if (timeWithinSegment < quarterSegment) {
			return new BeatSize(qSize.ElectricalPulse * intensity, qSize.MechanicalPulse * intensity);
		}
		if (timeWithinSegment < 2*quarterSegment) {
			return new BeatSize(qSize.ElectricalPulse * (1-intensity) + rSize.ElectricalPulse * intensity,
				qSize.MechanicalPulse * (1-intensity) + rSize.MechanicalPulse * intensity);
		}
		if (timeWithinSegment < 3*quarterSegment) {
			return new BeatSize(rSize.ElectricalPulse * (1-intensity) + sSize.ElectricalPulse * intensity,
				rSize.MechanicalPulse * (1-intensity) + sSize.MechanicalPulse * intensity);
		}	
		return new BeatSize(sSize.ElectricalPulse * (1-intensity), sSize.MechanicalPulse * (1-intensity));
	}
	
	public BeatSize GetBeatAtTime(float time) {
		if (time - segmentStartTime > segmentLength) {
			currentSegment = (Segment) (((int) currentSegment + 1) % 6);
			segmentStartTime = time;
			switch (currentSegment) {
			case Segment.PP_INTERVAL:
				segmentLength = (60 / currentRhythm.GetBPM()) - currentRhythm.GetPRInterval() - currentRhythm.GetQTInterval();
				segmentBeatSize = new BeatSize[]{ZERO_BEAT_SIZE};
				break;
			case Segment.P_WAVE:
				segmentLength = currentRhythm.GetPRInterval() - currentRhythm.GetPRSegment();
				segmentBeatSize = new BeatSize[]{currentRhythm.GetPSize()};
				break;
			case Segment.PR_SEGMENT:
				segmentLength = currentRhythm.GetPRSegment();
				segmentBeatSize = new BeatSize[]{ZERO_BEAT_SIZE};
				break;
			case Segment.QRS_COMPLEX:
				segmentLength = currentRhythm.GetQRSComplex();
				segmentBeatSize = new BeatSize[]{currentRhythm.GetQSize(), currentRhythm.GetRSize(), currentRhythm.GetSSize()};
				break;
			case Segment.ST_SEGMENT:
				segmentLength = currentRhythm.GetSTSegment();
				segmentBeatSize = new BeatSize[]{ZERO_BEAT_SIZE};
				break;
			case Segment.T_WAVE:
				segmentLength = currentRhythm.GetQTInterval() - currentRhythm.GetQRSComplex() - currentRhythm.GetSTSegment();
				segmentBeatSize = new BeatSize[]{currentRhythm.GetTSize()};
				break;
			}
		}
		
		switch (currentSegment) {
		case Segment.PP_INTERVAL:
		case Segment.PR_SEGMENT:
		case Segment.ST_SEGMENT:
			return segmentBeatSize[0];
		case Segment.P_WAVE:
			return GetPTWaveAtTime(time - segmentStartTime, segmentBeatSize[0]);
		case Segment.QRS_COMPLEX:
			return GetQRSWaveAtTime(time - segmentStartTime, segmentBeatSize[0], segmentBeatSize[1], segmentBeatSize[2]);
		case Segment.T_WAVE:
			return GetPTWaveAtTime(time - segmentStartTime, segmentBeatSize[0]);
		}
		
		// Should never get here!
		Debug.Log ("Should never get here!");
		return new BeatSize(0f, 0f);
	}

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }
}
