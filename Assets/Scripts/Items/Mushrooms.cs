using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using DG.Tweening;
public class Mushrooms : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _textPoint;
    private bool _reverseForce, _force;
    private Rigidbody2D _rigidbody2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            _reverseForce = true;
            _force = false;
            print("Çarptim");
        }
    }
    private void Update()
    {
        ReverseForce();
        Force();
    }

    public void Movement()
    {

        transform.DOMoveY(transform.position.y + 1f, 1f).OnComplete(() =>
        {

            transform.DOMoveX(transform.position.x + 4f, 2f);
            _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<CircleCollider2D>().enabled = true;
            _rigidbody2D.gravityScale = 30f;
            _force = true;
        });
    }
    private void Force()
    {
        if (!_force)
        {
            return;
        }
        else if (_force)
        {
            _rigidbody2D.velocity = Vector3.right * _speed * Time.deltaTime;
        }
    }
    private void ReverseForce()
    {
        if (!_reverseForce)
        {
            return;
        }
        else if (_reverseForce)
        {
            _rigidbody2D.velocity = Vector3.left * _speed * Time.deltaTime;
        }

    }
    
    public IEnumerator DestroyObject()
    {
        _speed = 0f;
        GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.CompareTag("Empty");
        GetComponent<SpriteRenderer>().enabled = false;
        DGPointText();
        yield return new WaitForSeconds(5f);
        
    }
    public void DGPointText()
    {
        _textPoint.SetActive(true);
        _textPoint.gameObject.transform.DOMoveY(transform.position.y + 0.7f, 1f).OnComplete(() =>
        {
            _textPoint.SetActive(false);
            Destroy(this.gameObject);
        });
    }
}
