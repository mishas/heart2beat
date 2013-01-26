using System;

public class VentricularTrigeminy : HeartRhythm {
	private static Random rand = new Random();
	
	public float jitterFactor = 0.05f;
	private int beatIndex = 0;
	
	public override HeartBeat.BeatSize GetPSize() {
		beatIndex++;
		float size = 0.2f * GetJitter();
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetQSize() {
		float size = -0.1f * GetJitter();
		if (beatIndex % 3 == 0) {
			size = 1.2f * GetJitter();
		}
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetRSize() {
		float size = 0.8f * GetJitter();
		if (beatIndex % 3 == 0) {
			size = 1.2f * GetJitter();
		}
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetSSize() {
		float size = -0.3f * GetJitter();
		if (beatIndex % 3 == 0) {
			size = -0.2f * GetJitter();
		}
		return new HeartBeat.BeatSize(size, size);
	}
	public override HeartBeat.BeatSize GetTSize() {
		float size = 0.3f * GetJitter();
		if (beatIndex % 3 == 0) {
			size = -0.6f * GetJitter();
		}
		return new HeartBeat.BeatSize(size, 0f);
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
		if (beatIndex % 3 == 0) {
			return 0.3f * GetJitter();
		}
		return 0.12f * GetJitter();
	}
	public override float GetSTSegment() {
		if (beatIndex % 3 == 0) {
			return 0f * GetJitter();
		}
		return 0.08f * GetJitter();
	}
	public override float GetQTInterval() {
		if (beatIndex % 3 == 0) {
			return 0.5f * GetJitter();
		}
		return 0.36f * GetJitter();
	}
	
	private float GetJitter() {
		return 1 + (float) rand.NextDouble() * jitterFactor;
	}
}
