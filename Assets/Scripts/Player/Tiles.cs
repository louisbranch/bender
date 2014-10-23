using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour {

	// Max number of tiles a player can hold at the same time
	public int maxTilesHeld = 10;
	
	// Up and down movement speed
	public float tileSpeed = 20.0f;

	private GameObject[] tiles;
	private int held = 0;
	
	private void Start () {
		tiles = new GameObject[maxTilesHeld];
	}

	private void Update () {
		for (int i = 0; i < held; i++) {
			GameObject tile = tiles[i];
			if (tile == null) break;
			if (!tile.renderer.enabled) continue;
			if (tile.transform.position.y > 0) {
				tile.transform.position = new Vector3(tile.transform.position.x, 
				                                      tile.transform.position.y - tileSpeed * Time.deltaTime,
				                                      tile.transform.position.z);
			} else {
				tile.renderer.enabled = false;
			}
			
		}
	}

	public bool Empty () {
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
			tiles[held + i] = tile;
		}
		held += i;
	}

}
