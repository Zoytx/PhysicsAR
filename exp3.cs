using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(ARRaycastManager))]
public class exp3 : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    private GameObject spwn3;
    private GameObject spwn2;
    private GameObject spwn;
    private ARRaycastManager arcman;
    private Vector2 pos;
    public Text aforce;
    public Text bforce;
    public Text angle;
    public Text CrossProduct;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();




private float change;


//Increasing the angle between vectors
public void increaseangle(){
    
    
        change=PlayerPrefs.GetFloat("rotation");
        if(change<=359){
        change+=1;

         spwn2.transform.rotation = Quaternion.Euler(0, change, 0);
         PlayerPrefs.SetFloat("rotation",change);
         if(change==360){
             PlayerPrefs.SetFloat("rotation",0.0f);
         }

        }


}
//decreasing the angle between vectors
public void decreaseangle(){
    
    
        change=PlayerPrefs.GetFloat("rotation");
        if(change>=0){
        change-=1;

         spwn2.transform.rotation = Quaternion.Euler(0, change, 0);
         PlayerPrefs.SetFloat("rotation",change);
        }

}

//Fixing the vectors
public void fix(){
    PlayerPrefs.SetInt("Exp3placed",1);
    PlayerPrefs.SetInt("a",1);
    PlayerPrefs.SetInt("b",1);
    PlayerPrefs.SetInt("angle",0);

}



//Increasing 1's magnitude
public void increase1(){

    int change=PlayerPrefs.GetInt("a");
     if(change<=9 || change==1){
    change+=1;
   spwn.transform.localScale += new Vector3(0.01f, 0.0f, 0.0f);
   
   PlayerPrefs.SetInt("a",change);}


}


//Increasing 2's magnitude
public void increase2(){

    int change=PlayerPrefs.GetInt("b");
     if(change<=9 || change==1){
    change+=1;

    
   spwn2.transform.localScale += new Vector3(0.01f, 0.0f, 0.0f);
    
    PlayerPrefs.SetInt("b",change);
     }

}



//decreasing 1's magnitude
public void decrease1(){

    int change=PlayerPrefs.GetInt("a");
     if(change>=2 || change==10){
    change-=1;

    
   spwn.transform.localScale -= new Vector3(0.01f, 0.0f, 0.0f);
  // spwn.transform.position -=new Vector3(-0.02f,0f,0f);
    PlayerPrefs.SetInt("a",change);}


}



//decreasing 2's magnitude
public void decerase2(){

    int change=PlayerPrefs.GetInt("b");
     if(change>=2 || change==10){
    change-=1;

    
   spwn2.transform.localScale -= new Vector3(0.01f, 0.0f, 0.0f);
  // spwn2.transform.position-=new Vector3(-0.02f,0f,0f);
    PlayerPrefs.SetInt("b",change);
     }





}


//Setting initial values
void Start(){
     PlayerPrefs.SetFloat("rotation",0);
     PlayerPrefs.SetInt("Exp3placed",0);

}

//updating the values to be displayed
void FixedUpdate(){
if(spwn!=null)
{
int avalue=PlayerPrefs.GetInt("a");
int bvalue=PlayerPrefs.GetInt("b");
float anglevalue=PlayerPrefs.GetFloat("rotation");
aforce.text="Vector A: "+avalue;
bforce.text="Vector B: "+bvalue;

angle.text="Angle: "+anglevalue;
 float resultant=avalue*bvalue*(float)(Math.Sin(anglevalue));
 spwn3.transform.localScale = new Vector3(Math.Abs(resultant)*0.05f, 0.4f, 0.08f);


CrossProduct.text="Vector Product = "+ resultant ;


}





}


//getting componenets from manager
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

//placing and moving vectors around
 void Update()
    {
        int fix;
        fix=PlayerPrefs.GetInt("Exp3placed");


        if(!getpos(out Vector2 pos))
            return;
       
        if (arcman.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
   {  
       
       {
           
            var hitPose = hits[0].pose;

        

         if(fix==0) {
                if (spwn == null)

            {
                hitPose.rotation = Quaternion.Euler(0, 0, 0);
                spwn = Instantiate(obj, hitPose.position, hitPose.rotation);
                

                spwn2 = Instantiate(obj2, hitPose.position, hitPose.rotation);
                
                hitPose.rotation = Quaternion.Euler(0, 0, -90);
                

                spwn3 = Instantiate(obj3, hitPose.position, hitPose.rotation);
                
                spwn3.transform.localScale = new Vector3(0f, 0.0f, 0.0f);
                
                

            }
            else
            {
                spwn.transform.position = hitPose.position;
                spwn2.transform.position = hitPose.position;
                spwn3.transform.position = hitPose.position;
                
            }}
        }
        }
        

    }
    





}
