using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    private Text _pointsCanvas;
    private int _points = 0;

    private void Start()
    {
        _pointsCanvas = GetComponent<Text>();
        UpdatePoints();
    }

    public void AddPoints(int points)
    {
        _points += points;
        UpdatePoints();
    }

    public int GetPoints()
    {
        return _points;
    }

    private void UpdatePoints()
    {
        _pointsCanvas.text = _points.ToString();
    }
}
