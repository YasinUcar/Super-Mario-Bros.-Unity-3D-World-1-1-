using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartingCollider : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemysList = new List<GameObject>();
    [SerializeField] private string _checkPointName;
    private CheckPointManager _checkPointManager;
    private void Awake()
    {   
        _checkPointManager = GameObject.FindGameObjectWithTag("CheckPointManager").GetComponent<CheckPointManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _checkPointManager.enabled = true;
        if (collision.CompareTag("Player"))
        {
            foreach (var i in _enemysList)
                i.gameObject.SetActive(true);
            _checkPointManager.CheckPointLocation(_checkPointName);
        }


    }

}
