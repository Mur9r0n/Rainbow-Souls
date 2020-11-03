using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UIObjects : MonoBehaviour
{
    public GameObject m_InventoryPanel;

    public void Show(GameObject _uiGameObject,bool _isShown)
    {
        _uiGameObject.SetActive(_isShown);
    }
}
