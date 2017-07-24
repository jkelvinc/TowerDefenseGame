# TowerDefenseGame

This project has 1 level named Level_1

Enemy Units:
There are 4 different enemy units with the following varying properties:
- health
- movement speed
- currency they give when destroyed

Each enemy unit is composed of the following components
- FollowPath
Follows a series of PathNodes

- Unit
Checks when the Unit is dead by checking Health

- Health
Health of the Unit

- DestroyOnEnterCollider
Destroys a gameobject is entering a gameobject with the specified tag

- ChangeResourceAmount 
Specifies which resource to change and the amount

Towers:
There are 2 different towers with the following varying properties:
- damage
- attack range
- rate of fire

Each tower is composed of the following components
- AttackTarget
Attacks first enemy that enters its range

- Tower
Basic tower info such as Damage

- Cost
Specifies cost to create that tower

Bullets:
There are 2 types of bullets: Normal and Slow

Managers
- LevelManager
This manager looks at the Win/Lose conditions and starts/stops the game
It then triggers the proper event (GameWon, GameLost) based on those conditions

- ResourceManager
Manages the values for the different resources used in the game (Lives, Currency)

Winning/Losing  
A game is won is all WinConditions are fulfilled  
A game is lost if all LoseConditions are fulfilled  
WinConditions/LoseConditions extend GameCondition  
New Win/Lose Conditions can easily be created as long as they return the proper value of IsFullfilled

Map
The map is composed of 
- a series of PathNodes that make up a Path
- EnemySpawners that are linked to a specific Path

Messaging/Events  
Messages/Events are registered/unregistered from the Event Manager
All events can be found in GameEvents
There are currently 3 events: StartGame, GameWon and GameLost



Resources:
https://opengameart.org/content/tower-sprite-tower-defense-asset-for-2d-games

http://ask4asset.com/tower-defense-free-art-pack/


