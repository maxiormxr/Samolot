using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEntered : MonoBehaviour {

    CheckpointsSpawn _checkpointSpawnScriptInstance;
    public GameObject _checkpointSpawnObjectInstance;
    [SerializeField]int[] _firstTrigger;
    [SerializeField]int[] _secondTrigger;
    List<int> _checkpointTriggeredOrder = new List<int>();
    int _checkpointEnteredOrder = 1;
    int _checkpointIndex = 0;


    int _firstTriggerCounter = 0;
    int _secondTriggerCounter = 0;
    GameObject _firstObject = null;
    GameObject _secondObject = null;

    bool _isCheckpointDestroyed = false;

    bool _isCheckpointBackdoor;

    float _realTimeOnStartupWhenOnTriggerExit;



    // Use this for initialization

    bool destroyCheckpoints(ref GameObject gObj1, ref GameObject gObj2)
    {
        Destroy(_checkpointSpawnScriptInstance._checkpointsList[0]);
        _checkpointSpawnScriptInstance._checkpointsList.RemoveAt(0);

        _firstObject = null;
        _secondObject = null;

        _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<ParticleSystem>()[0].name += "Actual";
        _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<ParticleSystem>()[1].name += "Actual";
        return true;
    }


    void Start () {
        _checkpointSpawnScriptInstance = _checkpointSpawnObjectInstance.GetComponent<CheckpointsSpawn>();
        foreach(var obj in _checkpointSpawnScriptInstance._checkpointsList)
        {
            Debug.Log(obj.name);
        }
        _firstTrigger = new int[_checkpointSpawnScriptInstance._checkpointsList.Count + 1];
        _secondTrigger = new int[_checkpointSpawnScriptInstance._checkpointsList.Count + 1];
        for (int i = 0; i <= _checkpointSpawnScriptInstance._checkpointsList.Count; i++)
        {
            _firstTrigger[i] = -1;
            _secondTrigger[i] = -1;
        }
           
    }
	
	// Update is called once per frame
	void Update () {

        //_firstTriggerCounter = _secondTriggerCounter = 1
        if (_isCheckpointBackdoor == true && _realTimeOnStartupWhenOnTriggerExit + 1f < Time.realtimeSinceStartup)
        {
            Debug.Log("weszło do warunku w Update po 3 sekundach od wjechania w czekpoint z tyłu");

            BoxCollider b0 = _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<BoxCollider>()[0];
            b0.enabled = true;
            BoxCollider b1 = _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<BoxCollider>()[1];
            b1.enabled = true;
            _isCheckpointBackdoor = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BoundsFrontActual")
        {
            BoxCollider b1 = _checkpointSpawnScriptInstance._checkpointsList[0].gameObject.GetComponentsInChildren<BoxCollider>()[1];
            b1.enabled = false;
            Debug.Log("drugi trigger zostal deaktywowany");
        }
        else if (other.gameObject.name == "BoundsBackActual")
        {
            BoxCollider b0 = _checkpointSpawnScriptInstance._checkpointsList[0].gameObject.GetComponentsInChildren<BoxCollider>()[0];
            b0.enabled = false;
            Debug.Log("pierwszy trigger zostal deaktywowany");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _realTimeOnStartupWhenOnTriggerExit = Time.realtimeSinceStartup;
        if (other.gameObject.name == "BoundsFrontActual")
        {
            Destroy(_checkpointSpawnScriptInstance._checkpointsList[0]);
            _checkpointSpawnScriptInstance._checkpointsList.RemoveAt(0);
            Debug.Log("usunięto czekpoint");
            if (_checkpointSpawnScriptInstance._checkpointsList.Count > 0)
            {
                ParticleSystem p0 = _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<ParticleSystem>()[0];
                ParticleSystem p1 = _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<ParticleSystem>()[1];
                p0.name += "Actual";
                p1.name += "Actual";
                p0.startColor = Color.green;
                p1.startColor = Color.yellow;

            }
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.name == "BoundsBackActual")
        {
            Debug.Log("nie usuwamy czekpointa, poniewaz zostal przekroczony od tyłu");
            BoxCollider b1 = _checkpointSpawnScriptInstance._checkpointsList[0].GetComponentsInChildren<BoxCollider>()[1];
            b1.enabled = false;
            _isCheckpointBackdoor = true;
        }
    }


    //POPRZEDNIA KONCEPCJA
    //private void OnTriggerEnter(Collider other)
    //{
    //    _firstTriggerCounter = _secondTriggerCounter = 0;
    //    if (_firstTrigger[_checkpointIndex] == 1 && _secondTrigger[_checkpointIndex] == 0)
    //    {
    //        Debug.Log("_first = 1 i _second = 0");
    //        if (_firstObject.name == "BoundsFrontActual" && _secondObject.name == "BoundsBackActual")
    //        {
    //            _isCheckpointDestroyed = destroyCheckpoints(ref _firstObject, ref _secondObject);
    //            _firstObject = null;
    //            _secondObject = null;
    //            _isCheckpointDestroyed = false;
    //            Debug.Log("usunieto checkpoint");
    //            _checkpointIndex++;
    //            other.gameObject.SetActive(false);
    //        }

    //    }
    //    else if ((_firstTrigger[_checkpointIndex] == 0 && _secondTrigger[_checkpointIndex] == 1) || (_firstTrigger[_checkpointIndex] == 0 && _secondTrigger[_checkpointIndex] == 0) || (_firstTrigger[_checkpointIndex] == 1 && _secondTrigger[_checkpointIndex] == 1))
    //    {
    //        Debug.Log("przywracanie wartosci poczatkowych triggerom!");
    //        _firstTrigger[_checkpointIndex] = -1;
    //        _secondTrigger[_checkpointIndex] = -1;
    //    }

    //    else if (_firstTrigger[_checkpointIndex] == -1 && other.gameObject.name == "BoundsFrontActual" && _firstTriggerCounter == 0)
    //    {
    //        _firstTrigger[_checkpointIndex] = _checkpointEnteredOrder % 2;
    //        _checkpointEnteredOrder++;
    //        _firstObject = other.gameObject;
    //        _firstObject.name = "BoundsFrontActual";
    //        _firstTriggerCounter = 1;
    //        Debug.Log("PIERWSZY TRIGGER: " + other.gameObject.name);
    //    }
    //    else if (_secondTrigger[_checkpointIndex] == -1 && other.gameObject.name == "BoundsBackActual" && _secondTriggerCounter == 0)
    //    {
    //        _secondTrigger[_checkpointIndex] = _checkpointEnteredOrder % 2;
    //        _checkpointEnteredOrder++;
    //        _secondObject = other.gameObject;
    //        _secondObject.name = "BoundsBackActual";
    //        _secondTriggerCounter = 1;
    //        Debug.Log("drugi trigger: " + other.gameObject.name);
    //    }
    //}

}
