using System;
using UnityEngine;

namespace _3._Scripts.Game
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FogPlane: MonoBehaviour
    {
        private static readonly int Property = Shader.PropertyToID("_FogColor");

        private void Start()
        {
            var component = GetComponent<MeshRenderer>();
            var theme = GameManager.CurrentTheme.FogSetting;
            
            component.material.SetColor(Property, theme.floorFog);
            RenderSettings.fog = theme.enableBackgroundFog;
            RenderSettings.fogColor = theme.backgroundFog;
            RenderSettings.fogStartDistance = theme.startValue;
            RenderSettings.fogEndDistance = theme.endValue;
        }
    }
}