using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFlight : MonoBehaviour {
	[SerializeField]
	float _planeSpeed;
	float _terrainHeightInOurLocation;
	Vector3 _cameraPositionCorrection;
	const float _bias = 0.96f;



    //dodane teraz

    Vector3 _przesuniecie;
    [SerializeField]
    float _x, _y, _z;



    // Use this for initialization
    void Start () {
		Debug.Log ("plane flight script added to: " + gameObject.name);
	}

    //Update is called once per frame
    void Update () {
    	_cameraPositionCorrection = transform.position - transform.forward * 20.0f + Vector3.up * 5.0f;
    	Camera.main.transform.position = Camera.main.transform.position * _bias + _cameraPositionCorrection * (1.0f - _bias);
    	Camera.main.transform.LookAt (transform.position+transform.forward * 30.0f);

    	//transform.position += transform.forward * Time.deltaTime * _planeSpeed;
    	//_planeSpeed -= transform.forward.y * Time.deltaTime * 30.0f;

    	//if (_planeSpeed < 35.0f) _planeSpeed = 35.0f;
        //if (_planeSpeed > 70.0f) _planeSpeed = 70.0f;

        transform.Translate(_przesuniecie);
        _z = 0.7f;
        _przesuniecie = new Vector3(0f, 0f, _z);

        transform.Rotate (Input.GetAxis ("Vertical"), 0.0f, -Input.GetAxis ("Horizontal"));
        _terrainHeightInOurLocation = Terrain.activeTerrain.SampleHeight(transform.position);

        if (_terrainHeightInOurLocation > transform.position.y) {
        	transform.position = new Vector3(transform.position.x, _terrainHeightInOurLocation, transform.position.z);
        }
      }
}