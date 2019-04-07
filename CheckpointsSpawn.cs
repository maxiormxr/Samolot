using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsSpawn : MonoBehaviour {
	[SerializeField]
	GameObject _checkpointRingPrefab;
	GameObject _checkpointRingPrefabClone;
	int _numberOfCheckpoints;
	int[] _checkpointsZones;
	const int _mapHeightAndWidth = 2000;
	Vector3 _checkpointPosition;
	[SerializeField]public List<GameObject> _checkpointsList = new List<GameObject>();
    [SerializeField]
    List<ParticleSystem> _psInCheckpoints = new List<ParticleSystem>();
	float _terrainHeightInCheckpointLocation;
    Color _red = new Color(255f, 0f, 0f);

    // Use this for initialization
    void Start () {
		//generowanie spawnspotów checkpointów
		_numberOfCheckpoints = Random.Range (5, 15);
		_checkpointsZones = new int[_numberOfCheckpoints];
		//Debug.Log ("Liczba checkpointów = " + _numberOfCheckpoints);

		for (int i = 0; i < _numberOfCheckpoints; i++) {
			_checkpointsZones [i] = _mapHeightAndWidth / _numberOfCheckpoints * i;

			//Debug.Log (i + " strefa spawnienia checkpointów = " + _checkpointsZones [i]);

			if (i > 0) {
                //_checkpointPosition.Set(100f, 100f, Random.Range(_checkpointsZones[i - 1], _checkpointsZones[i]));

                _checkpointPosition.Set (Random.Range(200f, 1800f), 100f, Random.Range (_checkpointsZones [i - 1], _checkpointsZones [i]));
                _checkpointRingPrefabClone = Instantiate (_checkpointRingPrefab, _checkpointPosition, Quaternion.identity) as GameObject;

				_terrainHeightInCheckpointLocation = Terrain.activeTerrain.SampleHeight(_checkpointRingPrefabClone.transform.position);
				//Debug.Log ("wysokosc terenu w miejscu czekpointa = " + _terrainHeightInCheckpointLocation);

				if (_terrainHeightInCheckpointLocation > _checkpointRingPrefabClone.transform.position.y - 20f) {
					_checkpointRingPrefabClone.transform.position = new Vector3(_checkpointRingPrefabClone.transform.position.x, _terrainHeightInCheckpointLocation + 50f, _checkpointRingPrefabClone.transform.position.z);
				}
				//Debug.Log ("Utworzono " + i + " checkpoint");


				//_checkpointRingPrefabClone.GetComponentsInChildren<ParticleSystem> ()[1].startColor = _black;
				_checkpointsList.Add (_checkpointRingPrefabClone);

                _psInCheckpoints.Add(_checkpointRingPrefabClone.GetComponentsInChildren<ParticleSystem>()[0]);
                _psInCheckpoints.Add(_checkpointRingPrefabClone.GetComponentsInChildren<ParticleSystem>()[1]);
                //_checkpointsList[i - 1].GetComponentsInChildren<ParticleSystem>()[0].gameObject.tag = "front";
                //_checkpointsList[i - 1].GetComponentsInChildren<ParticleSystem>()[1].gameObject.tag = "back";


                Debug.Log(_checkpointsList[i-1].GetComponentsInChildren<ParticleSystem>()[0].name);


                if(i == 1)
                {
                    _checkpointsList[i - 1].GetComponentsInChildren<ParticleSystem>()[0].name += "Actual";
                    _checkpointsList[i - 1].GetComponentsInChildren<ParticleSystem>()[1].name += "Actual";
                }
                else
                {
                    _checkpointsList[i - 1].GetComponentsInChildren<ParticleSystem>()[0].startColor = _red;
                    _checkpointsList[i - 1].GetComponentsInChildren<ParticleSystem>()[1].startColor = _red;
                }
            }
		}
	}

	// Update is called once per frame
	void Update () {


	}



}
