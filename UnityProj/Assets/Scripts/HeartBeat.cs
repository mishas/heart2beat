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
	
	private enum Segment {
		PP_INTERVAL = 0,
		P_WAVE = 1,
		PR_SEGMENT = 2,
		QRS_COMPLEX = 3,
		ST_SEGMENT = 4,
		T_WAVE = 5,
	}
	
	private Segment currentSegment = Segment.PP_INTERVAL;
	private float segmentStartTime = 0f;
	private float segmentLength = -1f;
	
	private BeatSize GetPTWaveAtTime(float timeWithinSegment, BeatSize beatSize) {
		float intensity = -Mathf.Pow(2*timeWithinSegment/segmentLength - 1, 2) + 1f;
		return new BeatSize(beatSize.ElectricalPulse * intensity, beatSize.MechanicalPulse * intensity);
	}
	
	private BeatSize GetQRSWaveAtTime(float timeWithinSegment) {
		float quarterSegment = qrsComplex / 4;
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
				segmentLength = (60 / bpm) - prInterval - qtIntervale;
				break;
			case Segment.P_WAVE:
				segmentLength = prInterval - prSegment;
				break;
			case Segment.PR_SEGMENT:
				segmentLength = prSegment;
				break;
			case Segment.QRS_COMPLEX:
				segmentLength = qrsComplex;
				break;
			case Segment.ST_SEGMENT:
				segmentLength = stSegment;
				break;
			case Segment.T_WAVE:
				segmentLength = qtIntervale - qrsComplex - stSegment;
				break;
			}
		}
		
		switch (currentSegment) {
		case Segment.PP_INTERVAL:
		case Segment.PR_SEGMENT:
		case Segment.ST_SEGMENT:
			return new BeatSize(0f, 0f);
		case Segment.P_WAVE:
			return GetPTWaveAtTime(time - segmentStartTime, pSize);
		case Segment.QRS_COMPLEX:
			return GetQRSWaveAtTime(time - segmentStartTime);
		case Segment.T_WAVE:
			return GetPTWaveAtTime(time - segmentStartTime, tSize);
		}
		
		// Should never get here!
		Debug.Log ("Should never get here!");
		return new BeatSize(0f, 0f);
	}
	
	// TODO(misha): Need some randomness in beats.
	private float bpm = 60;
	private BeatSize pSize = new BeatSize(0.2f, 0.2f);
	private BeatSize qSize = new BeatSize(-0.1f, -0.1f);
	private BeatSize rSize = new BeatSize(0.8f, 0.8f);
	private BeatSize sSize = new BeatSize(-0.4f, -0.4f);
	private BeatSize tSize = new BeatSize(0.3f, 0f);
	private float prInterval = 0.2f;
	private float prSegment = 0.08f;
	private float qrsComplex = 0.12f;
	private float stSegment = 0.08f;
	private float qtIntervale = 0.36f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
