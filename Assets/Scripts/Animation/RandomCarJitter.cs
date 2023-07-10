using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCarJitter : MonoBehaviour
{
    public int framesBetweenStateChange;
    public float moveBounds;

    enum State
    {
        NEUTRAL,
        RAISED,
        LOWERED,
    }

    private State state = State.NEUTRAL;
    private int framesToWait;
    private Vector3 origin;
    private Vector3 newPosition;
    private float minBounds;

    void Awake()
    {
        framesToWait = framesBetweenStateChange;
        origin = transform.localPosition;
        newPosition = new Vector3(0.0f, 0.0f, 0.0f);
        minBounds = moveBounds / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (framesToWait == 0)
        {
            switch (state)
            {
                case State.NEUTRAL:
                    newPosition.y = Random.Range(minBounds, moveBounds);
                    transform.localPosition = newPosition;
                    state = State.RAISED;
                    break;
                case State.RAISED:
                    newPosition.y = Random.Range(-moveBounds, -minBounds);
                    transform.localPosition = newPosition;
                    state = State.LOWERED;
                    break;
                case State.LOWERED:
                    newPosition.y = Random.Range(minBounds, moveBounds);
                    transform.localPosition = newPosition;
                    state = State.RAISED;
                    break;
            }
            framesToWait = framesBetweenStateChange;
        }
        else
        {
            framesToWait--;
        }
    }
}
