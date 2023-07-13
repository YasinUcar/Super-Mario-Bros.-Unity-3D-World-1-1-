using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTag : MonoBehaviour
{
    [SerializeField] private string _newTagName;
    private ChangeSFX _changeSFX;
    private string _firstTagName;
    private void Awake()
    {
        _changeSFX = GetComponent<ChangeSFX>();
        _firstTagName = this.gameObject.tag.ToString();
    }
    public void Change()
    {
        _changeSFX.PlaySFX(_firstTagName, this.gameObject.tag.ToString());
        if (this.gameObject.CompareTag(_newTagName))
        {
            return;
        }
        else
        {
            this.gameObject.tag = _newTagName;
        }
    }

}
