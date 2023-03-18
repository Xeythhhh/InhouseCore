# Nginx
## Scope
Used nginx to reverse_proxy my apps from localhost to development domains and test CORS and SSL.

Initially I tried using caddy for this but I couldn't get it to work.
```
¯\_(ツ)_/¯
```
---
## Install
I used chocolatey but you can use brew or manually install.
> choco install nginx
---
## Shell
```ps1
//start nginx
start nginx

//force shutdown
nginx -s stop 

//graceful shutdown
nginx -s quit

//see nginx processes
tasklist /fi "imagename eq nginx.exe"

//validate configuration
nginx -t 
```
---
## Configuration
Edit hosts file
> "C:\Windows\System32\drivers\etc\hosts"
```
# ...removed for brevity

# localhost name resolution is handled within DNS itself.
#127.0.0.1      localhost
#::1            localhost

127.0.0.1       api.inhousecore.dev
127.0.0.1       app.inhousecore.dev
127.0.0.1       auth.inhousecore.dev
```
nginx config file can be found at
> '(your nginx install Path)\conf\nginx.conf'

with a default chocolatey installation it is found under
> C:\tools\nginx-<version>\conf\nginx.conf'

I used [mkcert]("https://github.com/FiloSottile/mkcert") to create the certificates and placed them in a '_debug' folder which is excluded from source control.
```nginx
# ...removed for brevity

# HTTPS server
    
    server {
        listen       443 ssl;
        server_name  app.inhousecore.dev;

        ssl_certificate_key  /Source/InhouseCore/_debug/app.inhousecore.dev-key.pem;
        ssl_certificate      /Source/InhouseCore/_debug/app.inhousecore.dev.pem;

        ssl_session_cache    shared:SSL:1m;
        ssl_session_timeout  5m;

        ssl_ciphers  HIGH:!aNULL:!MD5;
        ssl_prefer_server_ciphers  on;

        location / {
            #root   html;
            #index  index.html index.htm;
			proxy_pass https://localhost:7243;
        }
    }
    
# ...removed for brevity
```