using UnityEngine;
using System.Collections;

public class PlayerTiles : MonoBehaviour {

	// Max number of tiles a player can hold at the same time
	public int maxTilesHeld = 10;
	
	private Grid grid;
	private GameObject[] tiles;
	private int held = 0;

	private void Awake () {
		grid = transform.parent.GetComponent<Grid>();
		tiles = new GameObject[maxTilesHeld];
	}

	public bool IsEmpty () {
		return held == 0;
	}

	public string Type () {
		return tiles[0].name;
	}

	public void Add (GameObject[] newTiles) {
		int i;
		int length = newTiles.Length;
		for (i = 0; i < length; i++) {
			if (held + i > maxTilesHeld) break;
			GameObject tile = newTiles[i];
			if (tile == null) break;
			tiles[held + i] = tile;
		}
		held += i;
		if (i == 0) return; // no tiles added
		Vector3 origin = newTiles[i - 1].transform.localPosition;
		Vector3 destiny = transform.position;
		new TileGroupFactory(grid, newTiles, origin, destiny);
	}

	public GameObject[] Clear () {
		int length = tiles.Length;
		GameObject[] copy = new GameObject[length];
		for (int i = 0; i < length; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			copy[i] = tile;
			tiles[i] = null;	
		}
		held = 0;
		return copy;
	}

}
