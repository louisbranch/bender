using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {
	
	private Grid grid;
	private GameObject[] holdingTiles = new GameObject[10];
	private int holdingTilesIndex = -1;

	void Start () {
		grid = GameObject.Find("TilesGrid").GetComponent<Grid>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Push();
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Pull();
		}
	}

	void Push () {

	}

	void Pull () {
		GameObject[] tiles = null;
		if (holdingTilesIndex == -1) {
			tiles = grid.PullAnyTilesFrom(Column());
			holdingTilesIndex = tiles.Length -1;
		}
		if (tiles == null) return;
		for (int i = 0; i < holdingTilesIndex; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			holdingTiles[i] = tile;
		}
	}

	private int Column() {
		return (int)transform.position.x;
	}
}