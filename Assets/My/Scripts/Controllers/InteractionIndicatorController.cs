using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionIndicatorController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float _startingSize = 0.2f;
    [SerializeField] private InteractableController _interactableController;
    [SerializeField] private Texture2D _objectIndicatorTexture;
    [SerializeField] private Texture2D _materialIndicatorTexture;
    [SerializeField] private Texture2D _useableIndicatorTexture;

    private ScriptableEvent _onTouchOnInteractableController;
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;
    private ScriptableEvent _subscribeToPool;
    private Transform _cameraTransform;

    public InteractableController InteractableController { get => _interactableController; }

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        transform.LookAt(_cameraTransform);
    }

    private void Setup()
    {
        _cameraTransform = Camera.main.transform;

        if (_interactableController == null)
        {
            _interactableController = GetComponentInParent<InteractableController>();
        }
        if (_interactableController == null)
        {
            Debug.LogError("Interaction Indicator Controller : I dont have interactable controller! I couldn't find it in parent! Assign it in inspector or make sure to have it in parent!");
        }

        if (_interactableController.InteractionType == Enums.InteractionType.Transform)
            _startingSize *= 0.5f;


        transform.localScale = Vector3.one * _startingSize;

        _onTouchOnInteractableController = ObjectPoolSystem.Instance.GetScriptableEventByTag(Enums.ScriptableEventTag.OnTouchOnInteractableController);

        _renderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
        ;
        _renderer.GetPropertyBlock(_propertyBlock);
        switch (_interactableController.InteractionType)
        {
            case Enums.InteractionType.Replace:
                _propertyBlock.SetTexture("_MainTex",_objectIndicatorTexture);
                break;
            case Enums.InteractionType.Material:
                _propertyBlock.SetTexture("_MainTex", _materialIndicatorTexture);
                break;
            default:
                _propertyBlock.SetTexture("_MainTex", _useableIndicatorTexture);
                break;
        }
        _renderer.SetPropertyBlock(_propertyBlock);


        _subscribeToPool = ObjectPoolSystem.Instance.GetScriptableEventByTag(Enums.ScriptableEventTag.SubscribeIndicatorControllerToObjectPool);

        _subscribeToPool.RaiseEvent(new InteractionIndicatorControllerMessage(this));

        ToggleIndicatorDependingOnCurrentApplicationMode();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_startingSize*2,.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_startingSize, .5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onTouchOnInteractableController.RaiseEvent(new InteractableControllerMessage(_interactableController));
    }

    public void ToggleIndicatorDependingOnCurrentApplicationMode()
    {
        if (gameObject == null)
            return;

        if (_interactableController.ApplicationModeInteractable == ApplicationManager.Instance.CurrentApplicationMode)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
