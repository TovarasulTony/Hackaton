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
        STRUCTURE_TYPE[,] structureMap;
        structureMap = Map.instance.GetStructureMap();
        int matrixLength = Map.instance.GetMatrixLength();
        m_MiniTileMatrix = new MiniTile[matrixLength, matrixLength];

        for (int i = 0; i< matrixLength; i++)
        {
            for (int j = 0; j < matrixLength; j++)
            {
                if (Map.instance.GetTile(i, j) == null)
                    continue;
                Vector3 position = new Vector3(i, j, 0);
                MiniTile prefabMiniTile = Instantiate(m_MiniTilePrefab, position, Quaternion.identity, transform);
                prefabMiniTile.transform.localPosition = position;
                switch (structureMap[i, j])
                {
                    case STRUCTURE_TYPE.Wall:
                        prefabMiniTile.SetMiniTileStatus(MINITILE_STATUS.DirtWall); 
                        break;
                    case STRUCTURE_TYPE.ShopWall:
                        prefabMiniTile.SetMiniTileStatus(MINITILE_STATUS.BrickWall);
                        break;
                    default:
                        break;
                }
                
                prefabMiniTile.SetTile(Map.instance.GetTile(i, j));
                m_MiniTileMatrix[i, j] = prefabMiniTile;



            }
        }
    }



}
