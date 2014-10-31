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

	// 2D array of tiles (columns x rows)
	private GameObject [,] grid;

	void OnDrawGizmos() {
		Gizmos.color = new Color(0, 1, 1, 0.5F);
		Vector3 position = transform.position;
		position.y -= 0.5f; // centralize gizmo
		Gizmos.DrawCube(position, new Vector3(width, height, 1));
	}

	void Awake () {
		CreateTiles();
	}

	public GameObject[] PullAnyTilesFrom (float column) {
		return SliceTiles(column, null);
	}
	
	public GameObject[] PullTilesTypeFrom (float column, string type) {
		return SliceTiles(column, type);
	}

	public void PushTilesTo (float column, GameObject[] tiles) {
		int x = XToIndex(column);
		int collisionIndex = -1;
		int index = 0;
		int last = height - 1;
		for (int y = last; y >= 0; y--) { // move from top to bottom
			GameObject tile = tiles[index];
			if (tile == null) break;
			if (grid[x,y] != null) continue;
			grid[x,y] = tile;
			if (collisionIndex == -1) collisionIndex = y;
			index++;
		}
		MoveTiles(tiles, column, IndexToY(collisionIndex));
	}

	public void OnCollision (GameObject[] tiles) {
		GameObject first = tiles[0];
		int column = XToIndex(first.transform.localPosition.x);
		CheckSequence(column);

	}

	private void MoveTiles (GameObject[] tiles, float x, float y) {
		float z = transform.position.z;
		Vector3 origin = new Vector3(x, IndexToY(1), z);
		Vector3 destiny = new Vector3(x, y, z);
		TileGroupMovement group = TileGroupFactory.Create(this, tiles, origin, destiny);
		group.OnMovementEnd(OnCollision);
	}

	private GameObject[] SliceTiles (float column, string name) {
		int x = XToIndex(column);
		GameObject[] tiles = new GameObject[height];
		int index = 0;
		for (int y = 0; y < height; y++) {
			GameObject tile = grid[x,y];
			if (tile == null) continue;
			if (name == null) name = tile.name;
			else if (tile.name != name) break;
			grid[x,y] = null;
			tiles[index] = tile;
			index++;
		}
		return tiles;
	}

	private void CheckSequence(int x) {

	}

	private float IndexToX (int x) {
		return x - width/2;
	}
	
	private float IndexToY (int y) {
		return y - height/2;
	}

	private int XToIndex (float x) {
		return (int)(x + width/2);
	}

	private int FirstTileIndex (int x) {
		// TODO
		return x;
	}

	private void CreateTiles () {
		int bottomGap = height - initialHeight;
		grid = new GameObject[width,height];
		
		for (int x = 0; x < width; x++) {
			for (int y = bottomGap; y < height; y++) {
				int rand = Random.Range(0, tileTypes.Length);
				GameObject tile = (GameObject)Instantiate(tileTypes[rand]);
				tile.transform.parent = transform;
				tile.transform.localPosition = new Vector3(IndexToX(x), IndexToY(y), tile.transform.localPosition.z);
				grid[x,y] = tile;
			}	
		}
	}
}