# WhoamiCore
A minimal Asp.Net 9 application inspired by the Containous whoami test-application.

This is my go-to testrig that i use to test Docker, Kubernetes, Docker swarm, Azure AKS, Container Apps, App Service with containers, reverse proxies etc.

It return information on the the request, platform and runtime environment.

Docker (linux) image
https://hub.docker.com/r/magohl/whoamicore

Usage
- ``docker run -p 8080:8080 magohl/whoamicore``
- Browse http://localhost:8080

![image](https://github.com/magohl/WhoamiCore/assets/1846780/0ebd708c-6b61-4898-8f65-b741713e2bf2)
