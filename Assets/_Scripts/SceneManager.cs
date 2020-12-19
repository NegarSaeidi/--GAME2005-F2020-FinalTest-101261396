using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
     
    }
    public void GotoMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void GotoStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
