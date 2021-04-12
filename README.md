# Golf

This is a console based golf game. In the application, the user controls a golf player trying to get a golf ball to the cup at the end of a course. You do this by selecting the angle of the ball’s trajectory, and the force you put behind the swing. The game ends when the ball is in the cup. 

### Required Features:
* The player should be able to launch the golf ball from its initial location, with a given angle (degrees from the ground up) and velocity (m/s at the start of the ball’s arc). 
* The angle and velocity should determine how far the ball travels, reducing the distance between the starting location and the cup. 
* Each swing should move the starting location for each arc. 
* Gravity should affect the ball while it travels, which should be used to determine the arc of the ball. 
* The progress the player makes should be displayed between swings, showing the amount of swings the player has taken, as well as the distance to the cup. 
* If the ball travels beyond the cup, the new distance should still be positive. 
* When the ball has reached the goal, the game should end, displaying all swings taken, and how far the ball travelled each time. 
* If the ball moves too far away from the cup, the game should generate an exception that takes you out of the game loop, with a failure message. 
* If too many swings have been taken, the game should end, with a different failure message.

### Algorithms: 
* Angle In Radians: (Math.PI / 180) * angle 
* Distance: Math.Pow(velocity, 2) / GRAVITY * Math.Sin(2 * angleInRadians) 
* Gravity is equal to 9.8
Example: At 45 Degrees and 56 m/s, the ball should travel 320 meters. 

### Subjects Covered:
* Collections
* Exceptions
