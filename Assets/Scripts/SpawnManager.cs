using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _floorPrefab;
    [SerializeField]
    private GameObject _tilePrefab;

    public Queue<GameObject> FloorPool = new Queue<GameObject>();
    public Queue<GameObject> ActiveFloorPool = new Queue<GameObject>();
    public Queue<GameObject> TilePool = new Queue<GameObject>();
    public Queue<GameObject> ActiveTilePool = new Queue<GameObject>();
    [SerializeField]
    int _tilePoolSize = 15;
    [SerializeField]
    int _activeTilePoolSize = 10;

    [SerializeField]
    private Transform _collectable;
    [SerializeField]
    private int _collectableCount = 3;
    private Transform[] _collecatbles;

    private Transform _lastTile;
    private Transform _currentTile;

    private float _zTilePos = 0f;
    private float _xTilePos = 0f;
    private float _zFloorPos = 20.0f;

    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void Start() {

        _collecatbles = new Transform[_collectableCount];
        for (int i = 0; i < _collectableCount; i++)
        {
            _collecatbles[i] = Instantiate(_collectable);
            _collecatbles[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _tilePoolSize; i++)
        {
            GameObject tileObj = Instantiate(_tilePrefab,Vector3.zero,Quaternion.identity);
            tileObj.SetActive(false);
            TilePool.Enqueue(tileObj);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject floorObj = Instantiate(_floorPrefab, Vector3.zero, Quaternion.identity);
            floorObj.SetActive(false);
            FloorPool.Enqueue(floorObj);
        }


        for (int i = 0; i < _activeTilePoolSize; i++)
        {
            GameObject tileObj = TilePool.Dequeue();
            tileObj.SetActive(true);     
            tileObj.transform.position = new Vector3(_xTilePos,0,_zTilePos);

            int r = Random.Range(1, 7);
            switch (r)
            {
                case 4:
                    r = 1;
                    break;
                case 5:
                    r = 1;
                    break;
                case 6:
                    r = 1;
                    break;
                default:
                    break;
            }
            _zTilePos += (2.5f * r);
            _xTilePos = 0f;
            ActiveTilePool.Enqueue(tileObj);
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject floorObj = FloorPool.Dequeue();
            floorObj.SetActive(true);
            floorObj.transform.position = new Vector3(0, -2f, _zFloorPos);
            _zFloorPos += 50f;
            ActiveFloorPool.Enqueue(floorObj);
        }
    }

    public void SpawnTile()
    {
        GameObject tileObj = TilePool.Dequeue();
        tileObj.SetActive(true);
        tileObj.transform.position = new Vector3(_xTilePos, 0, _zTilePos);
        _currentTile = tileObj.transform;

        if(_lastTile!=null && !_collecatbles[_collectableCount-1].gameObject.activeSelf)
        SpawnCollectable(_lastTile, _currentTile);

        int r = Random.Range(1, 7);
        switch (r)
        {
            case 4:
                r = 1;
                break;
            case 5:
                r = 1;
                break;
            case 6:
                r = 1;
                break;
            default:
                break;
        }
        _zTilePos += (2.5f * r);

        if (_lastTile != null && r == 1)
        {
            _xTilePos = 0f;
        }
        else {
            _xTilePos = Random.Range(-3.5f, 3.5f);
        }
        
        ActiveTilePool.Enqueue(tileObj);
        _lastTile = tileObj.transform;
    }

    public void SpawnFloor()
    {
        GameObject floorObj = FloorPool.Dequeue();
        floorObj.SetActive(true);
        floorObj.transform.position = new Vector3(0, -2f, _zFloorPos);
        _zFloorPos += 50f;
        ActiveFloorPool.Enqueue(floorObj);
    }

    private void SpawnCollectable(Transform startTrans, Transform endTrans)
    {
        Vector3 startPos = startTrans.position;
        Vector3 targetPos = endTrans.position;
        Vector3 middlePos = (targetPos + startPos) * 0.5f + new Vector3(0, 3, 0);

        float deltaInc = 1.0f / (_collectableCount + 1);

        float t = 0;
        for (int i = 0; i < _collectableCount; i++)
        {
            t = t + deltaInc;
            _collecatbles[i].gameObject.SetActive(true);
            Vector3 p1 = Vector3.Lerp(startPos, middlePos, t);
            Vector3 p2 = Vector3.Lerp(middlePos, targetPos, t);
            Vector3 p3 = Vector3.Lerp(p1, p2, t);

            _collecatbles[i].position = p3;

        }
    }
}
