using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColor : MonoBehaviour
{
    
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine("MaterialColorFade");
    }

    IEnumerator MaterialColorFade()
    {
        for (float i = 0; i < 1.1; i += 0.1f)
        {
            this.GetComponent<MeshRenderer>().material.SetFloat("_CumstomValue", i);
            yield return new WaitForSeconds(0.05f);
        }

        transform.parent.gameObject.SetActive(false);
    }
}
