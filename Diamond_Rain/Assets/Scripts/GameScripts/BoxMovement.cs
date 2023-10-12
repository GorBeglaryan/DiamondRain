using UnityEngine;

public enum StoneValues
{
    Marquise = 3,
    Long = 2,
    Emerald = 2,
    Stone = -2,
    Pear = 3,
    Radiant = 2,
    Heart = 3,
    Round = 2,
    Bomb = -6,
}
public class BoxMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 startPosition;


    private float deltaX, deltaY;
    public void Init()
    {
        transform.position = startPosition;
    }
    public void UpdateFrame()
    {

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = (touchPos.x - transform.position.x) * 1.5f;
                    deltaY = (touchPos.y - transform.position.y) * 1.5f;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector3(touchPos.x - deltaX, touchPos.y - deltaY, transform.position.z));
                    break;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.Lerp(
                                    transform.position,
                                    startPosition,
                                    (startPosition - transform.position).magnitude * Time.deltaTime
                                    );
        }

    }

    
}