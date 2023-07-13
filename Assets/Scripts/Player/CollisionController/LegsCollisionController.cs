using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsCollisionController : MonoBehaviour
{
    //TODO : neden inparent ile orangeEnemy'ye ulaþamýyoruz bu tarz almak zorunda kalýyoruz?
   
    private UIManager _uIManager;
    private void Awake()
    {
        _uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyUpper"))
        {
            //collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.transform.parent.tag = "EnemyEmpty";
            _uIManager.AddPoint(100);
           
            if (collision.GetComponentInParent<EnemyDie>() != null)
                collision.GetComponentInParent<EnemyDie>().Die();
            else
                Debug.LogError("EnemyDie null!");
            if (collision.GetComponentInParent<EnemyPath>() != null)
                collision.GetComponentInParent<EnemyPath>().enabled = false;
        }
        else if(collision.CompareTag("TurtleEnemyUpper"))
        {
            collision.transform.parent.GetComponent<TurtleEnemyController>().Controller();
        }
        else if(collision.CompareTag("TurtleEnemy"))
        {
            _uIManager.AddPoint(100);
            if (collision.GetComponent<TurtleEnemyController>() != null)
                collision.GetComponent<TurtleEnemyController>().Controller();
            else
                Debug.LogError("TurtleEnemyController.cs null!");
        }
    }

}
