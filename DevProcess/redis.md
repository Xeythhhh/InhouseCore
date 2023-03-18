# Redis
## Scope
Used to share keys within the Identity implementation.

---
## Prerequisites
Requires [WSL](https://learn.microsoft.com/en-us/windows/wsl/install) (Windows Subsystem for Linux) on Windows.
```
//powershell 
wsl --install -d Ubuntu
//requires system restart
```
---
## Installation
This stuff can probably be automated with Nuke.
```
//powershell
wsl

curl -fsSL https://packages.redis.io/gpg | sudo gpg --dearmor -o /usr/share/keyrings/redis-archive-keyring.gpg
echo "deb [signed-by=/usr/share/keyrings/redis-archive-keyring.gpg] https://packages.redis.io/deb $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/redis.list
sudo apt-get update
sudo apt-get install redis
sudo service redis-server start
redis-cli 
ping //should reply with PONG if installation was successful
```
