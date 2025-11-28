# CGProject

----SHADER EXPLANATIONS----
"MATRIX GLOW"Step 2: The Shader takes the magnitude of the difference between the player's position in world space and the current position of the target pixel. This magnitude now representing distance is divided by a float property to give a customizable range of the effect. This range value is saturated and fed to a gradient map which determines the color and alpha of that pixel once multiplied by its sample texture. It is notable to mention that the player position is fed to the shader via a Unity script.

This is a visual effect that is meant to, first of all, fill the huge space that the monotone grid texture currently creates in the scene, while also being used as a theming tool to make the scene feel like it's in some sort of matrix/digital world. The color blue is chosen as it is a color commonly associated with modern computing technology. Additionally, I created arrow textured planes that follow this shader as well, as a means to guide the player through the level and also add more attention and purpose to this effect since now you dont see the arrows if you dont need to… a good practice in UI design.

GLITCH
The glitch effect is comprised of multiple passes doing the same thing. It first takes the view coordinates of the target pixel, then an offset is applied, as well as a solid color which represents the entire fragment of this shader. The offset that is applied is actually a set vector defined by the user, multiplied by a timer being passed into a sine function which animates this set offset into an oscillating back and forth value. This is then quantized to create a “snapping” motion that breaks the otherwise smooth sine animation. This process is done three times each with their own respective colors and offsets. The final pass however consists of a white center which barely vibrates as much as the others and is rendered on top via setting the Z coordinate of its view position to a high 0.7+, forcing its prioritized render.

This was made to also give off the vibe that you are in a computer made world. The fact that weapons glitch out when being inactive/reloading is almost as if its being pulled out and back in to the simulation. It is comprised of RGB colors to further signify this as those are the three most famous colors associated with digital color representation.

DEATH EFFECT
The death effect uses a different method of color correction rather than LUT’s to change the full screen. Instead, it simply takes the rendered image of the current frame and modifies the color data accordingly. While LUT’s are much more optimized for finalized effects, this method is more flexible to play around with and test different outcomes, or even animate the effect properties itself. In this case I simply inverted the colors by taking the value and applying 1 - x to its components. This was added because it served as a cool hitstop effect whenever you got hit. It is extremely noticeable and you can tell from this effect that something major happened or that you went wrong in your play somewhere.

PORTAL EFFECT
The portal effect works by making use of the Twirl node on a UV to create a spiral effect, commonly associated with portals, along with the addition of Noise to give a more natural effect. Next, the portal was made dynamic by using a rotation node to rotate at a changeable speed, over a certain time. To achieve the distinct colour effect, I clamped a specific range within the center of the twirl to give the middle a different colour.

This effect was made to give a contrast to the static and colourless dynamic of the level. Which was implemented through the use of colour with high saturation to grab the player’s attention. This would in turn cause the player to move towards the effect and touch it as it is the only one of its kind within the level. This way, it could be used as a win condition after the player has reached the end of the level.

EXPLOSION EFFECT
The explosion effect for the Enemy Tars Bot makes use of the unity particle system by manipulating the particle orientation, lifetime, speed, and gravity. It uses a mesh instead of the base Lit unity particles to give it the blocky mesh. That material then has its own fresnel shader which is derived from the gun shader. This is what gives off the colour, emission, and transparency.

The addition of this effect is used to give a visual representation that signifies taking damage and also dying, This gives more clarity as to what happens when the enemy's health is depleted, while also leaving room for creative uniqueness.

INTRO ANIMATION EFFECT
The introduction animation effect implemented uses the extrude algorithm by using the extrude factor together with a start-up curve that moves with a gradient-like direction from a perspective where the applied faces of the map model are extruded outwards on their respective axis on start-up. Then, they are moved back to their normal state during run-time.

This effect is used to portray some narrative that the scene is one that the player is artificially put into, similar to that of a matrix. This way, the scene's atmosphere would give off a stronger digitally-crafted theme.




Slides Link: https://docs.google.com/presentation/d/1Q1m66QnYpIOGspP3NqCZ4AvMHSAIOewG8_UY9IWbdlU/edit?usp=sharing

Contributions:
**Noah**
Gun Animation & Bullet Particles
Turret Laser
Matrix Glow Hologram
Health+Death Effect
Glitch Effect
Intro Animation


**Edward**
Portal Effect
Explosion Effect
Tars Model & Texturing
Slides
ReadMe

