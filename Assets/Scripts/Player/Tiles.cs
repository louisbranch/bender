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
			Vector3 origin = tile.transform.position;
			if (tile.transform.position.y > 0) {
				tile.transform.position = new Vector3(origin.x, 
				                                      origin.y - tileSpeed * Time.deltaTime,
				                                      origin.z);
			} else {
				tile.renderer.enabled = false;
			}
			
		}
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
