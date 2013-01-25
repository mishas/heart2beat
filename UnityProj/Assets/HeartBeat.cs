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
	
	// TODO(misha): Need some randomness in beats.
	private float startTimeout = 0;
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
	
	public BeatSize getBeatAtTime(float time) {
		time -= startTimeout;
		float fullComplexSize = prInterval + qtIntervale;
		float complexAndSpaceSize = (60 - bpm * fullComplexSize) / bpm;
		// Move to use %
		float moduluTime = time - (complexAndSpaceSize * (int) (time / complexAndSpaceSize));
		
		if (moduluTime < prInterval) {
			if (moduluTime < prInterval - prSegment) {
				float intensity = -Mathf.Pow(moduluTime/(prInterval-prSegment) - 0.5f, 2f) + 1f;
				return new BeatSize(pSize.ElectricalPulse * intensity, pSize.MechanicalPulse * intensity);
			} else {
				return new BeatSize(0f, 0f);
			}
		}
		if (moduluTime < prInterval + qrsComplex) {
			moduluTime -= prInterval;
			float quoter = moduluTime / 4;
			float intensity = (moduluTime % (moduluTime / 4)) / quoter;
			if (moduluTime < quoter) {
				return new BeatSize(qSize.ElectricalPulse * intensity, qSize.MechanicalPulse * intensity);
			}
			if (moduluTime < 2*quoter) {
				return new BeatSize(qSize.ElectricalPulse * (1-intensity) + rSize.ElectricalPulse * intensity,
					qSize.MechanicalPulse * (1-intensity) + rSize.MechanicalPulse * intensity);
			}
			if (moduluTime < 3*quoter) {
				return new BeatSize(rSize.ElectricalPulse * (1-intensity) + sSize.ElectricalPulse * intensity,
					rSize.MechanicalPulse * (1-intensity) + sSize.MechanicalPulse * intensity);
			}	
			return new BeatSize(sSize.ElectricalPulse * (1-intensity), sSize.MechanicalPulse * (1-intensity));
		}
		if (moduluTime < prInterval + qrsComplex + stSegment) {
			return new BeatSize(0f, 0f);
		}
		if (moduluTime < prInterval + qtIntervale) {
			moduluTime -= prInterval + qrsComplex + stSegment;
			float intensity = -Mathf.Pow(moduluTime/(qtIntervale-qrsComplex-stSegment) - 0.5f, 2f) + 1f;
			return new BeatSize(tSize.ElectricalPulse * intensity, tSize.MechanicalPulse * intensity);
		}
		// We're in the space between beats.
		return new BeatSize(0f, 0f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
