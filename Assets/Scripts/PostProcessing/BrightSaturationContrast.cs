using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightSaturationContrast : BPostProcessing
{
    public Shader bscShader;
    private Material bscMaterial;

    public Material material
    {
        get
        {
            Debug.Log(1);
            bscMaterial = CheckShaderAndCreateMaterial(bscShader, bscMaterial);
            return bscMaterial;
        }
    }

    [Range(0, 3.0f)]
    public float brightness = 1.0f;
    [Range(0, 3.0f)]
    public float saturation = 1.0f;
    [Range(0, 3.0f)]
    public float contrast = 1.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log(1);
        if (material != null)
        {
            material.SetFloat("_Brightness", brightness);
            material.SetFloat("_Saturation", saturation);
            material.SetFloat("_Contrast", contrast);

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
