using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class thirdlaw : MonoBehaviour
{
    public GameObject obj;
   
    private GameObject spwn;
    private ARRaycastManager arcman;
    private Vector2 pos;
   
    
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();






//Getting the AR component from plugins
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


//Placing the object and moving it around
void Update()
    {
        if(!getpos(out Vector2 pos))
            return;
       
        if (arcman.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
   {  
       
        
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
