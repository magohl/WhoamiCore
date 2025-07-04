# WhoamiCore
A minimal Asp.Net 9 application inspired by the Containous whoami test-application.

This is my go-to testrig to test Docker, Kubernetes, Azure AKS/ACA with ingress, gateway api and reverse proxies etc.

It returns information on the the request, platform and runtime environment.

Docker (linux) image
https://hub.docker.com/r/magohl/whoamicore

Usage
- ``docker run -p 8080:8080 ghcr.io/magohl/whoamicore:latest``
- Browse or curl to http://localhost:8080

![image](https://github.com/magohl/WhoamiCore/assets/1846780/0ebd708c-6b61-4898-8f65-b741713e2bf2)
