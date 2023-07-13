using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    [SerializeField] int _sceneNumber;
    void Update()
    {
        if (Input.GetAxis("Submit") > 0)
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }
}
