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
		for (i = 0; i < newTiles.Length; i++) {
			if (held + i > maxTilesHeld) break;
			GameObject tile = newTiles[i];
			if (tile == null) break;
			tiles[held + i] = tile;
		}
		held += i;
		if (i == 0) return; // no tiles added
		Vector3 origin = newTiles[i - 1].transform.localPosition;
		Vector3 destiny = transform.position;
		TileGroupMovement group = TileGroupFactory.Create(grid, newTiles, origin, destiny);
		group.OnEnd(OnReceive);
	}

	public void OnReceive (GameObject[] tiles) {
		for (int i = 0; i < tiles.Length; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			tile.renderer.enabled = false;
		}
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