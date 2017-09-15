# ExpressionMouse-Kinect
This project utilizes your face as Mouse control via the Kinect for Windows sensor (v1.0). It is written in C# using the Kinect for Windows SDK. Replace your mouse with your face. Control the cursor just by moving your head. Click by winking your eyes, scroll by raising and lowering your eyebrows.

## Installation ##

For usage and development you have to install the Kinect for Windows SDK v1.8 and the Kinect Developer Toolkit v1.8.
Download Kinect SDK: https://www.microsoft.com/en-us/download/details.aspx?id=40278
Download Kinect Toolkit: https://www.microsoft.com/en-us/download/details.aspx?id=40276

For detailled usage instructions please see my blog:
http://futuretechblog.com?p=71

Here is a short demo youtube video:
https://www.youtube.com/watch?v=6NFsea7CoxQ

Unfortunately I have currently no time to maintain this project. So if anybody who is interested to enhance the software (maybe maintain it for newer Kinect versions) is welcome.

## Usage ##

If you want to use ExpressionMouse without Dev environment, just use the exe file within the "executable" folder.


Cursor Moving

It is really easy: Just move your head to control the cursor. Make sure that Kinect can see your face as well as your chest. Sometimes the inital recognition is better when you are waving. It is normal that Kinect needs a few seconds to identify your face correctly. In contrast to KinectMouse, ExpressionMouse Kinect is more precise when you are more close to the sensor (but not too close) as the sensor has a more detailed view on your face this way. One meter should be a sufficient distance.

Left Click

Just wink with your right eye about a second. At this point you may ask why you have to use your right eye for a left click and not your left eye. During testing we figured out that the sensor detected our right eyes much more accurate than our left eyes. So we decided to swap left and right. As left clicks are much more frequent than right clicks, we think it is a good idea to use the most sensitive wink for the left click.

Right Click

Just wink with your left eye about a second.

Double Click

Wink with both of your eyes at the same time. Then a double click will be executed.

Scrolling

Raise your eyebrows for scrolling up and lower it for scrolling down.

Drag & Drop

Open your mouth for starting drag & drop. Move your head to move the cursor and keep your mouth open. For dropping, just close your mouth.

Find the correct settings for yourself

Every face is different. It could be that the preselected settings in ExpressionMouse Kinect are not optimal for you. Just play a bit with the thresholds until you are satisfied.

ClickDelay: Timespan in frames (Normally Kinect works with 30 fps) which have to elapse between two mouse actions.
Headrotation Smoothing Filter Values: Frameweights for calculating weighted average of your head rotation. Used for smoothing the cursor motion. If you enter the following the cursor will become more precise, but it will also a bit more delayed: “2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1”

Percentage of horizontal edge Pixels: Used for differentiating between open and closed eye. A higher value means that the closed eye detection is less sensitive. A lower value makes it more sensitive.

Used frames for closed eye detection: More frames increases accuracy in closed eye detection, but it also increases the timespan between closing the eye and the execution of the mouseclick.

Eye closed filter threshold: Used for differentiating between open and closed eye. A higher value means that the closed eye detection is less sensitive. A lower value makes it more sensitive.

Double click second eye threshold: Threshold for differentiating between a normal click and a double click. If doubleclicks are not recognized correctly, you should decrease this value.  If normal clicks are recognized as doubleclicks, you should increase this value.
Brow raiser start threshold:  Threshold for raising your brow. Decrease value, if raising your brow is not recognized. If your computer is scrolling up, even if you are not raising your brow, increase this value.

Brow lowerer start threshold: Same as Brow raiser start threshold, but for lowering your brow and scrolling down.

Mouth open start threshold: Threshold for opening your mouth (executing MouseDown Event). Increase if opening your mouth is not recognized correctly. Decrease if MouseDown is executed, even if your mouth is closed.

Mouth open confirmation: Decrease this value, if MouseUp is executed, even if your mouth is still open.

Mouth open end threshold: Threshold for closing your mouth (executing MouseUp Event). Increase if closing your mouth is not recognized correctly. Decrease if MouseUp is executed, even if your mouth is still open.

Scroll multiplier up: A higher value means that scrolling up is faster.

Scroll multiplier down: A higher value means that scrolling down is faster.

Head to Screen relation X – Width: Sensitivity of the mouse cursor in horizontal direction. A higher value means less sensitivity.

Head to Screen relation Y – Height: Sensitivity of the mouse cursor in vertical direction. A higher value means less sensitivity.
