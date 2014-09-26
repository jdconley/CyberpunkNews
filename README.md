Dev Prerequisite
===============
- Visual Studio 2013 (older visual studio might work, but not tested)
- SQL Server 2014 (older version is most likely to work)

Getting Started
===============
1. Acquire source code from github: https://github.com/analyst74/CyberpunkNews
2. Open CyberpunkNews/CyberpunkNews.sln with Visual Studio
3. Build the solution
4. create a new database for the app (optional if your user has admin privilege with the database)
5. open Web.config and update connection string to the database created above
6. build solution
7. open Package Manage Console, and run `update-database`
8. in Visual Studio, start debugging

You should see the website and play around with it now.
