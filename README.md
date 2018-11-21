# Unity-CoLerp

## About
CoLerp is C# Class for Unity that is used to continuously interpolate between values. It's intended to be used to animate objects through code, however due it's simplicity it can be used for anything that needs to be interpolated. 

#### Why would you use CoLerp over SmoothDamp?

The main advantages with CoLerp over Unity's native [Mathf.SmoothDamp](https://docs.unity3d.com/ScriptReference/Mathf.SmoothDamp.html) is that the speed is controlled through a time value, meaning you can have a value interpolate from 0 to 1 in exactly 1 second if you want. Where as with smoothDamp you can only give it a speed value, meaning you can't determine how long an interpolation will actually take. On top of that CoLerp uses Unity's native AnimationCurve to affect the interpolation, which gives you a lot more control over how things interpolate, like adding a spring like effect. Where as with SmoothDamp you have no control over that.

Here's a video demonstrating it being used for both movement and rotation:

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/yHLHBVNWAQ8/0.jpg)](https://www.youtube.com/watch?v=yHLHBVNWAQ8)


## Installation

You can find CoLerp on the [Asset Store](https://assetstore.unity.com/packages/tools/animation/colerp-132979), or alternatively download the [CoLerp.unitypackage](https://github.com/RandomlyFish/Unity-CoLerp/raw/master/CoLerp.unitypackage) file and import it into your Unity project. It contains CoLerp.cs and the example scenes shown in the video.


## Documentation

[Documentation PDF](https://github.com/RandomlyFish/Unity-CoLerp/blob/master/Documentation.pdf)

[Example Script](https://github.com/RandomlyFish/Unity-CoLerp/blob/master/CoLerpSimpleExample.cs)
