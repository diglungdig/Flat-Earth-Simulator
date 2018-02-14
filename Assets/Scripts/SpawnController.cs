using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject newone = ObjectPooler.Instance.SpawnPoolingObject("Dino1", this.transform.position, RandomRotation());

            newone.GetComponent<Renderer>().material.color = Random.ColorHSV(); 
        }	
	}


    Quaternion RandomRotation()
    {
        return Quaternion.Euler(Random.insideUnitSphere * 360f);
    }
}
