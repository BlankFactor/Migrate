using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BPostProcessing : MonoBehaviour
{
    void Start()
    {
        CheckResources();
    }

    protected void CheckResources()
    {
        bool isSupported = CheckSupport();

        if (!isSupported)
            NotSupported();
    }

    protected bool CheckSupport()
    {
#pragma warning disable CS0618 // 类型或成员已过时
        if (SystemInfo.supportsImageEffects == false || SystemInfo.supportsRenderTextures == false)
        {
#pragma warning restore CS0618 // 类型或成员已过时
            Debug.LogWarning("Not support");
            return false;
        }
        return true;
    }

    protected void NotSupported()
    {
        enabled = false;
    }

    protected Material CheckShaderAndCreateMaterial(Shader _s, Material _m)
    {
        if (_s.Equals(null))
        {
            return null;
        }

        if (_s.isSupported && _m && _m.shader == _s)
        {
            return _m;
        }

        if (!_s.isSupported)
        {
            return null;
        }
        else
        {
            _m = new Material(_s);
            _m.hideFlags = HideFlags.DontSaveInEditor;

            if (_m)
                return _m;
            else
                return null;
        }
    }
}
