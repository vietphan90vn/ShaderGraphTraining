using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SetShaderGlobalTime : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_GlobalTime", Time.time);
    }
}