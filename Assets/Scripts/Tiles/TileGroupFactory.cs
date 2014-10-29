using UnityEngine;
using System.Collections;

public class TileGroupFactory {

	private static GameObject TileSequence;

	public static TileGroupMovement Create(Grid grid, GameObject[] tiles,
	                           Vector3 origin, Vector3 destiny) {
		if (TileSequence == null) {
			TileSequence = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/TileGroup.prefab", 
			                                                      typeof(GameObject));
		}

		GameObject prefab = (GameObject) MonoBehaviour.Instantiate(TileSequence);
		TileGroupMovement group = prefab.GetComponent<TileGroupMovement>();
		prefab.transform.parent = grid.transform;
		group.tiles = tiles;
		group.origin = origin;
		group.destiny = destiny;
		return group;
	}

}