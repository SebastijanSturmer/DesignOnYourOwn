using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObjectPlacingGUIController : MonoBehaviour
{
    [SerializeField] private Enums.ObjectCategory _currentCategory;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private TMP_Dropdown _categoryDropdown;
    [SerializeField] private Transform _objectButtonsHolder;
    [SerializeField] private GameObject _objectControlButtonsHolder;
    [Header("Events")]
    [SerializeField] private ScriptableEvent _spawnObject;
    [SerializeField] private ScriptableEvent _deleteObject;

    private ObjectButtonController[] _objectButtonControllers;
    private GameObject _currentSelectedObject;

    void Start()
    {
        _objectButtonControllers = _objectButtonsHolder.GetComponentsInChildren<ObjectButtonController>();
        for (int i = 0; i < _objectButtonControllers.Length; i++)
        {
            _objectButtonControllers[i].SetIndex(i);
        }

        var l_categoryOptions = new List<TMP_Dropdown.OptionData>();
        string[] l_categories = System.Enum.GetNames(typeof(Enums.ObjectCategory));
        for (int i = 0; i < l_categories.Length; i++)
        {
            l_categoryOptions.Add(new TMP_Dropdown.OptionData(l_categories[i].ToString()));
        }

        _categoryDropdown.options = l_categoryOptions;

        _objectControlButtonsHolder.gameObject.SetActive(false);
    }

    public void OnObjectClicked(EventMessage p_message)
    {
        _spawnObject.RaiseEvent(new ObjectDataMessage(_objectButtonControllers[((IntMessage)p_message).IntValue].CurrentObjectData));
    }

    /// <summary>
    /// We are sending index instead of enums becouse we cant assing enum to button click!
    /// </summary>
    /// <param name="p_index">This should match ObjectCategory index!</param>
    public void SwitchCategory(int p_index)
    {
        _currentCategory = (Enums.ObjectCategory)p_index;
        UpdateGUI();
    }

    public void OnObjectSelected(EventMessage p_message)
    {
        _currentSelectedObject = ((GameObjectMessage)p_message).GameObject;
        _objectControlButtonsHolder.gameObject.SetActive(true);
    }
    public void OnObjectUnselected()
    {
        _currentSelectedObject = null;
        _objectControlButtonsHolder.gameObject.SetActive(false);
    }

    public void OnDeleteObjectButtonPressed()
    {
        _deleteObject.RaiseEvent(new GameObjectMessage(_currentSelectedObject));
        _currentSelectedObject = null;
        _objectControlButtonsHolder.gameObject.SetActive(false);
    }
    public void OnResetObjectTransformButtonPressed()
    {
        _currentSelectedObject.transform.rotation = Quaternion.identity;
    }

    public void ToggleGUI(bool p_shouldDisplay)
    {
        _mainPanel.SetActive(p_shouldDisplay);

        if (p_shouldDisplay)
            UpdateGUI();
    }

    private void UpdateGUI()
    {
        for (int i = 0; i < _objectButtonControllers.Length; i++)
        {
            _objectButtonControllers[i].SetCurrentObjectData(null);
        }


        if (_currentCategory == Enums.ObjectCategory.Everything)
        {
            for (int i = 0; i < DataSystem.Instance.ObjectDataList.Count; i++)
            {
                _objectButtonControllers[i].SetCurrentObjectData(DataSystem.Instance.ObjectDataList[i]);
            }
        }
        else
        {
            int l_index = 0;
            for (int i = 0; i < DataSystem.Instance.ObjectDataList.Count; i++)
            {
                if (DataSystem.Instance.ObjectDataList[i].ObjectCategory == _currentCategory || DataSystem.Instance.ObjectDataList[i].ObjectCategory == Enums.ObjectCategory.Everything)
                {
                    _objectButtonControllers[l_index].SetCurrentObjectData(DataSystem.Instance.ObjectDataList[i]);
                    l_index++;
                }
            }
        }
    }
}
