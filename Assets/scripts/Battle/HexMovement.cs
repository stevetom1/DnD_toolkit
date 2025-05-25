using UnityEngine;
using System.Collections.Generic;

public class HexMovement
{
    private static readonly (int dx, int dy)[] EVEN_Q_DIRECTIONS = new (int, int)[6]
    {
        (1,0),(0,-1),(-1,-1),(-1,0),(-1,1),(0,1)
    };

    private static readonly (int dx, int dy)[] ODD_Q_DIRECTIONS = new (int, int)[6]
    {
        (1,0),(1,-1),(0,-1),(-1,0),(0,1),(1,1)
    };

    public static HashSet<Vector2Int> GetReachableTiles(Vector2Int start, int steps, int mapWidth, int mapHeight)
    {
        var visited = new HashSet<Vector2Int>();
        var queue = new Queue<(Vector2Int pos, int remainingSteps)>();
        queue.Enqueue((start, steps));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var(current, remainingSteps) = queue.Dequeue();

            if(remainingSteps == 0)  continue;

            var directions = current.x % 2 == 0 ? EVEN_Q_DIRECTIONS : ODD_Q_DIRECTIONS;

            foreach(var (dx, dy) in directions)
            {
                var neighbor = new Vector2Int(current.x + dx, current.y + dy);
                //jestli je na mapì

                if (visited.Contains(neighbor)) continue;
                //kontrola jestli hex je obsazen

                visited.Add(neighbor);
                queue.Enqueue((neighbor, remainingSteps - 1));
            }
        }
        return visited;
    }
}
