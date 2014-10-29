using UnityEngine;
using System.Collections;

public class TileGroupFactory {

	private static GameObject TileSequence;

	public TileGroupFactory(Grid grid, GameObject[] tiles,
	                           Vector3 origin, Vector3 destiny) {
		if (TileSequence == null) {
			TileSequence = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/TileGroup.prefab", 
			                                                      typeof(GameObject));
		}

		GameObject prefab = (GameObject) MonoBehaviour.Instantiate(TileSequence);
		TileGroupMovement sequence = prefab.GetComponent<TileGroupMovement>();

		prefab.transform.parent = grid.transform;
		sequence.tiles = tiles;
		sequence.origin = origin;
		sequence.destiny = destiny;
	}

}