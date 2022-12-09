using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehavior : MonoBehaviour
{

    public GameObject northPiece;
    public GameObject southPiece;
    public GameObject eastPiece;
    public GameObject westPiece;
    public GameObject centerPiece;

    List<GameObject> myPieces;
    List<Image> myPiecesImages;

	private void Awake()
	{
        myPieces = new List<GameObject>();
        myPiecesImages = new List<Image>();

        myPieces.Add(northPiece);
        if (southPiece != null)
        {
            myPieces.Add(southPiece);
            myPieces.Add(eastPiece);
            myPieces.Add(westPiece);
            myPieces.Add(centerPiece);
        }

        foreach(GameObject piece in myPieces)
		{
            myPiecesImages.Add(piece.GetComponent<Image>());
		}

	}

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image image in myPiecesImages)
		{
            image.color = References.CrosshairColor;
		}
    }
}
