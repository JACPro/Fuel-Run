# Fuel Run 

I was given the challenge of creating of creating an original Hypercasual mobile game for Android in just one week. I successfully created a game where you must steer a fast car to avoid traffic cones and collect fuel cans - which are vital for continuing and which contribute to your total score.
One of the bigger challenges with this project was optimising the game so it would comfortably have at least 30fps on lower end mobile devices. I was able to improve performance and hit this goal by making the following optimisations in Unity:

* Object pooling of gas icons as they are picked up
* Replace excessive water number of water tiles with a single water plane
* Deactivate pickups on collision and reactivate when restarting the level (rather than deleting and doing unnecessary reinstantiation)
* Baked lighting for static environment objects
* Turned off shadows
* Reducing max size of textures (and use crunch compression) where possible without losing visual quality
* Limit far render plane
* Switched material shaders to VertexLit mobile shaders where possible
* Disable raycast checking on non-interactable UI elements


___

<img src="https://github.com/JACPro/Fuel-Run/blob/main/Assets/Screenshots/fuelpickup.gif" title="Picking up Fuel" width="32%"></img>
<img src="https://github.com/JACPro/Fuel-Run/blob/main/Assets/Screenshots/fuelfinish.gif" title="Score Multipliers" width="32%"></img>
<img src="https://github.com/JACPro/Fuel-Run/blob/main/Assets/Screenshots/fuelfail.gif" title="Out of Fuel" width="32%"></img>
<img src="https://github.com/JACPro/Fuel-Run/blob/main/Assets/Screenshots/Start.png" title="Start Screen" width="32%"></img>
<img src="https://github.com/JACPro/Fuel-Run/blob/main/Assets/Screenshots/LevelEnd.png" title="Finish Line" width="32%"></img> 
<img src="https://github.com/JACPro/Fuel-Run/blob/main/Assets/Screenshots/Finish.png" title="Level Complete" width="32%"></img> 

___
#### To download the apk, [click here](https://github.com/JACPro/Fuel-Run/raw/main/FuelRun.apk)

#### To view a video demonstration, [click here](https://www.youtube.com/watch?v=8BiJZwPxs2s)
___
