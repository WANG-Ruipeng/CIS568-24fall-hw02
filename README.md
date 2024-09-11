# Develop Log-Alpha Version

## Demo


https://github.com/user-attachments/assets/972a3d90-e171-4d5a-b9ff-ce7aa0695f40


## Development Log

### **Destructible Scene Objects**

I began by focusing on creating destructible objects with a particle-based feel. Initially, I explored dynamic scene destruction, where walls would reflect real-time damage based on player actions. However, after further investigation, I realized that implementing fully dynamic destruction would be too complex for a small demo. It also seemed unnecessary for a gameplay prototype.

To streamline the process, I decided to pre-bake the destructible objects into small particles. When the player interacts with these objects, the particles are given a slight initial velocity, simulating a "breaking" effect. This approach effectively conveys the sense of destruction without overcomplicating the system.

### **Control System**

At first, I developed the 'Crash' and 'Control' abilities to use different inputs. However, this approach quickly proved computationally expensive. With over a thousand particles in the scene, iterating through the particle list every frame caused significant lag, making the game unplayable.

To solve this, I adjusted the control mechanic to apply forces to particles only when they are hit, rather than continuously updating them every frame. After playtesting, this change resulted in a more intuitive experience for players, allowing both 'Crash' and 'Control' to be handled elegantly with the same key input.

### **Cube Detector Function**

After refining the core abilities, I implemented a cube detector function that automatically counts the number of small particles within specified areas. Fortunately, there were no performance issues, and the system works smoothly.

### Future Plan for Next Development Cycle

- Enemy: AI, model, health bar
- Trying with more shape of the destructible

# Crash and Control Explained

## High Concept

“Crash and Control” is a first-person action-puzzle game where players wields their abilities to win. Using two unique abilities, players can crash parts of the scene into particles and then control these particles to solve puzzles, defeat enemies, and reshape the world around them.

## Genre

First-Person Action-Puzzle Game

## **Player Challenge/Motivation**

Players will be challenged to use their abilities to navigate through levels filled with environmental puzzles and hostile enemies. The main motivation is to master the dual abilities of ‘Crash’ and ‘Control’ to defeat opponents, overcome obstacles, and progress through increasingly complex scenarios. Since players cannot use the ‘Crash’ and ‘Control’ ability at the same time, players may need to make careful decision for how to use their ability: to crash the walls into particles and control the particles to hit the enemy, or just use the crash ability to crash enemies.

## **Design Goals**

•	**Empowerment:** Give players a sense of power by allowing them to manipulate the environment in ways that are both destructive and creative. The game should make players feel like they are in control of a powerful force.

•	**Strategy:** Create scenarios where players must think critically about how to use their abilities. The game should encourage experimentation with particle manipulation and exploration on how to use their abilities to find the best solutions.

•	**Satisfying Combat:** Make the combat system engaging by allowing players to use particles in various offensive and defensive ways. We would offer multiple approaches to tackle enemies.

## Feature

•	**Dynamic Destruction:** Players can destroy specific parts of the environment, breaking them down into particles that can be controlled and used in various ways.

•	**Dual Abilities:**

•	**Crash:** Emit lightballs that cause damage to enemies and break parts of the environment into particles.

•	**Control:** Guide and manipulate particles to solve puzzles, attack enemies, or interact with the environment.

•	**Puzzle Integration:** Levels are designed with puzzles that require creative use of particles, such as moving particles onto pressure plates or using them to block hazards.

•	**Combat Mechanics:** Use particles to strike enemies, create barriers, or push enemies out of arenas. The combat system allows for strategic and varied approaches.

•	**Interactive Environment:** The game world is reactive, with destructible environments and objects that can be manipulated using particle control.
