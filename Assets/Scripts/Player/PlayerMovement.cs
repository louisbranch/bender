using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	private Grid grid;
	private PlayerTiles tiles;

	private int minX;
	private int maxX;

	void Start () {
		grid = GetComponentInParent<Grid>();
		tiles = GetComponent<PlayerTiles>();
		int half = grid.width / 2;
		minX = -half;
		maxX = grid.width % 2 == 0 ? half - 1 : half;
		Move(0);
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(transform.localPosition.x - 1);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(transform.localPosition.x + 1);
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Push();
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Pull();
		}
	}

	private bool IsOutOfBound (float x) {
		return x < minX || x > maxX;
	}

	private void Move (float xAxis) {
		if (grid.enableCornerMovement) {
			if (xAxis < minX) xAxis = maxX;
			else if (xAxis > maxX) xAxis = minX;
		} else if (IsOutOfBound(xAxis)) {
			return;
		}
		Vector3 p = transform.localPosition;
		transform.localPosition = new Vector3(xAxis, p.y, p.z);
	}

	private void Push () {
		int x = (int)transform.localPosition.x;
		if (!tiles.IsEmpty()) {
			grid.PushTilesTo(x, tiles.Clear());
		}
	}
	
	private void Pull () {
		int x = (int)transform.localPosition.x;
		if (tiles.IsEmpty()) {
			tiles.Add(grid.PullAnyTilesFrom(x));
		} else {
			string type = tiles.Type();
			tiles.Add(grid.PullTilesTypeFrom(x, type));
		}
	}
}