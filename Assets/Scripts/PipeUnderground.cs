using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PipeUnderground : MonoBehaviour
{
    [SerializeField] private Transform _mario;
    
    [SerializeField] private GameObject _blackScreenController;
    private void Awake()
    {

    }
    public void PipeController()
    {
        _mario.transform.DOMoveX(_mario.transform.position.x + 2.20f, 1f).OnComplete(() =>
        {
            var obj = Instantiate(_blackScreenController);
            obj.GetComponent<BlackScreenController>()._isDown = false;

        });
    }
  
}
