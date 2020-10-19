using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used for packing pbr textures for material
/// </summary>
[CreateAssetMenu(fileName = "PBRTexturePack", menuName = "My/Data/PBR Textures Pack")]
public class PBRMaterialTexturePack : ScriptableObject
{
    [SerializeField] private Texture2D _albedoTexture;
    [SerializeField] private Texture2D _metallicSmoothnessTexture;
    [SerializeField] private Texture2D _normalTexture;
    [SerializeField] private float _metallicMultiplier = 1;
    [SerializeField] private float _smoothnessMultiplier = 1;

    public Texture2D AlbedoTexture { get => _albedoTexture; set => _albedoTexture = value; }
    public Texture2D MetallicSmoothnessTexture { get => _metallicSmoothnessTexture; set => _metallicSmoothnessTexture = value; }
    public Texture2D NormalTexture { get => _normalTexture; set => _normalTexture = value; }
    public float MetallicMultiplier { get => _metallicMultiplier; set => _metallicMultiplier = value; }
    public float SmoothnessMultiplier { get => _smoothnessMultiplier; set => _smoothnessMultiplier = value; }

}
