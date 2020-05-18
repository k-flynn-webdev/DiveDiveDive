using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Camera Camera
    { get { return this._camera; } }

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _targetDefault;

    [SerializeField]
    private Vector3 _followAxes = new Vector3();

    [SerializeField]
    private Vector3 _followOffset = new Vector3();

    [SerializeField]
    private Vector3 _followSmoothing = new Vector3();

    [SerializeField]
    private Transform _targetLocal;
    private Vector3 _followAxesLocal = new Vector3();
    private Vector3 _followOffsetLocal = new Vector3();
    private Vector3 _followSmoothingLocal = new Vector3();

    [SerializeField]
    private float _minAmount = 0.05f;

    void Awake()
    {
        SetTarget();
        _camera = GetComponent<Camera>();
        ServiceLocator.Register<CameraControl>(this);
    }


    public void SetPosition(Vector3 camPos)
    {
        this.transform.position = camPos;
    }

    private void SetFollowAxesLocal(Vector3 axes)
    {
        _followAxesLocal = new Vector3(
            axes[0] > 0.1f ? 1f : 0f,
            axes[1] > 0.1f ? 1f : 0f,
            axes[2] > 0.1f ? 1f : 0f);
    }

    public void SetTarget()
    {
        _targetLocal = _targetDefault;
        SetFollowAxesLocal(_followAxes);
        _followOffsetLocal = _followOffset;
        _followSmoothingLocal = _followSmoothing;
    }

    public void SetTarget(Transform target)
    {
        _targetLocal = target;
        SetFollowAxesLocal(_followAxes);
        _followOffsetLocal = _followOffset;
        _followSmoothingLocal = _followSmoothing;
    }

    public void SetTarget(Transform target, Vector3 followAxes)
    {
        _targetLocal = target;
        SetFollowAxesLocal(followAxes);
        _followOffsetLocal = _followOffset;
        _followSmoothingLocal = _followSmoothing;
    }

    public void SetTarget(Transform target, Vector3 followAxes, Vector3 followOffset)
    {
        _targetLocal = target;
        SetFollowAxesLocal(followAxes);
        _followOffsetLocal = followOffset;
        _followSmoothingLocal = _followSmoothing;
    }

    public void SetTarget(Transform target, Vector3 followAxes, Vector3 followOffset, Vector3 followSmooth)
    {
        _targetLocal = target;
        SetFollowAxesLocal(followAxes);
        _followOffsetLocal = followOffset;
        _followSmoothingLocal = followSmooth;
    }

    float GetAxesPos(int axes)
    {
        float offsetVar = _followOffsetLocal[axes];
        float current = this.transform.position[axes];
        float goal = _targetLocal.position[axes] + offsetVar;

        return Mathf.Lerp(
            current,
            goal,
            (Time.deltaTime * _followSmoothingLocal[axes]) * _followAxesLocal[axes]);
    }

    void Update()
    {
        Vector3 newPos = new Vector3(GetAxesPos(0), GetAxesPos(1), GetAxesPos(2));
        float distance = Vector3.Distance(this.transform.position, newPos);

        if (distance < _minAmount)
        {
            return;
        }

        this.transform.position = newPos;
    }
}
