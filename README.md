black-friday
============

# About

Black Friday is a solo-project created for my Interactive Media Development
class at RIT. It covers the implementation of several different steering
behaviors which I have listed below.


In Black Friday you are a security guard safe within the comfort of your
own office. You notice shoppers storming around the store and feel the need
to release the security roombas from the shelves. The roombas wander around
the store and select a leader whom will take them on their pursuit to slow
down the shoppers. 

## A-Star Pathfinding

Two randomly colored spheres move around the map in search of target hotspots
for where the sales would be located in the store. They use A-Star pathfinding
in order to find the ideal path to take throughout the store in order to reach
their goal.

## Leader Following

Once the "Security Roombas" are activated they will randomly select a leader.
The leader will steer the herd of roombas throughout the map. The leader also
has a more powerful force a separation than it's followers. This allows
for the leader to make more impactful decisions on the direction that the 
herd travels in.

## Raycast Avoidance

All of the autonomous agents in the scene use raycast avoidance to prevent
collisions with other physics objects in the game world.

## Wandering

Until the roombas have selected a leader they will all wander among the world.

** Note this is only true if the roombas have been activated by the user **

Once a leader is selected it will remain the only wanderer left in the scene.
The wandering is based off of a markov-chain like strategy where there are
percentage values attributed for turning different directions in relation
to the current direction of the wanderer.


# Contols

WASD to move around

E to (De)Activate the roombas
