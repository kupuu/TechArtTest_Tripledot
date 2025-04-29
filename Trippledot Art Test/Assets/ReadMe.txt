Tech art test - Tom Berridge

Test considerations.

PopUp blur background:
I wanted to focus on implementing something modular and performant. My intentions were to
1. Capture the camera's output at a much lower resolution into a render texture
2. Use a Blit pass to blur this render texture only once
3. Apply this render texture to a RawImage component
4. Animate it's alpha property to fade it in.

To try keep the implementation of PopUpBackgroundBlur.cs modular, it is event driven and subscribing to customStateBehaviours in the PopUp animator, not directly referenced.
I would need to ensure every popup in the scene isn't responding every time another popup is hitting those animation states though. Disabling the object when not in use.

I would be my ultimate intention for this to act as a standalone "layer" which can be dropped into other popUps where required, needing no further assignments in animation clips.
The Raw Image component is currently referenced in the animationClips. I would want to try removing this coupling,hoping to create an alternative way of grabbing
a duration, start and end colours and a blur amount and then applying it all internally.


