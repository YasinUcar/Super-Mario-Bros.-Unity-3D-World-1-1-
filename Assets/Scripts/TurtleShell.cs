using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : MonoBehaviour
{
    private TurtleEnemyController _teurtleEnemyController;
    private UIManager _uIManager;
    private void Awake()
    {
        _teurtleEnemyController = GetComponentInParent<TurtleEnemyController>();
        _uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            _teurtleEnemyController.Movement(-1);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.transform.parent.tag = "EnemyEmpty";
            _uIManager.AddPoint(100);

            if (collision.gameObject.GetComponentInParent<EnemyDie>() != null)
                collision.gameObject.GetComponentInParent<EnemyDie>().Die();
            else
                Debug.LogError("EnemyDie null!");
            if (collision.gameObject.GetComponentInParent<EnemyPath>() != null)
                collision.gameObject.GetComponentInParent<EnemyPath>().enabled = false;
        }
    }
}
