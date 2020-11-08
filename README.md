# AlphaToe
Tic Tac Toe-playing RL agent using TD-learning (C#)

Implemented Tic Tac Toe game and learning environment using a TD-learning agent. Model described in the first chapter of Reinforcement Learning: An Introduction.

TD-learning, or Temporal Difference Learning, is an algorithm used in Reinforcment Learning. TD-learning algorithms use experience to 'learn' a value function through trial and error. We can then use that value function to produce a policy, which is a model of behaviour, for the so called agent that is interacting with the environment.

The training part of this program starts out by enumerating all the possible states and filter out legal boards. After this process is completed we have a list with around 6000 states that we can hand over to the agent. Depending on if the agent plays as player one (O's) or player two (X's) all states will have an initial value of 0.5, except for states where the player wins which will have a value of 1.0, or if it's a loss it will get a value of -1.0. Draw states are set to 0.0. This list of states and values represent the agents policy. When the training process starts the agents will go at it against each other, picking the action(1-9) that will take it to the state that currently has the highest value. If there's a tie it will pick an action between the tied states at random. If however the move qualifes as an exploratory move(this happens with Probability EPSILON) the agent will pick between all possible actions at random regardless of state value. 

The learning happens by storing the previous state, the state before the agent made it's move, and before the opponent made it's move. And as it's the agents turn again, it updates the value of the state stored as previous state towards the value of the new state using the update rule: V(s) = V(s) + ALPHA*(V(s')-V(s)). Where V(s) is the value function for a given state s, and V(s') is the value function for the next state and ALPHA is a constant learning rate. The only times the update is not made is when a move has qualifed as an exploratory move, the agent will then skip the update step for that particular cycle. The name of the algorithm comes from this update rule as it updates the state using the difference between the next and previous state, the Temporal Difference, to learn the value function. 

Some data using constant ALPHA, EPSILON and 25 000 training episodes:
![Velocity plot](http://generalgames.org/AlphaToe/velocity.png)
The figure above plots the average recorded result-velocity 

![Openings distribution](https://generalgames.org/AlphaToe/opening_distribution.png)
The figure above plots the total distribution of openings by player one (5 being the middle tile)

The model is based on what is described in the first chapter of Reinforcement Learning: An Introduction, which is available for free as a pdf:
Book available as pdf: https://web.stanford.edu/class/psych209/Readings/SuttonBartoIPRLBook2ndEd.pdf 



