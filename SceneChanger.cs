using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
 //To change the scene from one experiment to another
 
 public void ChangeScene(string sceneName){
     Debug.Log("Scene Changed");
     SceneManager.LoadScene(sceneName);
 }

 public void Exit(){

     Application.Quit();
 }


}
