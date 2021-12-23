using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;

public class SphereController : MonoBehaviour
{
    
    public void Getvalue(string s){
        Debug.Log(s);
        PlayerPrefs.SetInt("forcevalue",int.Parse(s));


    }


    public Rigidbody rb;
    public Text velocitydisp;
    public int MoveSpeed = 10;
    private float speed;
    private GameObject spwn;
    private float mass=2;
    
//Increasing the mass of the ball
public void increasemass(){
        
       mass= PlayerPrefs.GetFloat("mass");
       if (mass<10){
        mass+=2;
        PlayerPrefs.SetFloat("mass",mass);}

    }
    //decreasing the mass of the ball
public void decreasemass(){
        mass= PlayerPrefs.GetFloat("mass");

        if (mass>3){
        mass-=2;
        PlayerPrefs.SetFloat("mass",mass);
}
    }
//setting initial values
    void Start(){
        PlayerPrefs.SetFloat("mass",2);
      
        MoveSpeed=0;
        velocitydisp.text ="NOT INITIALIZED";


    }
//Getting components
void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

//Updating the values 
void Update()

 
    {

        MoveSpeed=PlayerPrefs.GetInt("forcevalue");
        mass= PlayerPrefs.GetFloat("mass");

        if (CrossPlatformInputManager.GetButtonDown("Push")) {
            if(MoveSpeed!=0){
            rb.AddForce(new Vector3(0f, 0f, MoveSpeed-(0.78f*mass)));
}
        }
            
  
        // velocitydisp.GetComponent<Text>().text ="Velocity= "+ PlayerPrefs.GetFloat("velocity");
       
        
 
  
    }



//Setting the initial push of the object
  public void setvelocity(Rigidbody ball){

        
        if(ball!=null)
        if(ball.velocity.magnitude!=0){
        mass= PlayerPrefs.GetFloat("mass");
        MoveSpeed=PlayerPrefs.GetInt("forcevalue");
        velocitydisp.text ="Accelaration: " + (MoveSpeed/mass);
       
        }
        else
        velocitydisp.GetComponent<Text>().text ="################";


    }





}
