# Documentation SLIDDES Networking

This file contains all the documentation for SLIDDES Networking. It might be incomplete so if you need any help you can reach out to SLIDDES at support@sliddes.com.

## WebGL / SLIDDES.Networking.WebGL

The WebGL files allow you to create and communicate with a Node.js server

### Setup 1. Project Build Folder
1. Make sure that you have switched your project to the WebGL platform.
2. (Optional) For faster performance, Player Settings > Other Settings, check the options: static batching, strip engine code, Logging > exception > none.
3. Build Settings > Build, save your folder at a quick access location.
4. Replace the index.html file with the file com.sliddes.networking > Runtime > WebGL > Server > public > index.html
5. (Optional) Take a look inside this file and replace what needs to be replaced with your info.

### Setup 2. Node.js Server
1. For a WebGL server we use Node.js and unfortunately have to code in javascript.
2. Download and install Node.js https://nodejs.org/en/download/
3. Execute Node.js in windows by typing "node.js command prompt" in the windows search bar.
4. If NodeJS was installed correctly you should see the message "Your environment has been set up for using Node.js" if not then good luck fixing it.
5. Copy the folder located at com.sliddes.networking > Runtime > WebGL > Server paste it at your desired location outside your Unity project.
6. Copy and paste your build files (from Setup 1.) inside Server > public (reminder that you have to use the index.html that came with the package instead of the generated one by Unity).
7. Select the Node.js command prompt and set the directory to that of where your Server folder (Step 5.) is located by typing (without quotation marks) "cd urlFolder". Example how it would look: 
"C:\Users\SLIDDES>cd C:\Users\SLIDDES\Documents\Server"
8. After hitting enter (and hopefully no errors) you have set the directory to your server folder. You will need to install some necessary modules.

### Setup 3. Node modules installation
1. After setting the Node.js command prompt location to your server folder type the following:
2. "npm install express" and hit enter, wait for completion of installation.
2. "npm install socketio" and hit enter, wait for completion of installation.
3. "npm install shortid" and hit enter, wait for completion of installation.
4. These node modules will reside in a new folder named node_modules inside your server folder.

### Setup 4. Executing Server
1. To execute the server type in the Node.js command prompt "node server.js".
2. You can then open a desired browser and type "localhost:3001" in the url to see your application.
3. To host your server you can use Google cloud, Azure, Amazon Aws, or Heroku.