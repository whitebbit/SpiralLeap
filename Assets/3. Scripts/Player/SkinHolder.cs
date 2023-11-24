using _3._Scripts.Architecture.Scriptable;
using UnityEngine;

namespace _3._Scripts.Player
{
    public class SkinHolder
    {
        public Skin skin { get; private set; }
        private readonly MeshRenderer _meshRenderer;
        private readonly MeshFilter _meshFilter;

        public SkinHolder(MeshRenderer meshRenderer, MeshFilter meshFilter)
        {
            _meshRenderer = meshRenderer;
            _meshFilter = meshFilter;
        }
        
        public void ChangeSkin(Skin skin)
        {
            skin.LoadSkin(_meshRenderer, _meshFilter);
            this.skin = skin;
        }
    }
}