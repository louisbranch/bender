using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject[] tiles;

	public static int width = 11;
	public static int height = 6;
	private GameObject [,] grid = new GameObject[width,height];

	void Awake () {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				int rand = Random.Range(0, tiles.Length);
				GameObject tile = tiles[rand];
				GameObject t = (GameObject)Instantiate(tile);
				t.transform.position = new Vector3(x, y, t.transform.position.z);
				grid[x,y] = t;
			}	
		}
	}

	public void PullAnyTilesFrom (Vector3 destiny) {
		int x = (int) destiny.x;
		string name = null;
		for (int y = 0; y < height; y++) {
			GameObject tile = grid[x,y];
			if (tile == null) continue;
			if (name == null) {
				name = tile.name;
			} else {
				if (tile.name != name) break;
			}
			grid[x,y] = null;
			Vector3 origin = tile.transform.position;
			tile.transform.position = Vector3.Lerp(origin, destiny, 1);
		}
	}

	public void PullTilesTypeFrom (int x, GameObject type) {
		
	}
}