using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test : MonoBehaviour {
	public GPUParticleRenderer gshuriken;

	public Transform[] positions;
	public float size = 1f;

	void Update() {
        if (gshuriken == null || gshuriken.Particles == null || positions == null)
            return;
        
		var particles = gshuriken.Particles;
		var count = particles.Count;

		for (var i = count - positions.Length; i > 0; i--)
			particles.Pop ();
		for (var i = 0; i < count; i++)
			particles [i] = new GPUParticleRenderer.Particle (positions [i].position, size);
		for (var i = count; i < positions.Length; i++)
			particles.Push (new GPUParticleRenderer.Particle (positions [i].position, size));
	}
}
