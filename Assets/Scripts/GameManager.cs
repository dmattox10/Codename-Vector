using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

    public BoardManager board;

    public GameObject leader_light;
    public GameObject pawn_light;

    public GameObject leader_shadow;
    public GameObject pawn_shadow;

    public GameObject platform;

    private GameObject[,] pieces;

    private Player player_light;
    private Player player_shadow;
    public Player currentPlayer;
    public Player otherPlayer;
	void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start ()
    {
        pieces = new GameObject[5, 5];

        player_light = new Player("player_light", true);
        player_shadow = new Player("player_shadow", false);

        currentPlayer = player_light;
        otherPlayer = player_shadow;

        InitialSetup();
    }

	private void InitialSetup()
    {
        for (int i = -20; i < 21; i=i+10) 
        {
            for (int j = -20; j < 21; j=j+10)
            {
                AddPlatform(platform, i, j);
            }
        }
        AddPiece(leader_shadow, player_shadow, 0, 20);
        AddPiece(pawn_shadow, player_shadow, 20, 20);
		AddPiece(pawn_shadow, player_shadow, 10, 20);
		AddPiece(pawn_shadow, player_shadow, -10, 20);
		AddPiece(pawn_shadow, player_shadow, -20, 20);

        AddPiece(leader_light, player_light, 0, -20);
        AddPiece(pawn_light, player_light, 20, -20);
		AddPiece(pawn_light, player_light, 10, -20);
		AddPiece(pawn_light, player_light, -10, -20);
		AddPiece(pawn_light, player_light, -20, -20);
/*
        for (int i = 0; i < 5; i++) 
        {
            for (int j = 0; j < 5; j++)
            {
                AddPlatform(platform, MapValue(0, 4, -20, 20, i), MapValue(0, 4, -20, 20, j));
            }
        }
*/
        


    }
	public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
		Debug.Log("Add Piece");
		Debug.Log(col.ToString());
		Debug.Log(row.ToString());
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[MapValue(-20, 20, 0, 4, col), MapValue(-20, 20, 0, 4, row)] = pieceObject;
    }
    public void AddPlatform(GameObject prefab, int col, int row){
        GameObject platformObject = board.AddPlatform(prefab, col, row);

    }
	public int MapValue(int a0, int a1, int b0, int b1, int a)
	{
	return b0 + (b1 - b0) * ((a-a0)/(a1-a0));
	}
	public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = pieces[gridPoint.x, gridPoint.y];
        if (selectedPiece)
        {
            board.SelectPiece(selectedPiece);
        }
    }
	public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);

        // filter out offboard locations
        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);

        // filter out locations with friendly piece
        locations.RemoveAll(gp => FriendlyPieceAt(gp));

        return locations;
    }
	public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Piece pieceComponent = piece.GetComponent<Piece>();
        

        Vector2Int startGridPoint = GridForPiece(piece);
        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        board.MovePiece(piece, gridPoint);
    }
    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        if (pieceToCapture.GetComponent<Piece>().type == PieceType.King)
        {
            Debug.Log(currentPlayer.name + " wins!");
            Destroy(board.GetComponent<TileSelector>());
            Destroy(board.GetComponent<MoveSelector>());
        }
        currentPlayer.capturedPieces.Add(pieceToCapture);
        pieces[gridPoint.x, gridPoint.y] = null;
        Destroy(pieceToCapture);
    }

    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        return pieces[gridPoint.x, gridPoint.y];
    }

    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null) {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }

    public void NextPlayer()
    {
        Player tempPlayer = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = tempPlayer;
    }
}


