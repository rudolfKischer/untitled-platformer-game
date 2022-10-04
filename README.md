# untitled-platformer-game
2D Unity platformer project


# Setup
 * *install instructions go here*
   * *Install the editor VS Code [here](https://code.visualstudio.com/download), Select proper download for machine*
   * *get the git windows installation [here](https://gitforwindows.org), used for version control and more universal shell commands*
   * *fix line endings ```git config --global core.autocrlf true```*
   * *windows install Choclatey, choclatey is an installation manager*
     1. Run powerShell as administrator
     2. Enter the command `Get-ExecutionPolicy`
     3. If `Restricted` is returned than run `Set-ExecutionPolicy AllSigned`
     4. To install choclatey run the command 
      ```
      Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
      ```
     5. go [here](https://chocolatey.org/install) for more info
   
   * *Github command line interface installation*
     1. Run powershell or VS Code as administrator. Instructions to do so permanently for VS Code can be found [here](https://stackoverflow.com/questions/37700536/visual-studio-code-terminal-how-to-run-a-command-with-administrator-rights)
     2. Run `choco install gh`
     3. run `choco upgrade gh`
     4. Login to github with  `gh auth login`. When prompted select:
        1.  `github.com`
        2.  `HTTPS`
        3.  authenticate git with credentials `Y`
        4.  `Login with a web browser`, copy the code given and proceed to website
        5.  Login with git hub credentials
        6.  Login to git with email `git config --global user.email "<yourEmail@email.site>"`
   * *large file storage (LFS)*
     * LFS is used to manage large files like images, audio, models and video.
     * This is because the way git manages changes is not ideal for large file types or binaries
     * install with `git lfs install`
     * Before pushing a large file, ensure its file type is in the `.gitattributes` file
   * *Unity Unity and Unity*
     * install unity hub [here](https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe)
     * through unity hub install version 202.3.11f1 
 * *clone instructions go here*
 * *permission instructions go here*
 
# Contributing
 * *contributing instructions go here*
 * *issue instructions go here*
# Description
 * *info about the game*
### Authors:
 * *Rudolf C. Kischer*
 * *Dayton Bowen*
