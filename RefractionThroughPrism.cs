using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(ARRaycastManager))]
public class Prism_Script : MonoBehaviour
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
    public Text DotProduct;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private float change;


    //Incresing the angle between two vectors
    public void increaseangle()
    {
        change = PlayerPrefs.GetFloat("rotation");
        if (change <= 359)
        {
            change += 1;

            spwn2.transform.rotation = Quaternion.Euler(0, change, 0);
            spwn3.transform.rotation = Quaternion.Euler(0, 90-change, 0);
            PlayerPrefs.SetFloat("rotation", change);
            if (change == 360)
            {
                PlayerPrefs.SetFloat("rotation", 0.0f);
            }
        }
    }

    //Decreasing the angle between the vectors
    public void decreaseangle()
    {


        change = PlayerPrefs.GetFloat("rotation");
        if (change >= 0)
        {
            change -= 1;

            spwn2.transform.rotation = Quaternion.Euler(0, change, 0);
            spwn3.transform.rotation = Quaternion.Euler(0, 90- change, 0);
            PlayerPrefs.SetFloat("rotation", change);
        }

    }

    //Fixing the location of the vectors placed
    public void fix()
    {
        spwn2.transform.position += new Vector3(0.03f, 0.0f, 0.0f);
        spwn3.transform.position -= new Vector3(0.03f, 0.0f, 0.0f);
        PlayerPrefs.SetInt("placed1", 1);
        PlayerPrefs.SetInt("a", 1);
        PlayerPrefs.SetInt("b", 1);
        PlayerPrefs.SetInt("angle", 0);
    }



    //Increasing the magnitude of one vector
    public void increase1()
    {

        int change = PlayerPrefs.GetInt("a");
        
        {
            change += 1;
            spwn2.transform.position += new Vector3(0.03f, 0.0f, 0.0f);

            PlayerPrefs.SetInt("a", change);
        }
    }

    //Increasing the magnitude of second vector
    public void increase2()
    {

        int change = PlayerPrefs.GetInt("b");
       
        {
            change += 1;
            
            spwn2.transform.position += new Vector3(0.0f, 0.0f, 0.005f);

            PlayerPrefs.SetInt("b", change);
        }
    }

    //Decreasing the magnitude of one vector
    public void decrease1()
    {

        int change = PlayerPrefs.GetInt("a");
        
        {
            change -= 1;

            spwn2.transform.position -= new Vector3(0.005f, 0.0f, 0.0f);

            //spwn.transform.localScale -= new Vector3(0.02f, 0.0f, 0.0f);
            // spwn.transform.position -=new Vector3(-0.02f,0f,0f);
            PlayerPrefs.SetInt("a", change);
        }
    }


    //Decreasing the magnitude of second vector
    public void decerase2()
    {

        int change = PlayerPrefs.GetInt("b");
        
        {
            change -= 1;

            spwn2.transform.position -= new Vector3(0.0f, 0.0f, 0.005f);
            // spwn2.transform.position-=new Vector3(-0.02f,0f,0f);
            PlayerPrefs.SetInt("b", change);
        }
    }


    //Setting the initial Values
    void Start()
    {
        PlayerPrefs.SetFloat("rotation", 45);
        PlayerPrefs.SetInt("placed1", 0);
    }


    //constantly updating the information to be displayed
    void FixedUpdate()
    {
        if (spwn != null)
        {
            int avalue = PlayerPrefs.GetInt("a");
            int bvalue = PlayerPrefs.GetInt("b");
            float anglevalue = PlayerPrefs.GetFloat("rotation");
            aforce.text = "Vector A: " + avalue;
            bforce.text = "Vector B: " + bvalue;
            angle.text = "Angle: " + anglevalue;
            float resultant = avalue * bvalue * (float)(Math.Cos(anglevalue));
            DotProduct.text = "Dot Product = " + resultant;

        }
    }


    //Getting the components from the manager
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

    //Placing the vectors and moving them around
    void Update()
    {
        int fix;
        fix = PlayerPrefs.GetInt("placed1");


        if (!getpos(out Vector2 pos))
            return;

        if (arcman.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {

            {
                var hitPose = hits[0].pose;

                if (fix == 0)
                {
                    if (spwn == null)
                    {
                        hitPose.rotation = Quaternion.Euler(0, 0, 0);

                        spwn = Instantiate(obj, hitPose.position, hitPose.rotation);
                       
                        Vector3 raypos= new Vector3(hitPose.position.x - 0.13f, hitPose.position.y, hitPose.position.z-0.1f);
                        Vector3 raypos2= new Vector3(hitPose.position.x + 0.13f, hitPose.position.y, hitPose.position.z-0.1f);

                        
                        spwn2 = Instantiate(obj2, raypos, hitPose.rotation);

                        spwn3 = Instantiate(obj3, raypos2, hitPose.rotation);
                    }
                    else
                    {

                        Vector3 raypos = new Vector3(hitPose.position.x + 0.13f, hitPose.position.y, hitPose.position.z-0.07f);
                        Vector3 raypos2 = new Vector3(hitPose.position.x - 0.13f, hitPose.position.y, hitPose.position.z+0.07f);
                        spwn.transform.position = hitPose.position;


                        spwn2.transform.position = raypos; 
                        spwn3.transform.position = raypos2;

                    }
                }
            }
        }
    }
}
