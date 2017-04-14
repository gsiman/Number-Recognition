# Number-Recognition
Personal project that uses a neural network to identify single digits on a small 10 x 8 pixel matrix. The single digit can be any size or aspect ratio as long as it fits in the matrix. This project was made in Unity for quick prototyping.

# Interactable Demo
To try the demo you must have 'NumberRecognition.exe' and 'NumberRecognition_Data' in the same directory. The file 'playerInfo.dat' in the data directory is the trained weights used for the neural network. A backup of this file is included as 'playerInfo.dat.bac'.

# How To Use the Demo
The white canvas acts as the visible pixel matrix used as an input into the neural network. Click on the pixels to toggle them on (black) or off (white). After you have drawn your input digit you may click on any of the red buttons below a number to receive an output from the neural network. If you are training the neural network, it is important that you pick the correct answer to give proper feedback to the neural network. Below is a list of how the other button behave:

Save: Saves the current weights as the 'playerInfo.dat' file

Calibrate: Toggles whether or not the weights will be adjusted after each test. A green button will indicate that the calibration mode is active.

Reset Weights: Sets the values of the weights to the default values shown in the Reset class.
