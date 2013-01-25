using System;

public class NormalSynusRhythm : HeartRhythm {
	public override HeartBeat.BeatSize GetPSize() {
		return new HeartBeat.BeatSize(0.2f, 0.2f);
	}
	public override HeartBeat.BeatSize GetQSize() {
		return new HeartBeat.BeatSize(-0.1f, -0.1f);
	}
	public override HeartBeat.BeatSize GetRSize() {
		return new HeartBeat.BeatSize(0.8f, 0.8f);
	}
	public override HeartBeat.BeatSize GetSSize() {
		return new HeartBeat.BeatSize(-0.4f, -0.4f);
	}
	public override HeartBeat.BeatSize GetTSize() {
		return new HeartBeat.BeatSize(0.3f, 0f);
	}
	
	public override float GetPPInterval() {
		return (60 / bpm) - GetPRInterval() - GetQTInterval();
	}
	public override float GetPRInterval() {
		return 0.2f;
	}
	public override float GetPRSegment() {
		return 0.08f;
	}
	public override float GetQRSComplex() {
		return 0.12f;
	}
	public override float GetSTSegment() {
		return 0.08f;
	}
	public override float GetQTInterval() {
		return 0.36f;
	}
}
