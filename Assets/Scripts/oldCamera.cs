using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldCamera : MonoBehaviour
{
    //ref : https://www.youtube.com/watch?v=GTxiCzvYNOc
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private Vector2 _followOffset;
    [SerializeField] private float _speed = 3f;
    private Vector2 _threshold;
    private Rigidbody2D _rb;

    private void Start()
    {
        _threshold = CalculateThreshold();
        _rb = _targetObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 follow = _targetObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.zero, Vector2.right * follow.x);
        //float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= _threshold.x)
        {
            newPosition.x = follow.x;
        }
        //if (Mathf.Abs(yDifference) >= _threshold.y)
        //{
        //    newPosition.y = follow.y;
        //}
        float moveSpeed = _rb.velocity.magnitude > _speed ? _rb.velocity.magnitude : _speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition,  Time.deltaTime);
    }
    private Vector2 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= _followOffset.x;
        t.y -= _followOffset.y;
        return t;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y, 1));
    }
}

