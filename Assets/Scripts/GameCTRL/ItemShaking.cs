/*using UnityEngine;
using DG.Tweening;

public class ItemShaking : MonoBehaviour
{
    public float moveDuration = 2f;
    public float moveDistance = 0.5f;
    public Vector3 weaveAmplitude = new Vector3(0.1f, 0.1f, 0);

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        AnimateWater();
    }

    void AnimateWater()
    {
        Sequence waterSequence = DOTween.Sequence();

        // Moving water up and down
        waterSequence.Append(transform.DOMoveY(initialPosition.y + moveDistance, moveDuration).SetEase(Ease.InOutSine));
        waterSequence.Append(transform.DOMoveY(initialPosition.y - moveDistance, moveDuration).SetEase(Ease.InOutSine));

        // Weaving effect
        waterSequence.Join(transform.DOPunchPosition(weaveAmplitude, moveDuration * 2, 10, 1f));

        // Loop the animation
        waterSequence.SetLoops(-1, LoopType.Yoyo);
    }
}
*/
