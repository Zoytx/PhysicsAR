using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class Motor : MonoBehaviour
{
    public GameObject obj;
    public Animator animator;
    private GameObject spwn;
    private ARRaycastManager arcman;
    private Vector2 pos;
    float speed = 1.0f;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    public void ChangeSpeed()
    {
        speed += 1.0f;
        animator.SetFloat("CurrentSpeed", speed);
        spwn.gameObject.GetComponent<Animator>().SetFloat("CurrentSpeed", speed);
    }
    private void Start()
    {
    }
    private void Awake()
    {
        arcman = GetComponent<ARRaycastManager>();
       

    }
    
  

    public void pauseanimation()
    {
        spwn.gameObject.GetComponent<Animator>().enabled = false;
        

    }
    public void playanimation()
    {


        spwn.gameObject.GetComponent<Animator>().enabled = true;
        

        //anim.Play("FanMoving");

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


    //Getting the values to place the objects and move them around
    void Update()
    {
      
        if (!getpos(out Vector2 pos))
            return;

        if (arcman.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {

            {
                var hitPose = hits[0].pose;
                

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
