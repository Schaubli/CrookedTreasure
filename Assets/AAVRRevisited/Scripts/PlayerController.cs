﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject playerPrefab;
	private GameObject playerFigure;
	private Tile playerTile;
	private Tile oldPlayerTile; //Tile the player was last


	// Use this for initialization
	public void Initiate (Tile rootTile) {
		instance = this;
		playerFigure = (GameObject) Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		playerFigure.transform.SetParent(this.transform);
		playerFigure.transform.SetAsFirstSibling();
		oldPlayerTile = rootTile;
		this.playerTile = rootTile;
	}
	
	public void MovePlayerToTile(Tile tile) {
		oldPlayerTile = this.playerTile;
		playerTile = tile;
		playerFigure.transform.localPosition = tile.transform.localPosition; // Move Player to new Position
		//Rotate toward new tile
		Quaternion rotation =  Quaternion.FromToRotation(Vector3.forward, tile.transform.localPosition-oldPlayerTile.transform.localPosition);
		Vector3 eulers = rotation.eulerAngles;
		eulers.z = 0; //Avoid flipping
		rotation = Quaternion.Euler(eulers);
		playerFigure.transform.rotation = rotation;
		playerFigure.GetComponent<Animator>().Play("MoveForward");
	}
	
	private static PlayerController instance;
	public static PlayerController Instance { 
		get {
			if(instance == null)
			{
				GameObject obj = new GameObject();
				instance = (PlayerController) obj.AddComponent( typeof(PlayerController));
			}
    		return instance;
    	}
	}
}