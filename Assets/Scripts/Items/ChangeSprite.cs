using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _newSprite;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(GetComponent<Animator>() != null )
            _animator = GetComponent<Animator>();
    }
    public void Change()
    {
        if(_animator!=null)
        {
            _animator.enabled = false; 
        }
        if(_spriteRenderer.sprite==_newSprite)
        {
            return;
        }
        else
        { 
        _spriteRenderer.sprite = _newSprite;
        }
    }
    
    
}
