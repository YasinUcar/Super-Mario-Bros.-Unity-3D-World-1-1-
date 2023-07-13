using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PipeDown : MonoBehaviour
{

    [SerializeField] private Transform _mario;
    [SerializeField] private GameObject _blackScreenController;
    //private BlackScreenController _blackScreenController;

    public void PipeController()
    {

        _mario.transform.DOMoveY(_mario.transform.position.y - 5.55f, 1f).OnComplete(() =>
        {
         
            var obj = Instantiate(_blackScreenController);
            obj.GetComponent<BlackScreenController>()._isDown = true;

        });

    }
}
