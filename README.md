# CS50x
 *Harvard University's introduction to the intellectual enterprises of computer science and the art of programming*
 
## Final Project: Desert Defense

#### Video Demo:  https://www.youtube.com/watch?v=hEbOJ6oNto4

### Description

#### Gameplay

My final project 'Desert Defense' is a computer game in the popular tower defense style. Enemies spawn in waves and run along a certain path. The player's goal is to build turrets which shoot automatically in order to kill the enemies before they reach the end. The difficult part is that the resources for building are limited.

#### Turrets
There are three kind of turrets:
- Basic turret
- Missile Launcher
- Laser Beamer

Each of them has different abilities and can be upgraded.

#### Enemies

There are three kind of enemies:
- Zombie (basic)
- Skeleton (tough)
- Bat (fast)

Each of them has different weaknesses and strenghts.

#### Lives / Money / Levels

The player has a certain amount of lifes which means the level failed if more enemies reached the end of the path.
The player has a certain amount of start money to build turrets. He gets money by killing enemies, by selling turrets (with loss) and automatically after every wave.
Both lifes and money vary from level to level.

Each level unlocks the following after completion.

#### Controls

The game is controlled entirely with the mouse. 'WASD' can be used for camera movement.


#### Scripts

- `AudioManager.cs` handles audio
- `BuildManager.cs` handles the selection of nodes and turrets in the shop for building
- `Bullet.cs` handles the movement of the turret bullets and calls when hitting enemies
- `CameraController.cs` handles the camera movement
- `CompleteLevel.cs` handles the unlocking of new levels after completion
- `DrawRadiusAround.cs` handles the turret attack range indicator
- `Enemy.cs` handles all enemy stats, taking damage, getting slowed and dying
- `Enemy.cs` handles the enemy movement along the path and life loss when coming through
- `GameManager.cs` handles the winning and losing of levels by displaying the corresponding UI
- `GameOver.cs` handles the 'Game Over' UI via which you can retry or go back to the main menu
- `LevelSelector.cs` handles the level selection menu, in particular, which levels to unlock
- `LivesUI.cs` handles the text displaying the amount of lives
- `MainMenu.cs` handles the main menu buttons
- `MoneyUI.cs` handles the text displaying the amount of money
- `Node.cs` handles building, upgrading and selling turrets and the node hovering animations
- `NodeUI.cs` handles the turret UI via which you can upgrade or sell turrets
- `PauseMenu.cs` handles the pause menu via which you can continue, retry or go to the main menu
- `PlayerStats.cs` handles the player's money, lives and amount of rounds survived
- `RoundsSurvived.cs` handles the animation of the number of rounds survived after the level
- `SceneFader.cs` handles the smooth transition between scenes
- `Shop.cs` handles which turret in the shop was selected
- `Sound.cs` handles single sound files for the audio manager
- `Turret.cs` handles the detection of the closest enemy, rotation of the head and shooting
- `TurretBlueprint.cs` handles which turret to pass to the build manager
- `Wave.cs` handles the type, number and spawn rate of enemy waves
- `WaveSpawner.cs` handles the enemy spawn, wave countdown and bonus gold after every wave
- `WaveSpawnerInfinite.cs` handles the same, but for the infinite mode
- `Waypoints.cs` handles the waypoints along the path for the enemy movement


#### How the game's made and what I learned

The game was made with the Unity Engine.

The foundation of the game is a tutorial by Youtuber [Brackeys](https://www.youtube.com/user/Brackeys). Big thanks to him.

Following his videos and reading lots of documentations and Stack Overflow, of course, I learned how to handle Unity. Along the way, I also learned C# and in particular, the advantages of object oriented programming.

I've extended the basic game with graphics, sounds and lots of functions, e.g. the attack range indicator, the game progress reset button or the infinite mode with enemies getting stronger every round.



#### Sources

Also thanks for providing free graphics and sounds:

'ikrosis' from [Sketchfab](https://sketchfab.com/3d-models/nether-portal-4d728f25a6404d64ae8ef7fb8e2d332f)  
'orbarts' from [Sketchfab](https://sketchfab.com/3d-models/desert-stone-ground-def353b3968947d496a51f3783cd6eaa)  
'Sahir Virmani' from [Sketchfab](https://sketchfab.com/3d-models/japanese-torii-gate-2027a248de1b4b70985ff97e708fb50d)  
'Pxltiger' from [Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232)  
'Teamjoker' from [Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy-monster-skeleton-35635)  
'amusedART' from [Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/free-monster-bat-158125)  

'Migfus20' from [Freesound](https://freesound.org/people/Migfus20/sounds/562604/)  
'mazk1985' from [Freesound](https://freesound.org/people/mazk1985/sounds/187406/)  
'kolekurt' from [Freesound](https://freesound.org/people/kolekurt/sounds/540152/)  
'newlocknew' from [Freesound](https://freesound.org/people/newlocknew/sounds/553518/)  
'Alex-Fullam' from [Freesound](https://freesound.org/people/Alex-Fullam/sounds/553906/)