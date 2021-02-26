
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class SpiniWheel : UdonSharpBehaviour
{
    private bool _isSpining;
    private float _spinTime;
    public float speed;
    public float minSpintime;
    public Transform pivot;
    public Text[] _tasks;
    public Text[] _inputs;
    [UdonSynced] private float syncedTime;
    [UdonSynced] private bool syncedNewTime;
    [UdonSynced] private Quaternion syncedRotation;
    [UdonSynced] string task0 = "Task 1";
    [UdonSynced] string task1 = "Task 2";
    [UdonSynced] string task2 = "Task 3";
    [UdonSynced] string task3 = "Task 4";
    [UdonSynced] string task4 = "Task 5";
    [UdonSynced] string task5 = "Task 6";
    [UdonSynced] string task6 = "Task 7";
    [UdonSynced] string task7 = "Task 8";
    [UdonSynced] int _textCounterSynced;
    private int _textCounter;
    private float resetTime;

    private void Update()
    {
        if (_spinTime >= 0)
        {
            pivot.Rotate(new Vector3(0, speed, 0));
            _spinTime -= Time.deltaTime;
        }
        else 
        { 
            _isSpining = false;
            syncedRotation = pivot.rotation;
        }
        if (resetTime <= 0)
        {
            ResetSynced();
        }
        else resetTime -= Time.deltaTime;
    }
    public void StartSpin()
    {
        Debug.Log(Networking.LocalPlayer.GetHashCode());
        if (!Networking.IsOwner(this.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            Debug.Log("Owner changed");
            Debug.Log(Networking.GetOwner(gameObject).GetHashCode());
        }
        if (!_isSpining) 
        {
            float rand = Random.Range(0, 5);
            _spinTime = minSpintime + rand;
            syncedTime = _spinTime;
            syncedNewTime = true;
            _isSpining = true;
            resetTime = 1.0f;
        }
    }
    public override void OnDeserialization()
    {
        if (syncedNewTime && !_isSpining)
        {
            Debug.Log("Deserialadingsta");
            _isSpining = true;
            _spinTime = syncedTime;
        }
        else if(!_isSpining && pivot.rotation != syncedRotation)
        {
            pivot.rotation = syncedRotation;
        }
        if(_textCounter != _textCounterSynced)
        {
            _textCounter = _textCounterSynced;
            UpdateLokalText();
        }
    }
    private void ResetSynced()
    {
        syncedTime = 0;
        syncedNewTime = false;
    }
    public void UpdateLokalText()
    {
        _tasks[0].text = task0;
        _tasks[1].text = task1;
        _tasks[2].text = task2;
        _tasks[3].text = task3;
        _tasks[4].text = task4;
        _tasks[5].text = task5;
        _tasks[6].text = task6;
        _tasks[7].text = task7;
    }
    public void UpdateSyncedText()
    {
        if (Networking.IsOwner(gameObject))
        {
            if(_inputs[0].text != "" && _inputs[0] != null)
                task0 = _inputs[0].text;
            if (_inputs[1].text != "" && _inputs[1] != null)
                task1 = _inputs[1].text;
            if (_inputs[2].text != "" && _inputs[2] != null)
                task2 = _inputs[2].text;
            if (_inputs[3].text != "" && _inputs[3] != null)
                task3 = _inputs[3].text;
            if (_inputs[4].text != "" && _inputs[4] != null)
                task4 = _inputs[4].text;
            if (_inputs[5].text != "" && _inputs[5] != null)
                task5 = _inputs[5].text;
            if (_inputs[6].text != "" && _inputs[6] != null)
                task6 = _inputs[6].text;
            if (_inputs[7].text != "" && _inputs[7] != null)
                task7 = _inputs[7].text;
            _textCounterSynced++;
             UpdateLokalText();
        }
    }
    
}
