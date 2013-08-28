Keen Games
===========

Repository for COSC 4358 (UH Fall 2013). 

All the code and even the art can be uploaded here.

Joseph: testing

Sorry in advance for the file size! (380MB) but hey, at least you only have to download the bulk of that once

Controls
--------
Movement: WASD
Sneak: Shift
Use: z

Some features
-------------
-Sample avatars; enemies don't move yet. You can click on char_ethan and then open the Animator window to see how
it all works
-Enemies will detect player if you run around within hearing range
-They'll see you if you're within their field of view, and no objects are between you two
-Calculates path from enemy to player if you're within their sphere collider (you can see it in the Scene window)
-Lasers and security cameras will trigger alarm
-Doors open when you touch them
-Keycard can be picked up
-Xbox controller support. go ahead and give it a try

-We should be able to recycle some of the code, but some other parts are kinda clunky (doesn't detect xbox joystick sensitivity)
-Would like like to use a FOV mesh for enemies, rather than using a large sphere and calculating the angle