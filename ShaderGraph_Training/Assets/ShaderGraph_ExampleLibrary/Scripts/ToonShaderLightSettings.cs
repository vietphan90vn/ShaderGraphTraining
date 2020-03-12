using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToonShaderLightSettings : MonoBehaviour
{
	private Light _light;

	void OnEnable()
	{
		_light = GetComponent<Light>();
	}

	void Update ()
	{
		Shader.SetGlobalVector("_ToonLightDirection", -transform.forward);
		Shader.SetGlobalColor("_ToonLightColor", _light.color);
	}
}
