using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PostProcessSilhouete : MonoBehaviour
{
    public Material material;
    public Camera secondaryCam;
    public bool useEffect;
    [SerializeField] private LayerMask layerTarget;
    private RenderTexture render;

    public LayerMask SetLayerTarget {
        set
        {
            layerTarget = value;
            secondaryCam.cullingMask = value;
        }
    }

    private void Start()
    {
        render = new RenderTexture(Screen.width, Screen.height,0);
        secondaryCam.targetTexture = render;
        secondaryCam.cullingMask = layerTarget;
        secondaryCam.clearFlags = CameraClearFlags.SolidColor;
        material.SetTexture("_SilTex", render);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture output)
    {
        if (material != null && useEffect)
        {
            Graphics.Blit(source, output, material);
        }
        else
        {
            Graphics.Blit(source, output);
        }
    }

    public void SetEffectColors(Color background,Color target)
    {
        material.SetColor("_BackColor", background);
        material.SetColor("_SilColor", target);
    }

    public void SetSliderValues(float background,float target)
    {
        material.SetFloat("_BackSlider", Mathf.Clamp01(background));
        material.SetFloat("_SilSlider", Mathf.Clamp01(target));
    }

    public void SetBackValue(float background)
    { material.SetFloat("_BackSlider", Mathf.Clamp01(background)); }

    public void SetSilhoueteValue(float target)
    { material.SetFloat("_SilSlider", Mathf.Clamp01(target)); }

}
