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
        SetTarget(_targetDefault, _followAxes, _followOffset, _followSmoothing);
    }

    public void SetTarget(Transform target)
    {
        SetTarget(target, _followAxes, _followOffset, _followSmoothing);
    }

    public void SetTarget(Transform target, Vector3 followAxes)
    {
        SetTarget(target, followAxes, _followOffset, _followSmoothing);
    }

    public void SetTarget(Transform target, Vector3 followAxes, Vector3 followOffset)
    {
        SetTarget(target, followAxes, followOffset, _followSmoothing);
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
        this.transform.position =
            new Vector3(GetAxesPos(0), GetAxesPos(1), GetAxesPos(2)); ;
    }
}
