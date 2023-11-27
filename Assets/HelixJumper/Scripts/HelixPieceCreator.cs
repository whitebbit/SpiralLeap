using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts;
using HelixJumper.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using YG;
using Random = UnityEngine.Random;

public class HelixPieceCreator : MonoBehaviour
{
    private const int PIECE_COUNT = 12;
    [SerializeField] private Piece piecePrefab;
    [SerializeField] private Piece deadPiecePrefab;
    private List<Piece> _pieceList = new List<Piece>();
    private Collider _collider;
    public bool isStartPiece;
    public bool destroyed { get; private set; }

    private static int _lastLevelNumber;

    private void OnEnable()
    {
        //MoveController.Move += Move;
    }

    private void OnDisable()
    {
        //MoveController.Move -= Move;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (isStartPiece) return;

        CreatePieces();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 180f), 0);
    }

    private void Move()
    {
        if (!Ball.IsGameOver && GameManager.isStarted)
        {
            transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 10f, Space.World);
        }
    }

    private void CreatePieces()
    {
        float zAngeValue = 0;

        //Create all piece
        for (var i = 0; i < PIECE_COUNT; i++)
        {
            var piece = Instantiate(piecePrefab, transform, false);
            piece.transform.rotation = Quaternion.Euler(90f, 0, zAngeValue);
            piece.ChangeMaterial(GameManager.CurrentTheme.Ground);
            zAngeValue += 30f;
            _pieceList.Add(piece);
        }

        //Remove extra piece
        RemovePiece(Random.Range(0, 7));
        CreateDeadPiece();
    }

    private void RemovePiece(int value)
    {
        switch (value)
        {
            case 0:
                for (var i = 0; i < 2; i++)
                {
                    RemovePieceFromList(i);
                }

                break;
            case 5:
                for (var i = 0; i < 3; i++)
                {
                    RemovePieceFromList(i);
                }

                break;
            case 1:
                for (var i = 0; i < 4; i++)
                {
                    RemovePieceFromList(i);
                }

                break;
            case 2:
                for (var i = 0; i < 6; i++)
                {
                    RemovePieceFromList(i);
                }

                break;
            case 3:
                RemovePieceFromList(0);
                RemovePieceFromList(1);
                RemovePieceFromList(4);
                RemovePieceFromList(5);
                RemovePieceFromList(8);
                RemovePieceFromList(9);
                break;
            case 4:
                RemovePieceFromList(0);
                RemovePieceFromList(1);
                RemovePieceFromList(2);
                RemovePieceFromList(6);
                RemovePieceFromList(7);
                RemovePieceFromList(8);
                break;
        }

        _pieceList = _pieceList.Where(obj => obj != null).ToList();
    }

    private void RemovePieceFromList(int index)
    {
        if (_pieceList[index] == null) return;
        Destroy(_pieceList[index].gameObject);
        _pieceList[index] = null;
    }

    private void CreateDeadPiece()
    {
        if (_pieceList.Count >= 12)
            RemovePiece(Random.Range(0, 0));

        var indexes = new List<int>();
        for (var j = 0; j < _pieceList.Count; j++)
        {
            indexes.Add(j);
        }

        for (var i = 0; i < Random.Range(1, 6); i++)
        {
            var deadPiece = Instantiate(deadPiecePrefab, transform, false);
            var rand = indexes[Random.Range(0, indexes.Count)];
            var rotation = _pieceList[rand].transform.rotation;

            RemovePieceFromList(rand);
            deadPiece.transform.rotation = rotation;
            deadPiece.ChangeMaterial(GameManager.CurrentTheme.DeadGround);
            indexes.Remove(rand);
            _pieceList.Add(deadPiece);
        }

        _pieceList = _pieceList.Where(obj => obj != null).ToList();
    }

    private void DestroyAllPieces()
    {
        foreach (Transform child in transform)
        {
            _pieceList.Clear();
            Destroy(child.gameObject);
        }
    }

    public void ResetNewPiece()
    {
        DestroyAllPieces();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Up"))
        {
            ResetNewPiece();
        }
    }

    public void DestroyPieces()
    {
        foreach (var piece in _pieceList)
        {
            piece.Destroy(1);
        }

        destroyed = true;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        _collider.enabled = false;
    }
}