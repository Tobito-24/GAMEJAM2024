using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePredictor : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _Line;

    [SerializeField]
    private Transform _ProjectileOrigin;

    [SerializeField]
    private string[] _ReflectTags;

    [SerializeField]
    private int _MaxBounceCount;

    [SerializeField]
    private LayerMask _LayerMask;

    private void Update()
    {
        Recalculate();
    }

    public void Recalculate()
    {
        var hitPoints = new List<Vector3>();
        hitPoints.Add(transform.position);

        var origin = _ProjectileOrigin.position;
        var direction = _ProjectileOrigin.up;

        int bounceCount = 0;
        while (bounceCount < _MaxBounceCount)
        {
            var hit = TracePath(origin, direction);
            if (hit.collider == null)
            {
                hitPoints.Add(origin + direction * 100f);
                break;
            }

            hitPoints.Add(hit.point);

            if (CanBounce(hit.collider.gameObject))
            {
                origin = hit.point + hit.normal * 0.01f;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else if (hit.collider.gameObject.CompareTag("Wall"))
            {
                bounceCount = _MaxBounceCount;
            }
            else break;

            bounceCount++;
        }

        SetLineRenderer(hitPoints.ToArray());
    }

    private void SetLineRenderer(Vector3[] points)
    {
        _Line.positionCount = points.Length;
        _Line.SetPositions(points);
    }

    private bool CanBounce(GameObject other) => _ReflectTags.Any(other.CompareTag);

    private RaycastHit2D TracePath(Vector2 origin, Vector2 direction)
    {
        return Physics2D.Raycast(origin, direction, Mathf.Infinity, _LayerMask);
    }
}
