using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    [SerializeField] string Target_scene;
    public void To_Scene()
    {
        //toSceme
        SceneManager.LoadScene(Target_scene, LoadSceneMode.Single);
    }
}
