using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class tap : MonoBehaviour
{
    public GameObject obj;
    private GameObject spwn;
    private ARRaycastManager arcman;
    private Vector2 pos;
    private static float gravitymul=2;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public Text massdisp;


//Setting the mass of the object
 void setmass(float value){
        Debug.Log("setting value"+value);
        massdisp.text ="Mass: "+(value);
       // massdisp.text ="Mass: = " + value;
               }



//Increasing the scale of the object
public void incrasesize( bool change){
     Debug.Log(gravitymul);
     gravitymul=PlayerPrefs.GetFloat("gravity");
    if (gravitymul<10)
{
   spwn.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
  gravitymul+=2;
  PlayerPrefs.SetFloat("gravity",gravitymul);
  setmass(gravitymul);

    }

}




//Decreasing the size of the object
public void decreasesize( bool change){
    Debug.Log(gravitymul);
    gravitymul=PlayerPrefs.GetFloat("gravity");
    if(gravitymul>=4){
   spwn.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
   gravitymul-=2;
   PlayerPrefs.SetFloat("gravity",gravitymul);
  setmass(gravitymul);  


    }


}





//Reseting the variable that blocks the user from dropping the ball again
public void Reset(bool reset){

    Debug.Log("Position Reset");
    PlayerPrefs.SetInt("placed",0);

    return;


}

//Saving initial values
void Start(){
         PlayerPrefs.SetInt("placed",0);
         PlayerPrefs.SetFloat("gravity",0);
         massdisp.text ="NOT INITIALIZED";
    }




//Retriving the AR components
private void Awake()
    {
        arcman = GetComponent<ARRaycastManager>();
        
    }
    bool getpos(out Vector2 pos)
    {
        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
            return true;
        }

        pos = default;
        return false;
    }


//Updating to place and to move around
void Update()
    {
        if(!getpos(out Vector2 pos))
            return;
       
        if (arcman.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
   if( PlayerPrefs.GetInt("placed") ==0)  {  
        PlayerPrefs.SetInt("placed",1);
       {
            var hitPose = hits[0].pose;
            hitPose.position.y = hitPose.position.y + 2;

            if (spwn == null)
            {
                spwn = Instantiate(obj, hitPose.position, hitPose.rotation);

            }
            else
            {
                spwn.transform.position = hitPose.position;
            }
        }
        }
        

    }
    //GameObject.find(obj).transform.position;
  



}
