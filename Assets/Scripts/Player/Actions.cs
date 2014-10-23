using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {
	
	private Grid grid;
	private Tiles tiles;

	void Start () {
		grid = GameObject.Find("TilesGrid").GetComponent<Grid>();
		tiles = GetComponent<Tiles>();
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
		int x = (int)transform.position.x;
		if (tiles.Empty()) {
			tiles.Add(grid.PullAnyTilesFrom(x));
		} else {
			string type = tiles.Type();
			tiles.Add(grid.PullTilesTypeFrom(x, type));
		}
	}
}