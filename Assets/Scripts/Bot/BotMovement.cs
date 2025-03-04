using System;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private float _speedMove = 50f;
    private float _rangeDetection = 1f;

    public event Action TargetResourceAchieved;
    public event Action ResourceDelivered;
    public event Action TargetBaseAchieved;

    public void MoveTo(Vector3 targetPosition, bool isDeliveringResource = false, bool isMovingToFlag = false)
    {
        RotateBot(targetPosition);

        Move(targetPosition);

        if (IsWithinRange(targetPosition) == true)
        {
            if (isMovingToFlag == true)
            {
                TargetBaseAchieved?.Invoke();
            }
            else if (isDeliveringResource == true)
            {
                ResourceDelivered?.Invoke();
            }
            else
            {
                TargetResourceAchieved?.Invoke();
            }
        }
    }

    private void Move(Vector3 target)
    {
        float currentY = transform.position.y;
        Vector3 targetPosition = new Vector3(target.x, currentY, target.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedMove * Time.deltaTime);
    }

    private void RotateBot(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private bool IsWithinRange(Vector3 target)
    {
        int degree = 2;
        float rangeDetectionSqrt = Mathf.Pow(_rangeDetection, degree);
        float distance = (transform.position - target).sqrMagnitude;

        return distance <= rangeDetectionSqrt;
    }
}