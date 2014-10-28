using UnityEngine;
using System.Collections;

public class SequenceMovement : MonoBehaviour {

	private static GameObject TileSequence;
	private GameObject[] tiles;
	private Vector3 origin;
	private Vector3 destiny;

	public static GameObject Factory (GameObject[] t) {
		if (TileSequence == null) {
			TileSequence = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/TileSequence.prefab", typeof(GameObject));
		}
		GameObject prefab = (GameObject)Instantiate(TileSequence);
		SequenceMovement sequence = prefab.GetComponent<SequenceMovement>();
		sequence.tiles = t;
		return prefab;
	}

	public SequenceMovement From (Vector3 from) {
		origin = from;
		return this;
	}


	// List of tiles
	// Origin position
	// Destiny
	// Parent?
	// Callback fn

}