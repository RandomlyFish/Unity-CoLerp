# Unity-CoLerp
___
## About
CoLerp is C# Class for Unity that is used to continuously interpolate between values. It's intended to be used to animate objects through code, however due it's simplicity it can be used for anything that needs to be interpolated. 

#### Why would you use CoLerp over SmoothDamp?

The main advantages with CoLerp over Unity's native [Mathf.SmoothDamp](https://docs.unity3d.com/ScriptReference/Mathf.SmoothDamp.html) is that the speed is controlled through a time value, meaning you can have a value interpolate from 0 to 1 in exactly 1 second if you want. Where as with smoothDamp you can only give it a speed value, meaning you can't determine how long an interpolation will actually take. On top of that CoLerp uses Unity's native AnimationCurve to affect the interpolation, which gives you a lot more control over how things interpolate, like adding a spring like effect. Where as with SmoothDamp you have no control over that.

Here's a video demonstrating it being used for both movement and rotation:

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/yHLHBVNWAQ8/0.jpg)](https://www.youtube.com/watch?v=yHLHBVNWAQ8)

___
## Installation

You can download the CoLerp.unitypackage file and import it into your Unity project. It contains CoLerp.cs and the example scenes shown in the video.

___
## Documentation


### Properties

**defaultCurve : AnimationCurve**
Returns default AnimationCurve used for interpolation, unless one is provided in the To method
___

**paused : bool** (Read Only)
Returns true if the interpolation is paused
___

### Constructor

**CoLerp**
Creates a new CoLerp with an initial value

&nbsp;&nbsp;&nbsp;&nbsp;**_value : float** 
&nbsp;&nbsp;&nbsp;&nbsp;The initial value for the CoLerp, if the CoLerp is used to move an object, this value should match the object's current position
___

### Public Methods

**To** : void
Sets the value to interpolate towards

&nbsp;&nbsp;&nbsp;&nbsp;**_value : float**
&nbsp;&nbsp;&nbsp;&nbsp;The value to interpolate towards

&nbsp;&nbsp;&nbsp;&nbsp;**_time : float**
&nbsp;&nbsp;&nbsp;&nbsp;The time in seconds it will take for the value to change from it's current value to the provided value

&nbsp;&nbsp;&nbsp;&nbsp;**_curve : AnimationCurve** (Optional)
&nbsp;&nbsp;&nbsp;&nbsp;The AnimationCurve used to change the way it interplates from it's current value to the provided value
&nbsp;&nbsp;&nbsp;&nbsp;If one isn't provided, then defaultCurve will be used instead
___

**Get : float**
Returns the current value
___

**Set** : void
Override the current value and remove interpolations

&nbsp;&nbsp;&nbsp;&nbsp;**_value : float**
&nbsp;&nbsp;&nbsp;&nbsp;The value to override the current value with
___

**Pause** : void
Pauses the interpolation
___

**Resume** : void
Resumes the interpolation
___________________________________________________________________________
