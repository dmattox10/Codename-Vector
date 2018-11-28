using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	// Use this for initialization
	public Material defaultMaterial;
    public Material selectedMaterial;

    public GameObject AddPiece(GameObject piece, int col, int row)
    {
		Debug.Log("Board.AddPiece");
        Debug.Log(col.ToString());
		Debug.Log(row.ToString());
        Vector2Int gridPoint = Geometry.GridPoint(col, row); // MATH???
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint), Quaternion.identity, gameObject.transform);
		Debug.Log("GridPoint");
		Debug.Log(gridPoint);
        return newPiece;
    }  
	public GameObject AddPlatform(GameObject platform, int col, int row) {
		Vector2Int gridPoint = Geometry.GridPoint(col, row); // MATH???
        GameObject newPlatform = Instantiate(platform, Geometry.PointFromGrid(gridPoint), Quaternion.identity, gameObject.transform);
        return newPlatform;
	}
	public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }
	public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }
	public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = defaultMaterial;
    }
}
