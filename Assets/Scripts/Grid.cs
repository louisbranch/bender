using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// List which kind of tiles are enabled for this grid
	public GameObject[] tileTypes;

	// Number of tile columns
	public int width = 11;

	// Max number of tile rows
	public int height = 8;

	// Initial number of tile rows
	public int initialHeight = 5;

	// Whether the player can move to one side of the screen to the other
	public bool enableCornerMovement = false;

	// 2D array of tiles (rows x columns)
	private GameObject [,] grid;

	void Awake () {
		int bottomGap = height - initialHeight;
		grid = new GameObject[width,height];


		for (int x = 0; x < width; x++) {
			for (int y = bottomGap; y < height; y++) {
				int rand = Random.Range(0, tileTypes.Length);
				GameObject tile = tileTypes[rand];
				GameObject t = (GameObject)Instantiate(tile);
				t.transform.parent = transform;
				t.transform.localPosition = new Vector3(IndexToX(x), IndexToY(y), t.transform.localPosition.z);
				grid[x,y] = t;
			}	
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(0, 1, 1, 0.5F);
		Gizmos.DrawCube(transform.position, new Vector3(width, height, 1));
	}
	
				

	public GameObject[] PullAnyTilesFrom (float position) {
		return RemoveTiles(position, null);
	}
	
	public GameObject[] PullTilesTypeFrom (float position, string type) {
		return RemoveTiles(position, type);
	}

	public void PushTilesTo (float position, GameObject[] tiles) {
		int x = XToIndex(position);
		int y;
		int index = 0;
		int last = height - 1;
		for (y = last; y >= 0; y--) {
			GameObject tile = tiles[index];
			if (tile == null) break;
			if (grid[x,y] != null) continue;
			grid[x,y] = tile;
			index++;
		}
		float z = transform.position.z;
		Vector3 origin = new Vector3(position, IndexToY(1), z);
		Vector3 destiny = new Vector3(position, IndexToY(y+1), z);
		new TileGroupFactory(this, tiles, origin, destiny);
	}

	private GameObject[] RemoveTiles (float position, string name) {
		int x = XToIndex(position);
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

	private float IndexToX (int x) {
		return x - width/2;
	}
	
	private float IndexToY (int y) {
		return y - height/2 + 0.5f;
	}

	private int XToIndex (float x) {
		return (int)(x + width/2);
	}
}