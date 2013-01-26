using System;

public class NormalSynusRhythm : HeartRhythm {
	private static Random rand = new Random();
	
	public float jitterFactor = 0.05f;
	
	private float fixedRSize = 0.8f;
	private float jitteredRSize;
	
	public override HeartBeat.BeatSize GetPSize() {
		return new HeartBeat.BeatSize(0.2f * GetJitter(), 0f);
	}
	public override HeartBeat.BeatSize GetQSize() {
		jitteredRSize = fixedRSize * GetJitter();
		return new HeartBeat.BeatSize(-0.1f * GetJitter(), jitteredRSize);
	}
	public override HeartBeat.BeatSize GetRSize() {
		return new HeartBeat.BeatSize(jitteredRSize, jitteredRSize);
	}
	public override HeartBeat.BeatSize GetSSize() {
		return new HeartBeat.BeatSize(-0.4f * GetJitter(), jitteredRSize);
	}
	public override HeartBeat.BeatSize GetTSize() {
		return new HeartBeat.BeatSize(0.3f * GetJitter(), 0f);
	}
	
	public override float GetPPInterval() {
		return ((60 / bpm) - GetPRInterval() - GetQTInterval()) * GetJitter();
	}
	public override float GetPRInterval() {
		return 0.2f * GetJitter();
	}
	public override float GetPRSegment() {
		return 0.08f * GetJitter();
	}
	public override float GetQRSComplex() {
		return 0.12f * GetJitter();
	}
	public override float GetSTSegment() {
		return 0.08f * GetJitter();
	}
	public override float GetQTInterval() {
		return 0.36f * GetJitter();
	}
	
	private float GetJitter() {
		return 1 + (float) rand.NextDouble() * jitterFactor;
	}
}
