using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpikeController : MonoBehaviour
{
    private static SpikeController _instace;
    public static SpikeController Instace
    {
        get { return _instace; }
    }


    [Header("Camera FOV settings")]
    public float _maxFOV = 90f;
    public float _minFOV = 60f;
    public float _zoomSpeed = 30f;
    public float _maxCameraY;
    public float _minCameraY;
    public float _rotateSpeedCameraY;
    public float _currentCameraY;


    [Header("Spike")]
    public bool inSpike;
    public float _maxSpikeForce = 0f;
    public float _spikeGrowthRate;
    public float _spikeForce = 0;

    [Header("UI")]
    public GameObject _arrow;
    public Slider _spikeForceSlider;
    public Image _spikeIcon;
    public float _fillSpeed;
    

    private Rigidbody _rb;
    private Camera _mainCamera;
    private float _currentFOV;
    private bool isZoomingOut = false;
    private float _amountFillIcon = 0.01f; 

    private void Start()
    {
        _mainCamera = Camera.main;
        _currentFOV = _mainCamera.fieldOfView;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleSpikeForce();

        _spikeForceSlider.value = _spikeForce;
        if (isZoomingOut)
        {
            _currentFOV += _zoomSpeed * Time.deltaTime;
            _currentFOV = Mathf.Clamp(_currentFOV, _minFOV, _maxFOV);
            _currentCameraY += _rotateSpeedCameraY * Time.deltaTime;
            _currentCameraY = Mathf.Clamp(_currentCameraY, _maxCameraY, _minCameraY);
        }
        else
        {
            _currentFOV -= _zoomSpeed * Time.deltaTime;
            _currentFOV = Mathf.Clamp(_currentFOV, _minFOV, _maxFOV);
            _currentCameraY -= _rotateSpeedCameraY * Time.deltaTime;
            _currentCameraY = Mathf.Clamp(_currentCameraY, _maxCameraY, _minCameraY);
        }

        _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _currentFOV, Time.deltaTime * _zoomSpeed);
        Quaternion _newRotationY = Quaternion.Euler(0, _currentCameraY, 0);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _newRotationY, _rotateSpeedCameraY * Time.deltaTime);
        HandleIconFillAmount();
    }

    public void Rotate(float _deltaX)
    {
        transform.rotation = Quaternion.Euler(_deltaX, 0, 0);
    }
    public void SetSpiker(bool _spikeStatus)
    {
        isZoomingOut = _spikeStatus;
        _arrow.SetActive(_spikeStatus);
        _rb.isKinematic = _spikeStatus;
        inSpike = _spikeStatus;
        _amountFillIcon = 0.01f;
    }
    public void Spike()
    {
        SetSpiker(false);
        // Get the local forward direction of the GameObject
        Vector3 localForward = transform.forward;
        _rb.AddForce(localForward * _spikeForce, ForceMode.Impulse);
        _spikeForce = 0;
        _amountFillIcon = 0.01f;
    }
    private bool isIncreasing = true;
    void HandleSpikeForce()
    {
        // Check if _spikeForce is increasing or decreasing
        if (isIncreasing)
        {
            // Increase _spikeForce up to _maxSpikeForce
            _spikeForce += _spikeGrowthRate * Time.deltaTime;

            if (_spikeForce >= _maxSpikeForce)
            {
                isIncreasing = false;
            }
        }
        else
        {
            // Decrease _spikeForce down to zero
            _spikeForce -= _spikeGrowthRate * Time.deltaTime;

            if (_spikeForce <= 0)
            {
                isIncreasing = true;
            }
        }

        // Apply clamping using Mathf.Clamp to ensure _spikeForce stays between 0 and _maxSpikeForce
        _spikeForce = Mathf.Clamp(_spikeForce, 0, _maxSpikeForce);
    }
    void HandleIconFillAmount()
    {   if (!inSpike)
            {
            // Increase _amount by _fillSpeed * Time.deltaTime
            _amountFillIcon += _fillSpeed * Time.deltaTime;

            // Clamp _amount between 0 and 1 using Mathf.Clamp01
            _amountFillIcon = Mathf.Clamp01(_amountFillIcon);

            // Update the fillAmount of the _spikeIcon
            _spikeIcon.fillAmount = _amountFillIcon;
            if (_spikeIcon.fillAmount == 1)
            {
                _spikeIcon.GetComponent<Button>().interactable = true;
            }
            else
            {
                _spikeIcon.GetComponent<Button>().interactable = false;
            }
        }

    }
}
