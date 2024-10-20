# Flocking and Rope Testbed

This is a testbed implementation simulating AI flocking behaviour and rope physics.


## Table of Contents

- [Requirements](https://github.com/AndyDoanSAE/Flocking-and-Rope-GPG213/?tab=readme-ov-file#requirements)
- [Installation](https://github.com/AndyDoanSAE/Flocking-and-Rope-GPG213/?tab=readme-ov-file#installation)
- [Flocking Guide](https://github.com/AndyDoanSAE/Flocking-and-Rope-GPG213/?tab=readme-ov-file#flocking-guide)
- [Rope Guide](https://github.com/AndyDoanSAE/Flocking-and-Rope-GPG213/?tab=readme-ov-file#rope-guide)
- [Maintainers](https://github.com/AndyDoanSAE/Flocking-and-Rope-GPG213/?tab=readme-ov-file#maintainers)


## Requirements
This project requires Unity Editor version 2022.3.16f1.


## Installation
- Clone the repository OR download the project as a zip file and extract it
- Open the project with Unity Hub


## Flocking Guide

- Flocking assets can be found under Assets > Flock Assets
- The test scene for the flocking simulation can be found in Assets > Flock Assets > Scenes > Boid Test Scene
- Boid behaviour can be customised through the **BoidManager** game object

### Customisable Values
> **World Size (X, Y)** - Sets the size of the world for the boids to navigate through

> **Edge (Wrap, Collide)** - Changes the behaviour of the boid when it reaches the edge of the world
- Wrap - Boids wrap around to the opposite side
- Collide - Boids collide against the edge

> **Boid Count** - Sets the number of boids to be displayed

> **Boid Speed** - Sets the speed of the boid

> **Boid Sight Range** - Sets the range for how far the boid can see other boids

> **Boid Enable Separation** - Toggles separation behaviour

> **Boid Enable Cohesion** - Toggles cohesion behaviour

> **Boid Enable Alignment** - Toggles alignment behaviour

> **Boid Strength Separation** - Adjusts the strength of separation behaviour

> **Boid Strength Cohesiion** - Adjusts the strength of cohesion behaviour

> **Boid Strength Alignment** - Adjusts the strength of alignment behaviour

> **Debug Ranges** - Toggle to show visualisation of the boid sight range in scene view

> **Debug Nearby** - TOggle to show visualisation of which boid is considered nearby to this boid


## Rope Guide

- Rope assets can be found under Assets > Rope Assets
- The test scene for the rope simulation can be found in Assets > Rope Assets > Scenes > Rope Test Scene
- Rope behaviour can be customised through the **Rope Manager** game object

### Customisable Values

> **Cycles** - The number of times the rope is simulated

> **Num of Nodes** - The number of nodes the rope has

> **Spacing** - Spacing between each nodes

> **Gravity** - Gravity force

> **Fixed Origin** - Fixes the origin point of the rope


## Maintainers
- Andy Doan - [AndyDoanSAE](https://github.com/AndyDoanSAE)
