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

    public Camera _mainCamera;
    public float _maxFOV = 90f;
    public float _minFOV = 60f;
    public float _zoomSpeed = 30f;
    public bool inSpike;
    public float _maxSpikeForce = 0f;
    public float _spikeGrowthRate;
    public float _spikeForce = 0;
    public GameObject _arrow;
    public Slider _spikeForceSlider;
    public Image _spikeIcon;
    public float _fillSpeed;
    

    private Rigidbody _rb;
    private float _currentFOV;
    private bool isZoomingIn = false;
    private bool isZoomingOut = false;
    private float _amount = 0.01f;

    private void Start()
    {
        _currentFOV = _mainCamera.fieldOfView;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleSpikeForce();

        _spikeForceSlider.value = _spikeForce;
        /*if (isZoomingIn)
        {
            _currentFOV -= _zoomSpeed * Time.deltaTime;
            _currentFOV = Mathf.Clamp(_currentFOV, _minFOV, _maxFOV);
        }*/
        if (isZoomingOut)
        {
            _currentFOV += _zoomSpeed * Time.deltaTime;
            _currentFOV = Mathf.Clamp(_currentFOV, _minFOV, _maxFOV);
        }
        else
        {
            _currentFOV -= _zoomSpeed * Time.deltaTime;
            _currentFOV = Mathf.Clamp(_currentFOV, _minFOV, _maxFOV);
        }

        _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _currentFOV, Time.deltaTime * _zoomSpeed);

        HandleIconFillAmount();
    }

    public void Rotate(float _deltaX)
    {
        transform.rotation = Quaternion.Euler(_deltaX, 0, 0);
    }
    public void SetSpiker(bool _spikeStatus)
    {
        isZoomingIn &= !_spikeStatus;
        isZoomingOut = _spikeStatus;
        _arrow.SetActive(_spikeStatus);
        _rb.isKinematic = _spikeStatus;
        _amount = 0.01f;
        inSpike = _spikeStatus;
    }
    public void Spike()
    {
        SetSpiker(false);
        _rb.AddForce(Vector3.forward * _spikeForce, ForceMode.Impulse);
        _spikeForce = 0;
        _amount = 0.01f;
    }
    private bool isIncreasing = true;
    void HandleSpikeForce()
    {
        // Check if _spikeForce is increasing or decreasing
        if (isIncreasing)
        {
            // Increase _spikeForce up to _maxSpikeForce
            _spikeForce += _spikeGrowthRate * Time.deltaTime;

            // Check if it has reached or exceeded _maxSpikeForce
            if (_spikeForce >= _maxSpikeForce)
            {
                // If it has reached or exceeded, switch the direction to decreasing
                isIncreasing = false;
            }
        }
        else
        {
            // Decrease _spikeForce down to zero
            _spikeForce -= _spikeGrowthRate * Time.deltaTime;

            // Check if it has reached zero
            if (_spikeForce <= 0)
            {
                // If it has reached zero, switch the direction to increasing
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
            _amount += _fillSpeed * Time.deltaTime;

            // Clamp _amount between 0 and 1 using Mathf.Clamp01
            _amount = Mathf.Clamp01(_amount);

            // Update the fillAmount of the _spikeIcon
            _spikeIcon.fillAmount = _amount;
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
