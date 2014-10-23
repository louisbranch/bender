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
}