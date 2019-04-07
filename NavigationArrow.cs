using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrow : MonoBehaviour {
    CheckpointsSpawn _checkpointSpawnScriptInstance;
    public GameObject _checkpointSpawnObjectInstance;
    private GameObject _target;
    private Vector3 _targetPoint;
    private Quaternion _targetRotation;


    // Use this for initialization
    void Start () {
        _checkpointSpawnScriptInstance = _checkpointSpawnObjectInstance.GetComponent<CheckpointsSpawn>();
    }

    // Update is called once per frame
    void Update () {
        if (_checkpointSpawnScriptInstance._checkpointsList.Count > 0)
        {
            _target = _checkpointSpawnScriptInstance._checkpointsList[0];
            _targetPoint = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z) - gameObject.GetComponentInParent<Camera>().transform.position;
            _targetRotation = Quaternion.LookRotation(_targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * 2.0f);
        }
    }
}