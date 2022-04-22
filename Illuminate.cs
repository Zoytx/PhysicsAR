using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminate : MonoBehaviour
{

    public Material mat;
    

    private void OnTriggerExit(Collider other)
    {
       // mat.EnableKeyword("_EMISSION");
       // StartCoroutine(waitpls(1));
    }
    private void OnTriggerEnter(Collider other)
    {
        mat.EnableKeyword("_EMISSION");
       // mat.SetColor("_EmissionColor", new Color(0.0f, 0.7f, 1.0f, 1.0f) * 5);
        StartCoroutine(waitpls(1));
    }

    private IEnumerator waitpls(int time)
    {

        yield return new WaitForSeconds(time);
        mat.DisableKeyword("_EMISSION");
    }
 

}
