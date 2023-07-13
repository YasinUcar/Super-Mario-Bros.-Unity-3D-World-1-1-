using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class EnemyDie : MonoBehaviour
{
    [SerializeField] private AudioClip _stompSFX;
    [SerializeField] private Sprite _dieSprite;
    [SerializeField] private float _destroyTime = 1f;
    
    //TODO : textPoint dgpoint vb. yapýlar fireflower mushroom ve burada kullanlýyor bunlarý tek bir yapý haline getirilip kod çaðýralabilir
    [SerializeField] private GameObject _textPoint;
   


    public void Die()
    {
        GetComponent<Animator>().enabled= false;
        AudioManager.Instance.PlaySound(_stompSFX);
      
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        this.GetComponent<SpriteRenderer>().sprite = _dieSprite;
      
    
        if (GetComponent<FollowPath>() != null)
            GetComponent<FollowPath>().enabled = false;
        if(GetComponent<EnemyPath>() != null)
            GetComponent<EnemyPath>().enabled = false;
        StartCoroutine(DestroyObject());

    }
    public IEnumerator DestroyObject()
    {


        DGPointText();
        yield return new WaitForSeconds(1f);
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
