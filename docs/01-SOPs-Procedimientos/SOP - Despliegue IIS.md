---
type: sop
owner: Mikel
rev: 1
area: despliegue
tags:
---

# SOP: Despliegue IIS con HTTPS (ASP.NET Core / Blazor Server)

## Propósito
Publicar y ejecutar la aplicación **PRA-PCCS.Web** en **IIS (Windows)** sirviendo **HTTPS** de forma
## Alcance
Entornos Windows Server (2019/2022). No cubre Linux/Apache (otro SOP).
## Prerrequisitos
- - Acceso admin al servidor Windows.
- **Certificado TLS** válido instalado en `Local Computer > Personal > Certificates` (con clave privada).
- **.NET Hosting Bundle** del mismo major/minor que la app (recomendado .NET 8.x).
- Puertos abiertos en firewall (TCP **443** para HTTPS).
- Paquete de publicación de la app (carpeta `publish` generada por `dotnet publish`).
- ## Pasos
1.  Publicación en el equipo de build Restaurar y compilar:
   ```powershell
   dotnet restore .\src\PRA-PCCS.Web
   dotnet publish .\src\PRA-PCCS.Web -c Release -o .\publish
2. …
## Verificación
- …
## Rollback
- …
## Anexos
- [[ADR - ejemplo]]
