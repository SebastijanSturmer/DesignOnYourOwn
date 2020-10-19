using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInteractableController : InteractableController
{

    [SerializeField] private List<PBRMaterialTexturePack> _texturePacks = new List<PBRMaterialTexturePack>();
    [SerializeField] private int _changableMaterialIndex = 0;
    [SerializeField] private bool _canChangeRotationAndScale = false;
    [SerializeField] private PBRMaterialTexturePack _defaultTexturePack;
    [SerializeField] private bool _shouldSetDefaultMaterialPropertiesFromMaterial = true;
    [SerializeField] private Color _defaultMaterialColor = Color.white;
    [SerializeField] private float _defaultMaterialRotation = 0f;
    [SerializeField] private float _defaultMaterialScale = 1f;

    private MaterialPropertyBlock _propertyBlock;
    private Renderer _renderer;
    private PBRMaterialTexturePack _currentlySelectedTexturePack;
    private Color _currentlySelectedColor = Color.white;
    private float _currentMaterialRotation = 0;
    private float _currentMaterialScale = 0;


    public bool CanChangeRotationAndScale { get => _canChangeRotationAndScale; }
    public float CurrentMaterialRotation { get => _currentMaterialRotation; }
    public float CurrentMaterialScale { get => _currentMaterialScale; }
    public PBRMaterialTexturePack CurrentlySelectedTexturePack { get => _currentlySelectedTexturePack; }
    public Color CurrentlySelectedColor { get => _currentlySelectedColor; }
    public Renderer Renderer { get => _renderer; }
    public Material ChangeableMaterial { get => _renderer.material; }
    public List<PBRMaterialTexturePack> TexturePacks { get => _texturePacks; }

    public override void Start()
    {
        
        _propertyBlock = new MaterialPropertyBlock();

        _interactionType = Enums.InteractionType.Material;

        if (GetComponent<Renderer>() != null)
            _renderer = GetComponent<Renderer>();
        else if (GetComponentInChildren<Renderer>() != null)
            _renderer = GetComponentInChildren<Renderer>();

        //StartCoroutine(SetDefaultTexturePack());
        SetDefaultTexturePack();
        base.Start();
    }

    public void SetTexturePackByIndex(int p_materialIndex)
    {
        if (_texturePacks.Count == 0)
            return;
        if (_texturePacks.Count < p_materialIndex)
            return;
        if (_texturePacks[p_materialIndex] == null)
            return;

        _renderer.GetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        _propertyBlock.SetTexture(Constants.AlbedoTextureReference, _texturePacks[p_materialIndex].AlbedoTexture);
        _propertyBlock.SetTexture(Constants.MetallicSmoothnessReference, _texturePacks[p_materialIndex].MetallicSmoothnessTexture);
        _propertyBlock.SetTexture(Constants.NormalTextureReference, _texturePacks[p_materialIndex].NormalTexture);
        _propertyBlock.SetFloat(Constants.MetallicMultiplierReference, _texturePacks[p_materialIndex].MetallicMultiplier);
        _propertyBlock.SetFloat(Constants.SmoothnessMultiplierReference, _texturePacks[p_materialIndex].SmoothnessMultiplier);

        _renderer.SetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        _currentlySelectedTexturePack = _texturePacks[p_materialIndex];
    }

    public void SetMaterialColor(Color p_color)
    {
        _renderer.GetPropertyBlock(_propertyBlock, _changableMaterialIndex);
        _propertyBlock.SetColor(Constants.ColorReference, p_color);
        _renderer.SetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        _currentlySelectedColor = p_color;
    }

    public void SetMaterialRotation(float p_rotation)
    {
        _renderer.GetPropertyBlock(_propertyBlock, _changableMaterialIndex);
        _propertyBlock.SetFloat(Constants.RotationTextureReference, p_rotation);
        _renderer.SetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        _currentMaterialRotation = p_rotation;
    }

    public void SetMaterialScale(float p_scale)
    {
        _renderer.GetPropertyBlock(_propertyBlock, _changableMaterialIndex);
        _propertyBlock.SetFloat(Constants.ScaleTextureReference, p_scale);
        _renderer.SetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        _currentMaterialScale = p_scale;
    }

    void SetDefaultTexturePack()
    {
        if (_defaultTexturePack == null && _texturePacks.Count == 0)
        {
            _defaultTexturePack = (PBRMaterialTexturePack)ScriptableObject.CreateInstance(typeof(PBRMaterialTexturePack));
            _defaultTexturePack.AlbedoTexture = (Texture2D)_renderer.sharedMaterials[_changableMaterialIndex].GetTexture(Constants.AlbedoTextureReference);
            _defaultTexturePack.MetallicSmoothnessTexture = (Texture2D)_renderer.sharedMaterials[_changableMaterialIndex].GetTexture(Constants.MetallicSmoothnessReference);
            _defaultTexturePack.NormalTexture = (Texture2D)_renderer.sharedMaterials[_changableMaterialIndex].GetTexture(Constants.NormalTextureReference);
            _defaultTexturePack.MetallicMultiplier = _renderer.sharedMaterials[_changableMaterialIndex].GetFloat(Constants.MetallicMultiplierReference);
            _defaultTexturePack.SmoothnessMultiplier = _renderer.sharedMaterials[_changableMaterialIndex].GetFloat(Constants.SmoothnessMultiplierReference);

            if (_shouldSetDefaultMaterialPropertiesFromMaterial)
            {
                _defaultMaterialColor = _renderer.sharedMaterials[_changableMaterialIndex].GetColor(Constants.ColorReference);
                _defaultMaterialRotation = _renderer.sharedMaterials[_changableMaterialIndex].GetFloat(Constants.RotationTextureReference);
                _defaultMaterialScale = _renderer.sharedMaterials[_changableMaterialIndex].GetFloat(Constants.ScaleTextureReference);
            }
        }

        //yield return new WaitUntil(()=> ObjectPoolSystem.Instance != null);

        Material[] l_materials = new Material[_renderer.sharedMaterials.Length];
        for (int i = 0; i < l_materials.Length; i++)
        {
            l_materials[i] = _renderer.sharedMaterials[i];
        }
        l_materials[_changableMaterialIndex] = ObjectPoolSystem.Instance.GetMainMaterial();

        _renderer.sharedMaterials = l_materials;

        _renderer.GetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        if (_defaultTexturePack != null)
        {
            if (_defaultTexturePack.AlbedoTexture != null)
                _propertyBlock.SetTexture(Constants.AlbedoTextureReference, _defaultTexturePack.AlbedoTexture);
            if (_defaultTexturePack.MetallicSmoothnessTexture != null)
                _propertyBlock.SetTexture(Constants.MetallicSmoothnessReference, _defaultTexturePack.MetallicSmoothnessTexture);
            if (_defaultTexturePack.NormalTexture != null)
                _propertyBlock.SetTexture(Constants.NormalTextureReference, _defaultTexturePack.NormalTexture);
            _propertyBlock.SetFloat(Constants.MetallicMultiplierReference, _defaultTexturePack.MetallicMultiplier);
            _propertyBlock.SetFloat(Constants.SmoothnessMultiplierReference, _defaultTexturePack.SmoothnessMultiplier);
        }
        else if (_texturePacks.Count > 0)
        {
            if (_texturePacks[0].AlbedoTexture != null)
                _propertyBlock.SetTexture(Constants.AlbedoTextureReference, _texturePacks[0].AlbedoTexture);
            if (_texturePacks[0].MetallicSmoothnessTexture != null)
                _propertyBlock.SetTexture(Constants.MetallicSmoothnessReference, _texturePacks[0].MetallicSmoothnessTexture);
            if (_texturePacks[0].NormalTexture != null)
                _propertyBlock.SetTexture(Constants.NormalTextureReference, _texturePacks[0].NormalTexture);
            _propertyBlock.SetFloat(Constants.MetallicMultiplierReference, _texturePacks[0].MetallicMultiplier);
            _propertyBlock.SetFloat(Constants.SmoothnessMultiplierReference, _texturePacks[0].SmoothnessMultiplier);
        }

        _propertyBlock.SetColor(Constants.ColorReference, _defaultMaterialColor);
        _propertyBlock.SetFloat(Constants.ScaleTextureReference, _defaultMaterialScale);
        _propertyBlock.SetFloat(Constants.RotationTextureReference, _defaultMaterialRotation);


        _renderer.SetPropertyBlock(_propertyBlock, _changableMaterialIndex);

        _currentlySelectedColor = _defaultMaterialColor;
        _currentlySelectedTexturePack = _defaultTexturePack;
        _currentMaterialRotation = _defaultMaterialRotation;
        _currentMaterialScale = _defaultMaterialScale;
    }
}
