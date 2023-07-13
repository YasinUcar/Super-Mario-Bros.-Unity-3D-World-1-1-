
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerController : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);        
    }
}
