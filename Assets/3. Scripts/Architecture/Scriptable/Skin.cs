using System;
using System.Collections.Generic;
using _3._Scripts.Architecture.Enums;
using UnityEngine;

namespace _3._Scripts.Architecture.Scriptable
{
    [CreateAssetMenu(fileName = "New Skin", menuName = "Configs/Skins/Skin")]
    public class Skin: ScriptableObject
    {
        [Header("Visual")]
        [SerializeField] private Mesh mesh;
        [SerializeField] private Material[] materials;
        [SerializeField] private Color color;
        [Header("Shop Settings")] 
        [SerializeField] private Sprite image;
        [SerializeField] private BuyType buyType;

        public string Name => $"{name}";
        public Sprite Image => image;
        public BuyType BuyType => buyType;
        public Color Color => color;
        public void LoadSkin(MeshRenderer meshRenderer, MeshFilter meshFilter)
        {
            meshFilter.mesh = mesh;
            meshRenderer.materials = materials;
        }

        public void ChangeIcon(Sprite newIcon)
        {
            image = newIcon;
        }
    }
}