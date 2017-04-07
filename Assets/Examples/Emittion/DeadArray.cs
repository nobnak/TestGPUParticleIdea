using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using BasicGPUParticle;

public class DeadArray : GPUArray<uint> {

	public DeadArray(int size) : base(size, ComputeBufferType.Append) {
	}

	public override void Clear() {
		for (var i = 0; i < cpuBuffer.Length; i++)
			cpuBuffer [i] = (uint)(cpuBuffer.Length - 1 - i);
		GPUBuffer.SetCounterValue ((uint)Capacity);
		Upload ();
	}
}
