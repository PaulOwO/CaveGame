GPR920
Aidan/Paul

# CaveGame 
## The procedurally generated Top Down Shooter where you face ennemies within a cave, you must explore it to find the exit and to win the game

### Contexte and explication of the code
CaveGame is mainly inspired by games like enter the gungeon, and was made during the module GPR4300 which centered on procedural generation. it had the key objective of creating a cave to have a gameplay that differs from it. We tested the game nuclear throne which already uses this principle but we tried to move away from its brutal gameplay with the choice of the weapon of the revolver and its reloading. We have therefore chosen for cellular automata a formation with less open space. To mark the beginning and the end of the level going beyond the left and the right side is the start and the arrival in the form of prefab because it was the easiest and fastest way to create this two component. We also had the idea of doing it like in the enter the gungeon's style in that each region of our cellular automata is separated by corridors that close as the player passes through and make enemies appear in the area where the player arrives. But we had to abandon this idea because it would have been long and complicated to implement due to several factors : we would have had to make a bfs through the hotpath to determine the order of the rooms and therefore the direction in which the corridors are supposed to be taken. After that, each corridor should understand which region it belongs to in order to make the enemies appear. Since all this was too complicated for us, we decided to make the enemies appear randomly on the whole map, even the corridors to force the player to fight. To have a "bullet hell" feel to the game, both enemies in the game shoot at some point. To create a notion of choice for the player we put a fighter that follows the player and then explodes while dying to keep the player moving and a shooter that fills the room with bullets. The player has to decide which of the two is most important to eliminate. You can see from the diagram that the player has several choices: move to the right but risk taking damage from the hunter's explosion or have good timing to avoid the shooter's bullets.


### the problems that were encountered
A problem that we had throughout but that we were able to solve towards the end of the project is a problem of visibility. Indeed to display the player's life and bullets in the UI I had to reach other script variables, but putting them in public was not the solution. I learned the use of "sender" and "getteur" as well as "encapsulate field" which allow at the same time to make the code more readable and optimized. For example, my UI which was updated at each update is now updated when a change affecting it is called. Another big issue was getting the chaser to work properly, especially with the fact that multiple techniques were used to achieve the pathfidning we wanted for the AI, we started off with a steering entity and then opted to try A* to find the quickest path to the player itself. The pathfinding was somewhat successfull but would not work when paired with our cellular automata. We therefore opted for a more simple AI pathfinding and started using a radius to define when the chaser would detect the player. This was used via a circular trigger placed upon the chaser. However the box collider and ciricle collider ended up intertwining with each other when interacting with the bullet prefab we had made, which made things significantly harder, in the end we opted for a more simple approach that had much less to do with A* or pathfinding than we wished.

### here are a couple screenshots of the actual game itself in action showing the various things we decided to implement together :

![image](https://user-images.githubusercontent.com/71376109/121244613-5c5ebd00-c89f-11eb-9408-f466d0a89e7d.png)

![image](https://user-images.githubusercontent.com/71376109/121245000-cf683380-c89f-11eb-920d-00aebc776521.png)

![image](https://user-images.githubusercontent.com/71376109/121245034-d98a3200-c89f-11eb-9c62-6328f8cb14fc.png)

### and also have an early draft of what the game was to look like, and how we first started imagining it :
![image](https://user-images.githubusercontent.com/71376109/121247972-215e8880-c8a3-11eb-8d39-d0ac7dd3b993.png)





### the images above show the sprites of Finalbossblues which were used for all the sprites in the game except a couple others.

