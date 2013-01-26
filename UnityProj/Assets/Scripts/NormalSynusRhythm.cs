using System;

public class NormalSynusRhythm : HeartRhythm {
	private static Random rand = new Random();
	
	public float jitterFactor = 0.05f;
	
	public override HeartBeat.BeatSize GetPSize() {
		return new HeartBeat.BeatSize(0.2f * GetJitter(), 0f);
	}
	public override HeartBeat.BeatSize GetQSize() {
		float size = -0.1f * GetJitter();
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetRSize() {
		float size = 0.8f * GetJitter();
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetSSize() {
		float size = -0.4f * GetJitter();
		return new HeartBeat.BeatSize(size, size);
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
