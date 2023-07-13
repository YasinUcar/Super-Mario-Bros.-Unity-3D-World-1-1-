using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarioRaycast : MonoBehaviour
{
    [SerializeField] private float _distance = 10f;
    private bool _isRay;
    void FixedUpdate()
    {
        Vector2 _position = transform.position;
        float _height = GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 _direction = Vector2.up;


        RaycastHit2D hit = Physics2D.Raycast(_position + Vector2.up * _height * 0.5f, _direction, _distance);
        if (hit.collider != null && hit.collider.name != this.gameObject.name)
        {
            _isRay = true;
            Debug.DrawLine(_position + Vector2.up * _height * 0.5f, hit.point, Color.green);            
            //Debug.Log("Vurulan obje: " + hit.collider.gameObject.name);
        }
        else if(hit.collider==null)
        {
            Debug.DrawLine(_position + Vector2.up * _height * 0.5f, hit.point, Color.red);
            _isRay = false;
        }
       
    }
    public bool CurrentRayStatus()
    {
        return _isRay;
    }



}
