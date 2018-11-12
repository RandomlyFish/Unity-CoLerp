using System.Collections.Generic;
using UnityEngine;

public class CoLerp {

    // Public Properties

    /// <summary>
    /// Returns the current value to interpolate towards
    /// </summary>
    public float targetValue { get; private set; }

    /// <summary>
    /// Returns true if it's currently interpolating, even if it's paused
    /// </summary>
    public bool interpolating { get; private set; }

    /// <summary>
    /// Returns true if the interpolation is paused
    /// </summary>
    public bool paused { get; private set; }

    /// <summary>
    /// The default AnimationCurve used for interpolation, unless one is provided in the To method
    /// </summary>
    public AnimationCurve defaultCurve {

        get {
            return DefaultCurve;
        }
        set {
            DefaultCurve = ClampCurve(value);
        }
    }

    private AnimationCurve DefaultCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    // Private Properties

    private float value;
    private float timePaused;
    private List<Lerp> lerps = new List<Lerp>();
    
    private class Lerp {

        public float from;
        public float to;
        public float toOffset;
        public float previous;
        public float startTime;
        public float duration;
        public AnimationCurve curve;
    }

    // Constructor

    /// <summary>
    /// Creates a new CoLerp with an initial value
    /// </summary>
    /// <param name="_value">The initial value for the CoLerp, if the CoLerp is used to move an object, this value should match the object's current position</param>
    public CoLerp (float _value) {

        value = _value;
        targetValue = _value;
        paused = false;
        interpolating = false;
    }

    // Public Methods

    // Base To method
    /// <summary>
    /// Sets the value to interpolate towards
    /// </summary>
    /// <param name="_value">The value to interpolate towards</param>
    /// <param name="_time">The time in seconds it will take for the value to change from it's current value to the provided value</param>
    /// <param name="_delay">The time in seconds it will take for the interpolation to start</param>
    /// <param name="_curve">The AnimationCurve used to change the way it interpolates from it's current value to the provided value</param>
	public void To (float _value, float _time, float _delay, AnimationCurve _curve) {

        if (targetValue == _value) {
            return;
        }

        Lerp lerp = new Lerp();

        lerp.from = value;
        lerp.to = _value;
        lerp.toOffset = 0;
        lerp.previous = value;
        lerp.startTime = Time.time + _delay;
        lerp.duration = _time;
        lerp.curve = ClampCurve(_curve);

        // Add an offset to account for ongoing interpolations
        foreach (Lerp currentLerp in lerps) {
            lerp.toOffset -= currentLerp.to + currentLerp.toOffset - currentLerp.previous;
        }

        lerps.Add(lerp);

        targetValue = _value;
        interpolating = true;
    }

    // To without delay
    /// <summary>
    /// Sets the value to interpolate towards
    /// </summary>
    /// <param name="_value">The value to interpolate towards</param>
    /// <param name="_time">The time it will take for the value to change from it's current value to the provided value</param>
    /// <param name = "_curve" > The AnimationCurve used to change the way it interpolates from it's current value to the provided value</param>
    public void To (float _value, float _time, AnimationCurve _curve) {
        To(_value, _time, 0f, _curve);
    }

    // To without AnimationCurve
    /// <summary>
    /// Sets the value to interpolate towards
    /// </summary>
    /// <param name="_value">The value to interpolate towards</param>
    /// <param name="_time">The time it will take for the value to change from it's current value to the provided value</param>
    /// <param name="_delay">The time in seconds it will take for the interpolation to start</param>
    public void To (float _value, float _time, float _delay) {
        To(_value, _time, _delay, defaultCurve);
    }

    // To without delay and AnimationCurve
    /// <summary>
    /// Sets the value to interpolate towards
    /// </summary>
    /// <param name="_value">The value to interpolate towards</param>
    /// <param name="_time">The time it will take for the value to change from it's current value to the provided value</param>
    public void To (float _value, float _time) {
        To(_value, _time, 0f, defaultCurve);
    }

    /// <summary>
    /// Returns the current value
    /// </summary>
    public float Get () {

        if (paused == true) {
            return value;
        }

        for (int i = 0; i < lerps.Count; i += 1) {

            float currentFromLerp = CalculateValueFromLerp(lerps[i], Time.time);

            value += currentFromLerp - lerps[i].previous; // Value is changed based on the difference between the previus and current value
            lerps[i].previous = currentFromLerp;

            // If the interpolation have reached the end, remove it
            if (Time.time >= lerps[i].startTime + lerps[i].duration) {
                
                if (lerps.Count == 1) {

                    if (Mathf.Approximately(value, lerps[0].to) == true) {
                        value = lerps[0].to;
                    }

                    interpolating = false;
                }

                lerps.RemoveAt(i);
                i -= 1;
            }
        }

        return value;
    }

    /// <summary>
    /// Override the current value and remove interpolations
    /// </summary>
    /// <param name="_value">The value to override the current value with</param>
    public void Set (float _value) {

        value = _value;
        lerps = new List<Lerp>();
        interpolating = false;
    }

    /// <summary>
    /// Pauses the interpolation
    /// </summary>
    public void Pause () {

        if (paused == true) {
            return;
        }

        timePaused = Time.time;

        paused = true;
    }

    /// <summary>
    /// Resumes the interpolation
    /// </summary>
    public void Resume () {

        if (paused == false) {
            return;
        }

        // Offsets the startTime of each lerp based on the the duration of the pause
        foreach (Lerp lerp in lerps) {

            if (lerp.startTime < timePaused) {
                lerp.startTime += Time.time - timePaused;
            } else {
                lerp.startTime = Time.time;
            }
        }

        paused = false;
    }

    // Private Methods

    /// <summary>
    /// Returns the value from the Lerp given the time
    /// </summary>
    /// <param name="_lerp">The Lerp</param>
    /// <param name="_time">The time in seconds to base the calculation off</param>
    /// <returns>The value from the Lerp given the time</returns>
    private float CalculateValueFromLerp (Lerp _lerp, float _time) {

        float progress = Mathf.Min(1f, (_time - _lerp.startTime) / _lerp.duration);
        return Mathf.LerpUnclamped(_lerp.from, _lerp.to + _lerp.toOffset, _lerp.curve.Evaluate(progress));
    }

    /// <summary>
    /// Clamp the provided curve so that it goes from 0 to 1 for both time and value
    /// </summary>
    internal AnimationCurve ClampCurve (AnimationCurve _curve) {

        int last = _curve.keys.Length - 1;

        // If the keys in the curve is already clamped, return it
        if (_curve.keys[0].time == 0f && _curve.keys[0].value == 0f && _curve.keys[last].time == 1f && _curve.keys[last].value == 1f) {
            return _curve;
        }

        if (_curve.keys[0].value == _curve.keys[last].value) {
            Debug.LogWarning("The provided curve uses the same value for both the first and last key in the curve which is not allowed. defaultCurve will be used instead");
            return defaultCurve;
        }

        // Clamp time and value of keys between 0 and 1
        float timeStart = _curve.keys[0].time;
        float valueStart = _curve.keys[0].value;
        float timeDifference = _curve.keys[last].time - timeStart;
        float valueDifference = _curve.keys[last].value - valueStart;

        for (int i = 0; i < _curve.keys.Length; i += 1) {

            Keyframe key = _curve.keys[i]; // _curve.keys[i] only returns the key which means it can't be modified directly, so it's stored as a new key

            key.time -= timeStart;
            key.time /= timeDifference;

            key.value -= valueStart;
            key.value /= valueDifference;

            _curve.MoveKey(i, key);
        }

        return _curve;
    }
}
