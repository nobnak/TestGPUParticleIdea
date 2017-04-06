using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GPUArray<T> : System.IDisposable, IEnumerable<T> where T : struct {
	public readonly int size;
	public readonly ComputeBuffer buffer;

	protected readonly T[] cpuBuffer;

	public GPUArray(int size) : this(size, ComputeBufferType.Default) {}
	public GPUArray(int size, ComputeBufferType bufferType) {
		this.size = size;
		this.buffer = new ComputeBuffer (size, Marshal.SizeOf (typeof(T)), bufferType);
		this.cpuBuffer = new T[size];
		Clear ();
	}

	#region Array
	public T this [int index] {
		get { return cpuBuffer [index]; }
		set { cpuBuffer [index] = value; }
	}
	#endregion

	public virtual void Clear() {
		System.Array.Clear (cpuBuffer, 0, cpuBuffer.Length);
		Upload ();
	}
	public void SetBuffer(ComputeShader cs, int kernel, string name) {
		SetBuffer (cs, kernel, Shader.PropertyToID (name));
	}
	public void SetBuffer(ComputeShader cs, int kernel, int name) {
		Upload();
		cs.SetBuffer (kernel, name, buffer);
	}
	public void Download() {
		buffer.GetData (cpuBuffer);
	}
	public void Upload () {
		buffer.SetData (cpuBuffer);
	}

	#region IDisposable implementation
	public void Dispose () {
		if (buffer != null)
			buffer.Dispose ();
	}
	#endregion

	#region IEnumerable implementation
	public IEnumerator<T> GetEnumerator () {
		foreach (var e in cpuBuffer)
			yield return e;
	}
	#endregion
	#region IEnumerable implementation
	IEnumerator IEnumerable.GetEnumerator () {
		return GetEnumerator ();
	}
	#endregion
}
