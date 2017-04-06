using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using BasicGPUParticle;
using System.Text;

public class EmissionTest : MonoBehaviour {
	public const string PROP_EMIT_LIST = "emits";
	public const string PROP_FLAG_LIST = "flags";
	public const string PROP_DEAD_LIST = "deads";
	public const string PROP_PARTICLE_LIST = "particles";

	public int emitCount = 4;

	public int emitCountLimit = 64;
	public int particleCountLimit = 64;
	public ComputeShader emit;

	ComputeBuffer emitBuffer;
	GPUArray<uint> flagBuffer;
	ComputeBuffer particleBuffer;
	DeadArray deadBuffer;

	void OnEnable() {
		emitBuffer = new ComputeBuffer (emitCountLimit, Marshal.SizeOf (typeof(GPUParticle)));
		flagBuffer = new GPUArray<uint> (particleCountLimit);
		particleBuffer = new ComputeBuffer (particleCountLimit, Marshal.SizeOf (typeof(GPUParticle)));
		deadBuffer = new DeadArray (particleCountLimit);
	}
	void Start() {
		#if true
		emit.SetBuffer (0, PROP_EMIT_LIST, emitBuffer);
		flagBuffer.SetBuffer(emit, 0, PROP_FLAG_LIST);
		emit.SetBuffer (0, PROP_PARTICLE_LIST, particleBuffer);
		deadBuffer.SetBuffer(emit, 0, PROP_DEAD_LIST);
		emit.Dispatch (0, emitCount, 1, 1);
		#endif

		flagBuffer.Download ();
		Debug.Log (ToString (flagBuffer, "Flag Buffer : ", "{0},"));
	}
	void OnDisable() {
		emitBuffer.Release ();
		flagBuffer.Dispose ();
		particleBuffer.Release ();
	}

	static string ToString<T>(IEnumerable<T> data, string label, string valueFormat) {
		var writer = new StringBuilder (label);
		foreach (var e in data)
			writer.AppendFormat (valueFormat, e);
		return writer.ToString ();
	}
}
