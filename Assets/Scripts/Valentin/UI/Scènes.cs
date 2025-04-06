using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scènes : MonoBehaviour
{
    public string scene1;
    public string scene2;
    public string scene3;
    public string scene4;
    public string scene5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SceneManager.LoadScene(scene1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SceneManager.LoadScene(scene2);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SceneManager.LoadScene(scene3);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SceneManager.LoadScene(scene4);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    SceneManager.LoadScene(scene5);
        //}
    }
    public void Montée()
    {
        SceneLoader.LoadScene(scene1);
    }
    public void Descente()
    {
        SceneLoader.LoadScene(scene2);
    }
    public void Liste()
    {
        SceneLoader.LoadScene(scene3);
    }
    public void Block()
    {
        SceneLoader.LoadScene(scene4);
    }
    public void Dalle()
    {
        SceneLoader.LoadScene(scene5);
    }
}
