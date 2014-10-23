using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// List which kind of tiles are enabled for this grid
	public GameObject[] tiles;

	// Number of tile columns
	public int width = 11;

	// Number of tile rows
	public int height = 8;
	
	public int initialHeight = 6;

	// Whether the player can move to one side of the screen to the other
	public bool enableCornerMovement = false;

	// Up and down movement speed
	public float tileSpeed = 20.0f;

	// 2D array of tiles (rows x columns)
	private GameObject [,] grid;

	// Array of tiles that are being moved to the player
	private GameObject[] movingTiles;

	void Awake () {
		int bottomGap = height - initialHeight;
		grid = new GameObject[width,height];
		movingTiles = new GameObject[height];

		for (int x = 0; x < width; x++) {
			for (int y = bottomGap; y < height; y++) {
				int rand = Random.Range(0, tiles.Length);
				GameObject tile = tiles[rand];
				GameObject t = (GameObject)Instantiate(tile);
				t.transform.position = new Vector3(x, y, t.transform.position.z);
				grid[x,y] = t;
			}	
		}
	}

	public GameObject[] PullAnyTilesFrom (int x) {
		string name = null;
		int index = 0;
		for (int y = 0; y < height; y++) {
			GameObject tile = grid[x,y];
			if (tile == null) continue;
			if (name == null) {
				name = tile.name;
			} else {
				if (tile.name != name) break;
			}
			grid[x,y] = null;
			movingTiles[index] = tile;
			index++;
		}
		return movingTiles;
	}

	private void Update () {
		for (int i = 0; i < movingTiles.Length; i++) {
			GameObject tile = movingTiles[i];
			if (tile == null) break;
			if (tile.transform.position.y > 0) {
				tile.transform.position = new Vector3(tile.transform.position.x, 
				                                      tile.transform.position.y - tileSpeed * Time.deltaTime,
				                                      tile.transform.position.z);
			} else {
				tile.renderer.enabled = false;
			}

		}
	}

	public void PullTilesTypeFrom (int x, GameObject type) {
		
	}
}