using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MINITILE_STATUS { Invalid, DirtWall, BrickWall};

public class MiniMap : MonoBehaviour
{

    public static MiniMap instance = null;
    public MiniTile m_MiniTilePrefab;
    MiniTile[,] m_MiniTileMatrix;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("MiniMap instantiat de doua ori");
            Destroy(gameObject);
        }
        instance = gameObject.GetComponent<MiniMap>();
        CreateMiniMap();
    }

    void CreateMiniMap()
    {
        STRUCTURE_TYPE[,] _StructureMap;
        _StructureMap = Map.instance.GetStructureMap();
        int _MatrixLength = Map.instance.GetMatrixLength();
        m_MiniTileMatrix = new MiniTile[_MatrixLength, _MatrixLength];

        for (int i = 0; i< _MatrixLength; i++)
        {
            for (int j = 0; j < _MatrixLength; j++)
            {
                if (Map.instance.GetTile(i, j) == null)
                    continue;
                Vector3 _position = new Vector3(i, j, 0);
                MiniTile _prefab_minitile = Instantiate(m_MiniTilePrefab, _position, Quaternion.identity, transform);
                _prefab_minitile.transform.localPosition = _position;
                switch (_StructureMap[i, j])
                {
                    case STRUCTURE_TYPE.Wall:
                        _prefab_minitile.SetMiniTileStatus(MINITILE_STATUS.DirtWall); 
                        break;
                    case STRUCTURE_TYPE.ShopWall:
                        _prefab_minitile.SetMiniTileStatus(MINITILE_STATUS.BrickWall);
                        break;
                    default:
                        break;
                }
                
                _prefab_minitile.SetTile(Map.instance.GetTile(i, j));
                m_MiniTileMatrix[i, j] = _prefab_minitile;



            }
        }
    }



}
