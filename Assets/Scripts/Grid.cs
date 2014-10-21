using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject tile;
	public static int width = 8;
	public static int height = 6;
	private GameObject [,] grid = new GameObject[width,height];

	void Awake () {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject t = (GameObject)Instantiate(tile);
				t.transform.position = new Vector3(-width/2 + x, -height/2 + y, t.transform.position.z);
				t.renderer.material.color = new Color(Random.value,Random.value,Random.value,1);
				grid[x,y] = t;
			}	
		}
	}
}