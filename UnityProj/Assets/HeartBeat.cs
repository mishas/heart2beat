using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	
	/**
	 * BeatSize represents the electrical and mechanical beat of the heart in
	 * some point in time.
	 * 
	 * This class has 2 properties:
	 *   ElectricalSize: The strangth of the electrical pulse at given time in mV.
	 *   MechanicalSize: The strangth of the mecahnical beat at given time in Joule*1.6e-19
	 */
	public class BeatSize {
		public BeatSize(float electricalPulse, float mechanicalPulse) {
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
	
	public BeatSize getBeatAtTime(float time) {
		// He's dead jim.
		return new BeatSize(0f, 0f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
