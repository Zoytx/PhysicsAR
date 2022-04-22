using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class MagnetThrough : MonoBehaviour
{
    public Material mat;
    public GameObject obj;
    private GameObject spwn;   
    public GameObject obj2;
    private GameObject spwn2;
    private ARRaycastManager arcman;
    private Vector2 pos;
    float speed = 1.0f;
    public float move;
    public Text Flux;
    public Text magsize;
    public Text Faraday;
    public Text Intensity;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public void increasearea()
    {
        float intensity =PlayerPrefs.GetFloat("intensity");
        PlayerPrefs.SetInt("coilsize", PlayerPrefs.GetInt("coilsize") + 1);
        intensity += 0.5f ;
        PlayerPrefs.SetFloat("intensity", intensity);

        spwn.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
        Color clr = mat.GetColor("_Color");

        mat.SetColor("_EmissionColor", clr * intensity);


    }
  
    public void increasemagnet()
    {
        if (PlayerPrefs.GetInt("magsize") < PlayerPrefs.GetInt("coilsize"))
        {

            float intensity = PlayerPrefs.GetFloat("intensity");
            PlayerPrefs.SetInt("coilsize", PlayerPrefs.GetInt("coilsize") + 1);
            intensity += 0.5f;
            PlayerPrefs.SetFloat("intensity", intensity);

            spwn2.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
            PlayerPrefs.SetInt("magsize", PlayerPrefs.GetInt("magsize") + 1);

            Color clr = mat.GetColor("_Color");

            mat.SetColor("_EmissionColor", clr * intensity);
        }




    }
    private void Start()
    {
        PlayerPrefs.SetInt("placed1", 0);
        PlayerPrefs.SetFloat("rotation", 0);
    }

    
    private void Awake()
    {
        arcman = GetComponent<ARRaycastManager>();


    }

    public void fix()
    {
        spwn.transform.rotation = Quaternion.Euler(0, 90, 0);
        spwn2.transform.rotation = Quaternion.Euler(0, 90, 0);
        PlayerPrefs.SetInt("placed1", 1);
        PlayerPrefs.SetFloat("intensity", 1);
        PlayerPrefs.SetInt("a", 1);
        PlayerPrefs.SetInt("b", 1);
       // Flux.text = "Flux A: " + PlayerPrefs.GetFloat("magsize");
        mat.DisableKeyword("_EMISSION");
        PlayerPrefs.SetInt("angle", 0);
        PlayerPrefs.SetInt("coilsize", 1);
        PlayerPrefs.SetInt("magsize", 1);

    }

    public void Move(float moveloc)
    {


        
        float xloc=spwn.transform.position.x;
        spwn2.transform.position = new Vector3(xloc+0.01f*moveloc, spwn.transform.position.y, spwn.transform.position.z);
        

    }


    public void rotate()
    {
        float change = PlayerPrefs.GetFloat("rotation");
        change += 1;
        spwn.transform.rotation = Quaternion.Euler(0, change, 0);
        spwn2.transform.rotation = Quaternion.Euler(0, change, 0);

        PlayerPrefs.SetFloat("rotation", change);

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
    private void FixedUpdate()
    {

        int flux = PlayerPrefs.GetInt("coilsize");
        float intensity= PlayerPrefs.GetFloat("intensity");
        float Solution;
        Solution = -1 * (7*(flux));

        Flux.text = "Flux A: " + flux ;
        Intensity.text = "Intensity: " + intensity;
        Faraday.text = "Faradays Law: "+Solution;
    }


    void Update()
    {
        

        int fix;
        fix = PlayerPrefs.GetInt("placed1");

        if (!getpos(out Vector2 pos))
            return;

        if (arcman.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {

            if(fix==0){
                var hitPose = hits[0].pose;


                if (spwn == null)
                {
                  hitPose.position.y += 0.2f;
                    spwn = Instantiate(obj, hitPose.position, hitPose.rotation);
                    hitPose.position.x = -0.2f;
                    spwn2 = Instantiate(obj2, hitPose.position, hitPose.rotation);


                }
                else
                {
                    hitPose.position.y += 0.2f;
                    spwn.transform.position = hitPose.position;
                    hitPose.position.x = -0.2f;
                    spwn2.transform.position = hitPose.position;
                }
            }
        }


    }
    //GameObject.find(obj).transform.position;






}
