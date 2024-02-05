using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointsZone : MonoBehaviour
{
    [SerializeField] private TextMeshPro _canYouGoText;
    [SerializeField] private int _goalPoints;
    [SerializeField] private PointsController _pointsCanvas;

    [SerializeField] private UnityEvent _onEnoughPointsEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ArePointsEnough())
            {
                _onEnoughPointsEvent.Invoke();
                _canYouGoText.text = "You can go now!";
            }
            else
            {
                _canYouGoText.text = "Not enough points!\n" +
                                     "You have to get " + _goalPoints.ToString();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canYouGoText.text = "";
        }
    }

    private bool ArePointsEnough()
    {
        return _goalPoints <= _pointsCanvas.GetPoints();
    }
}
