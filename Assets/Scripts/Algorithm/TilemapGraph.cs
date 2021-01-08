using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph: IGraph<Vector3Int> {
    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions = {
        //Base
        new Vector3Int(-1, 0, 0), //Up
        new Vector3Int(1, 0, 0), //Down
        new Vector3Int(0, -1, 0), //Left  
        new Vector3Int(0, 1, 0),  //Right 

        //new Vector3Int(-1, -1, 0), //Up left 
        //new Vector3Int(-1, 1, 0),  //Up right
        //new Vector3Int(1, 1, 0),  //Up right
        //new Vector3Int(1, -1, 0),  //Up right
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node) {
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }

    public IEnumerable<Edge> Edges(Vector3Int node)
    {
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
            {
                int neighborWeight = GetWeight(neighborTile);
                yield return new Edge(neighborPos, neighborWeight);
            }
        }
    }

    private int GetWeight(TileBase neighborTile)
    {
        GameObject allowedObject = GameObject.Find("AllowedTiles");
        AllowedTiles allowed = allowedObject.GetComponent<AllowedTiles>();
        return allowed.GetWeight(neighborTile);
    }
}
