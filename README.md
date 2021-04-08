# Usage

````sh

nuget sources remove -Name dev
nuget sources add -Name dev -Source Http://localhost:5000/packages
nuget push your-versionned.nupkg -ApiKey 15d0dda1-4028-4896-9f1a-188817da23f4 -Source http://localhost:5000/packages

````
