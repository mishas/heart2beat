using System;

public class VentricularFibrilation : HeartRhythm {
	private static Random rand = new Random();
	public override HeartBeat.BeatSize GetPSize() {
		return HeartBeat.ZERO_BEAT_SIZE;
	}
	public override HeartBeat.BeatSize GetQSize() {
		float size = -(float) rand.NextDouble() * 0.2f;
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetRSize() {
		float size = (float) rand.NextDouble() * 0.2f;
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetSSize() {
		float size = -(float) rand.NextDouble() * 0.2f;
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetTSize() {
		return HeartBeat.ZERO_BEAT_SIZE;
	}
	
	public override float GetPPInterval() {
		return 0f;
	}
	public override float GetPRInterval() {
		return 0f;
	}
	public override float GetPRSegment() {
		return 0f;
	}
	public override float GetQRSComplex() {
		return (float) rand.NextDouble() * 0.3f;
	}
	public override float GetSTSegment() {
		return 0f;
	}
	public override float GetQTInterval() {
		return 0f;
	}
}
