# PANG!
Created with Unity 2019.4.10f

Extra credits applied:
- **MVC pattern.** Not really reliant on the provided article, though did my best to separate logic from UI and data
(there are still some parts that I was not able to properly move apart from each other).

- **Levels with increasing difficulty.** Applied the approach where the game would build the level itself by detecting the level value.
In this case, the game will spawn only one ball for level 1, 2 for level 2, etc. The initial direction of the balls also switch depending
on their generation.

- **Custom visuals.** Applied animation for movement, a sprite for the bullet and the ball.

- **SFX and BGM.** Reused some of the existing SFX that were made available from CS50.

- **Leaderboards.** Got to include a simple view for the rankings, as well as the retro-style input of player name on game over.


Did not go with the two player option because I never got to make the `JoinGame()` from the new input system to work on my end,
and it also does not make sense when playing on mobile. But it was fun trying it.
