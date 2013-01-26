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
	
	public static BeatSize ZERO_BEAT_SIZE = new BeatSize(0f, 0f);
	
	private enum Segment {
		PP_INTERVAL = 0,
		P_WAVE = 1,
		PR_SEGMENT = 2,
		QRS_COMPLEX = 3,
		ST_SEGMENT = 4,
		T_WAVE = 5,
	}
	
	public HeartRhythm currentRhythm;
	private Segment currentSegment = Segment.PP_INTERVAL;
	private float segmentStartTime = 0f;
	private float segmentLength = -1f;
	private BeatSize[] segmentBeatSize = {ZERO_BEAT_SIZE};
	
	private BeatSize[] beatSizeMemory = new BeatSize[500];
	private int indexInBeatSizeMemory = 0;
	
	private BeatSize GetPTWaveAtTime(float timeWithinSegment, BeatSize beatSize) {
		float intensity = -Mathf.Pow(2*timeWithinSegment/segmentLength - 1, 2) + 1f;
		return new BeatSize(beatSize.ElectricalPulse * intensity, beatSize.MechanicalPulse * intensity);
	}
	
	// Note: In order to give relatively stable force up, the whole QRS complex gives MechanicalPulse of the maximal R size.
	private BeatSize GetQRSWaveAtTime(float timeWithinSegment, BeatSize qSize, BeatSize rSize, BeatSize sSize) {
		float quarterSegment = segmentLength / 4;
		float intensity = (timeWithinSegment % quarterSegment) / quarterSegment;
		float mechanicalPulse = rSize.MechanicalPulse;
		if (timeWithinSegment < quarterSegment) {
			return new BeatSize(qSize.ElectricalPulse * intensity, mechanicalPulse);
		}
		if (timeWithinSegment < 2*quarterSegment) {
			return new BeatSize(qSize.ElectricalPulse * (1-intensity) + rSize.ElectricalPulse * intensity, mechanicalPulse);
		}
		if (timeWithinSegment < 3*quarterSegment) {
			return new BeatSize(rSize.ElectricalPulse * (1-intensity) + sSize.ElectricalPulse * intensity, mechanicalPulse);
		}	
		return new BeatSize(sSize.ElectricalPulse * (1-intensity), mechanicalPulse);
	}
	
	public BeatSize GetBeatAtFramesBehind(int framesBehind) {
		return beatSizeMemory[(beatSizeMemory.Length + indexInBeatSizeMemory - framesBehind) % beatSizeMemory.Length];
	}
	
	public BeatSize GetBeatAtTime(float time) {
		if (time - segmentStartTime > segmentLength) {
			currentSegment = (Segment) (((int) currentSegment + 1) % 6);
			segmentStartTime = time;
			switch (currentSegment) {
			case Segment.PP_INTERVAL:
				segmentLength = currentRhythm.GetPPInterval();
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
			beatSizeMemory[indexInBeatSizeMemory++ % beatSizeMemory.Length] = segmentBeatSize[0];
			break;
		case Segment.P_WAVE:
			beatSizeMemory[indexInBeatSizeMemory++% beatSizeMemory.Length] =
				GetPTWaveAtTime(time - segmentStartTime, segmentBeatSize[0]);
			break;
		case Segment.QRS_COMPLEX:
			beatSizeMemory[indexInBeatSizeMemory++% beatSizeMemory.Length] =
				GetQRSWaveAtTime(time - segmentStartTime, segmentBeatSize[0], segmentBeatSize[1], segmentBeatSize[2]);
			break;
		case Segment.T_WAVE:
			beatSizeMemory[indexInBeatSizeMemory++% beatSizeMemory.Length] =
				GetPTWaveAtTime(time - segmentStartTime, segmentBeatSize[0]);
			break;
		}
		
		return beatSizeMemory[(indexInBeatSizeMemory-1) % beatSizeMemory.Length];
	}

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }
}
