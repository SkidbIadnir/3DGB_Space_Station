using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenScript : MonoBehaviour {

    public Transform trans;
	public float TileDimension = 4.0f;
	public bool ClusterMode=false;
	public GameObject[] prefabClusterBlue;
	public GameObject[] prefabClusterBlack;
	public GameObject[] prefabClusterWhite;
	public GameObject[] prefabClusterRed;
	public GameObject[] prefabClusterGreen;
	public GameObject[] prefabClusterCyan;
	public GameObject[] prefabClusterMagenta;

    public GameObject[] prefabFloor;
	public GameObject[] prefabGround_1;
	public GameObject[] prefabGround_2;
	public GameObject[] prefabWall_01;
	public GameObject[] prefabWall_02;
	public GameObject[] prefabWall_03;
	public GameObject[] prefabWall_04;
    public GameObject[] prefabWall;
    public GameObject[] prefabCurveL;
	public GameObject[] prefabCollumn;
    public GameObject[] prefabCeiling;
	public GameObject[] prefabBloodCell;
	public GameObject[] prefabVirus;
//    public Texture2D Map;

    private int width;
    private int height;
	private System.Random rnd = new System.Random();
	private System.Random rnd2 = new System.Random();
	private bool player_exists = false;

    
    public void PressButon() {
		if(!ClusterMode) {
			GenerateMap();
		} else {
			GenerateClusterMap();
		}
    }

	public Texture2D getMapTexture() {
        string filename = "Assets/MapGenerator/Maps/Big map test.png";
        var rawData = System.IO.File.ReadAllBytes(filename);
        Texture2D tex = new Texture2D(2, 2); // Create an empty Texture; size doesn't matter (she said)
        tex.LoadImage(rawData);
		return (tex);
	}

	private void GenerateClusterMap() { // Création basique de la carte. Placement des objets au bon endroit
		float multiplierFactor = TileDimension + float.Epsilon;
	    Texture2D Map = getMapTexture();
		width = Map.width;
		height = Map.height;
		Color[] pixels = Map.GetPixels();
		for (int pos_x = 0; pos_x < height; pos_x++) {
			for (int pos_z = 0; pos_z < width; pos_z++) {
				Color pixelColor = pixels[pos_x * height + pos_z];
				if (pixelColor == Color.white) { // Si la case est blanche, tu places le sol
					if(prefabClusterWhite.Length!=0) {
						GameObject inst = GameObject.Instantiate(randomPrefab(prefabClusterWhite), trans);
						inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
				}
				if (pixelColor == Color.black) { // Si la case est noire, tu places rien
					if(prefabClusterBlack.Length!=0) {
						GameObject inst = GameObject.Instantiate(randomPrefab(prefabClusterBlack), trans);
						inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
				}
				if (pixelColor == Color.blue) { // Si c'est bleu, ça place une colonne/couloir (la colonne correspond à un demi mur avec du sol, du coup il fait la liaison entre sol et mur pour les couloirs)
					if(prefabClusterBlue.Length!=0) {
						GameObject inst = GameObject.Instantiate(randomPrefab(prefabClusterBlue), trans);
						inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
				}
				if (pixelColor == Color.red) { // Si la case est rouge, tu places un mur
					if(prefabClusterRed.Length!=0) {
						GameObject inst = GameObject.Instantiate(randomPrefab(prefabClusterRed), trans);
						inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
				}
				if (pixelColor == Color.green) { // Les cases vertes permettent de définir le coin d'une salle
					if(prefabClusterGreen.Length!=0) {
						GameObject inst = GameObject.Instantiate(randomPrefab(prefabClusterGreen), trans);
						inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
				}
				if (pixelColor == Color.cyan) { // Test pour un plafond si jamais on veut cacher des zones
					if(prefabClusterCyan.Length!=0) {
						GameObject inst = GameObject.Instantiate(randomPrefab(prefabClusterCyan), trans);
						inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
				}
			}
		}
	}

	private void createBloodCell() {

	}

    private void GenerateMap() { // La c'est la map avec les rotations en plus
		float multiplierFactor = TileDimension + float.Epsilon;
		Texture2D Map = getMapTexture();
		GameObject myMap = new GameObject("Map");
		width = Map.width;
		height = Map.height;
		Color[] pixels = Map.GetPixels();
        for (int pos_x = 0; pos_x < height; pos_x++) {
            for (int pos_z = 0; pos_z < width; pos_z++) {
                Color pixelColor = pixels[pos_x * height + pos_z];
				if (pixelColor == Color.white) { // Sol
					if(prefabGround_1.Length != 0) {
//						int player = rnd.Next(1, 60);
//						if (player == 1 && player_exists == false); {
//							GameObject playerObject = GameObject.Find("/");
    	                	//.transform.position = new Vector3(pos_z * multiplierFactor, 1, pos_x * multiplierFactor);
//						}
						int monster = rnd.Next(1, 100);
						if (monster == 50) {
							GameObject spawnMonster = GameObject.Instantiate(randomPrefab(prefabVirus), trans);
							spawnMonster.transform.localScale = new Vector3(30.0f, 30.0f, 30.0f);
							spawnMonster.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    	                	spawnMonster.transform.position = new Vector3(pos_z * multiplierFactor - 2, 1, pos_x * multiplierFactor - 2);
							spawnMonster.transform.SetParent(myMap.transform);
						} else if (monster > 95) {
							GameObject spawnMonster = GameObject.Instantiate(randomPrefab(prefabBloodCell), trans);
							spawnMonster.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
    	                	spawnMonster.transform.position = new Vector3(pos_z * multiplierFactor - 2, 1, pos_x * multiplierFactor - 2);
							spawnMonster.transform.SetParent(myMap.transform);
						}
						selectGroundPrefab(pos_x, pos_z, multiplierFactor, myMap);
					}
                }
                if (pixelColor == Color.red) // Mur
                {
					if(prefabWall_01.Length!=0) {
                    	GameObject inst = selectWallPrefab(pos_x, pos_z, multiplierFactor, myMap, pixels);
						float my_Rotation = FindRotationW(pixels, pos_x, pos_z);
						inst.transform.Rotate(new Vector3(0, my_Rotation, 0), Space.Self);
						if (my_Rotation == 180)
	                    	inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor + 2);
						else if (my_Rotation == 90)
		                    inst.transform.position = new Vector3(pos_z * multiplierFactor + 2, 0, pos_x * multiplierFactor);
						else if (my_Rotation == 0)
		                    inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor - 2);
						else
		                    inst.transform.position = new Vector3(pos_z * multiplierFactor - 2, 0, pos_x * multiplierFactor);
						selectGroundPrefab(pos_x, pos_z, multiplierFactor, myMap);
					}
                }
                if (pixelColor == Color.green) // Angle de mur
                {
					if(prefabWall_01.Length!=0) {
                    	GameObject inst = selectWallPrefab(pos_x, pos_z, multiplierFactor, myMap, pixels);
					    inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
						inst.transform.localScale = new Vector3(1.42f, 1.0f, 1.0f);
						inst.transform.Rotate(new Vector3(0, FindRotationL(pixels, pos_x, pos_z) + 45, 0), Space.Self);
						selectGroundPrefab(pos_x, pos_z, multiplierFactor, myMap);
					}
                }
				if (pixelColor == Color.blue) // Couloir
				{
					if(prefabCollumn.Length!=0) {
						selectGroundPrefab(pos_x, pos_z, multiplierFactor, myMap);
					}
				}
                if (pixelColor == Color.cyan) { // Plafond
					if(prefabFloor.Length!=0) {
                   		GameObject inst = GameObject.Instantiate(randomPrefab(prefabCeiling), trans);
                    	inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
					}
                }
            }
        }
    }

	private void selectGroundPrefab(int pos_x, int pos_z, float multiplierFactor, GameObject myMap)
	{
		int selecter = rnd2.Next(1, 3);
		int rotater = rnd2.Next(0, 4);
		GameObject inst;

		if (selecter == 1) {
			inst = GameObject.Instantiate(randomPrefab(prefabGround_1), trans);
		} else {
    		inst = GameObject.Instantiate(randomPrefab(prefabGround_2), trans);
		}
		inst.transform.Rotate(new Vector3(0, (90 * rotater), 0), Space.Self);
	    inst.transform.position = new Vector3(pos_z * multiplierFactor, 0, pos_x * multiplierFactor);
		inst.transform.SetParent(myMap.transform);
	}

	private GameObject selectWallPrefab(int pos_x, int pos_z, float multiplierFactor, GameObject myMap, Color[] pixels)
	{
		GameObject inst;
		int selecter = rnd2.Next(1, 4);

		if (selecter == 1) {
			inst = GameObject.Instantiate(randomPrefab(prefabWall_01), trans);
		} else if (selecter == 2) {
			inst = GameObject.Instantiate(randomPrefab(prefabWall_02), trans);
		} else if (selecter == 3) {
			inst = GameObject.Instantiate(randomPrefab(prefabWall_03), trans);
		} else {
			inst = GameObject.Instantiate(randomPrefab(prefabWall_04), trans);
		}
		inst.transform.SetParent(myMap.transform);
		return (inst);
	}

    // Donner une rotation au mur selon son placement
	private float FindRotationW(Color[] pixels, int pos_x, int pos_z) {
        // Valeur de base si il a le vide sur sa droite
		float Rotation = 90.0f;
		// vide en dessous de lui
		if (pos_x - 1 >= 0 && (pixels[(pos_x - 1) * height + pos_z] == Color.black || pixels[(pos_x - 1) * height + pos_z] == Color.cyan)){
			Rotation = 00.0f;
		}
		// vide sur sa gauche
		else if (pos_z - 1 >= 0 && (pixels[pos_x * height + (pos_z - 1)] == Color.black || pixels[pos_x * height + (pos_z - 1)] == Color.cyan)){
			Rotation = -90.0f;
		}
		// vide au dessus de lui
		else if (pos_x + 1 < height && (pixels[(pos_x + 1) * height + pos_z] == Color.black || pixels[(pos_x + 1) * height + pos_z] == Color.cyan)){
			Rotation = 180.0f;
		}
		return Rotation;
    }

	// Même système mais pour la colonne
    private float FindRotationC(Color[] pixels, int pos_x, int pos_z) {
		float Rotation = 0.0f;
		if (pos_x - 1 >= 0 && pos_z + 1 < width && (pixels[(pos_x - 1) * height + (pos_z + 1)] == Color.black || pixels[(pos_x - 1) * height + (pos_z + 1)]==Color.cyan))
			Rotation = 90.0f;
		else if (pos_x - 1 >= 0 && pos_z - 1 >= 0 && (pixels[(pos_x - 1) * height + (pos_z - 1)] == Color.black || pixels[(pos_x - 1) * height + (pos_z - 1)]==Color.cyan))
			Rotation = 180.0f;
		else if (pos_x + 1 < height && pos_z - 1 >= 0 && (pixels[(pos_x + 1) * height + (pos_z - 1)] == Color.black || pixels[(pos_x + 1) * height + (pos_z - 1)] == Color.cyan))
			Rotation = -90.0f;
		return Rotation;
	}

    // Même système mais pour les angles
	private float FindRotationL(Color[] pixels, int pos_x, int pos_z) {
		float rotation = 0.0f;
		if (((pixels[pos_x * height + pos_z - 1] == Color.black || pixels[pos_x * height + pos_z - 1] == Color.cyan)) && ((pixels[(pos_x - 1) * height + pos_z] == Color.black) || (pixels[(pos_x - 1) * height + pos_z] == Color.cyan)))
			rotation = 180.0F;
		if (((pixels[pos_x * height + pos_z - 1] == Color.black) || (pixels[pos_x * height + pos_z - 1] == Color.cyan)) && ((pixels[(pos_x + 1) * height + pos_z] == Color.black) || (pixels[(pos_x + 1) * height + pos_z] == Color.cyan)))
			rotation = -90.0f;
		if (((pixels[pos_x * height + pos_z + 1] == Color.black) || (pixels[pos_x * height + pos_z + 1] == Color.cyan)) && ((pixels[(pos_x - 1) * height + pos_z] == Color.black) || (pixels[(pos_x - 1) * height + pos_z] == Color.cyan)))
			rotation = 90.0f;
		return rotation;
	}

	// Chope un des éléments du type de prefab correspondant
    private GameObject randomPrefab(GameObject[] prefabArray) {
		if (prefabArray.Length > 0)
        	return prefabArray[UnityEngine.Random.Range(0, prefabArray.Length-1)];
		return null;
    }
}
