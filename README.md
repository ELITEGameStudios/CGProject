# CGProject

**Deliverables**

New Content:





Updates:


----SHADER EXPLANATIONS----
"MATRIX GLOW"Step 2: The Shader takes the magnitude of the difference between the player's position in world space and the current position of the target pixel. This magnitude now representing distance is divided by a float property to give a customizable range of the effect. This range value is saturated and fed to a gradient map which determines the color and alpha of that pixel once multiplied by its sample texture. It is notable to mention that the player position is fed to the shader via a Unity script.

This is a visual effect that is meant to, first of all, fill the huge space that the monotone grid texture currently creates in the scene, while also being used as a theming tool to make the scene feel like it's in some sort of matrix/digital world. The color blue is chosen as it is a color commonly associated with modern computing technology. Additionally, I created arrow textured planes that follow this shader as well, as a means to guide the player through the level and also add more attention and purpose to this effect since now you dont see the arrows if you dont need to… a good practice in UI design.

GLITCH
The glitch effect is comprised of multiple passes doing the same thing. It first takes the view coordinates of the target pixel, then an offset is applied, as well as a solid color which represents the entire fragment of this shader. The offset that is applied is actually a set vector defined by the user, multiplied by a timer being passed into a sine function which animates this set offset into an oscillating back and forth value. This is then quantized to create a “snapping” motion that breaks the otherwise smooth sine animation. This process is done three times each with their own respective colors and offsets. The final pass however consists of a white center which barely vibrates as much as the others and is rendered on top via setting the Z coordinate of its view position to a high 0.7+, forcing its prioritized render.

This was made to also give off the vibe that you are in a computer made world. The fact that weapons glitch out when being inactive/reloading is almost as if its being pulled out and back in to the simulation. It is comprised of RGB colors to further signify this as those are the three most famous colors associated with digital color representation.

DEATH EFFECT
The death effect uses a different method of color correction rather than LUT’s to change the full screen. Instead, it simply takes the rendered image of the current frame and modifies the color data accordingly. While LUT’s are much more optimized for finalized effects, this method is more flexible to play around with and test different outcomes, or even animate the effect properties itself. In this case I simply inverted the colors by taking the value and applying 1 - x to its components. This was added because it served as a cool hitstop effect whenever you got hit. It is extremely noticeable and you can tell from this effect that something major happened or that you went wrong in your play somewhere.


Materials:
Find them in Assets/Materials

Models:
Find models in project file Assets/Models
 - Turret, Gun, Map

Slides Link: 

Video Link:

Contributions:
**Noah**

**Edward**


