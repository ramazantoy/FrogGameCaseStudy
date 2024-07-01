using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public float xOffset = 0.8f; // X ekseninde offset (hexagon genişliğinin 3/4'ü)
    public float yOffset = 0.866f; // Y ekseninde offset (hexagon yüksekliğinin yarısı, 3/4'ü değil)
    public GameObject tilePref;
    public Vector3 startPosition = new Vector3(0, 0, 0);

    public int gridWidth = 5;
    public int gridHeight = 5;

    private void Start()
    {
        for (var x = 0; x < gridWidth; x++)
        {
            var xPos = startPosition.x + xOffset * x;
            for (var y = 0; y < gridHeight; y++)
            {
                var yPos = startPosition.y - yOffset * y - (x % 2 == 0 ? 0 : yOffset / 2);
                var tileTemp = Instantiate(tilePref, transform);
                tileTemp.transform.position = new Vector3(xPos, yPos, startPosition.z);
                tileTemp.name = $"{x}-{y}"; // hex ismi 0-0, 0-1, 1-0 şeklinde
            }
        }
    }
}
