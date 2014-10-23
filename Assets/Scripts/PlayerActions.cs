using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	public GameObject player;
	private Grid grid;

	private int holdingTiles = 0;
	private GameObject tileType;

	void Start () {
		grid = GameObject.Find("Tiles").GetComponent<Grid>();
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
		if (holdingTiles == 0) {
			grid.PullAnyTilesFrom(player.transform.position);
		}
	}
}