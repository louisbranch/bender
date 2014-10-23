using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// List which kind of tiles are enabled for this grid
	public GameObject[] tiles;

	// Number of tile columns
	public int width = 11;

	// Number of tile rows
	public int height = 8;
	
	public int initialHeight = 5;

	// Up and down movement speed
	public float tileSpeed = 20.0f;

	// Whether the player can move to one side of the screen to the other
	public bool enableCornerMovement = false;

	// 2D array of tiles (rows x columns)
	private GameObject [,] grid;

	void Awake () {
		int bottomGap = height - initialHeight;
		grid = new GameObject[width,height];

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

	private void Update () {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject tile = grid[x,y];
				if (tile == null) continue;
				tile.renderer.enabled = true;
				Vector3 origin = tile.transform.position;
				if (origin.x != x) {
					origin.x = x;
					tile.transform.position = origin;
				}
				Vector3 destiny = new Vector3(x, y, origin.z);
				if (origin == destiny) continue;
				float step = tileSpeed * Time.deltaTime;
				tile.transform.position = Vector3.MoveTowards(origin, destiny, step);
			}
		}
	}

	public GameObject[] PullAnyTilesFrom (int x) {
		return RemoveTiles(x, null);
	}
	
	public GameObject[] PullTilesTypeFrom (int x, string type) {
		return RemoveTiles(x, type);
	}

	public void PushTilesTo (int x, GameObject[] tiles) {
		int index = 0;
		int last = height - 1;
		for (int y = last; y >= 0; y--) {
			GameObject tile = tiles[index];
			if (tile == null) break;
			if (grid[x,y] != null) continue;
			grid[x,y] = tile;
			index++;
		}
	}

	private GameObject[] RemoveTiles (int x, string name) {
		GameObject[] tiles = new GameObject[height];
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
			tiles[index] = tile;
			index++;
		}
		return tiles;
	}
}