using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ScrollBackground : MonoBehaviour
    {
        private Material backgroundMaterial;

        private void FindReferences()
        {
            backgroundMaterial = GetComponent<MeshRenderer>().material;
        }

        void Awake()
        {
            FindReferences();
        }

        void Update()
        {
            var offset = backgroundMaterial.mainTextureOffset;
            offset.y += Time.deltaTime /10f;
            backgroundMaterial.mainTextureOffset = offset;
        }
    }
}
