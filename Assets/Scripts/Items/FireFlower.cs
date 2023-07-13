using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    [SerializeField] private GameObject _textPoint;
    public void Movement()
    {


        transform.DOMoveY(transform.position.y + 0.85f, 1f).OnComplete(() =>
            {
                GetComponent<CircleCollider2D>().enabled = true;
            });
    }

    public IEnumerator DestroyObject()
    {

        GetComponent<CircleCollider2D>().enabled = false;
      
        GetComponent<SpriteRenderer>().enabled = false;

        DGPointText();
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    public void DGPointText()
    {
        _textPoint.SetActive(true);
        _textPoint.gameObject.transform.DOMoveY(transform.position.y + 0.7f, 1f).OnComplete(() =>
        {
            _textPoint.SetActive(false);
         
        });
    }
}
