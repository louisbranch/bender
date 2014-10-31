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
			GameObject tile = newTiles[i];
			if (tile == null) break;
			if (held + i < maxTilesHeld) {
				tiles[held + i] = tile;
			} else {
				Destroy(tile);
			}
		}
		held += i;
		if (i == 0) return; // no tiles added
		GameObject firsTile = newTiles[i-1];
		Vector3 origin = firsTile.transform.localPosition;
		MoveTiles(newTiles, origin);
	}

	private void MoveTiles (GameObject[] tiles, Vector3 origin) {
		Vector3 destiny = transform.position;
		TileGroupMovement group = TileGroupFactory.Create(grid, tiles, origin, destiny);
		group.OnMovementEnd(OnReceive);
	}

	public void OnReceive (GameObject[] tiles) {
		for (int i = 0; i < tiles.Length; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			tile.renderer.enabled = false;
		}
	}

	public GameObject[] Clear () {
		GameObject[] copy = tiles;
		tiles = new GameObject[maxTilesHeld];
		held = 0;
		return copy;
	}
}