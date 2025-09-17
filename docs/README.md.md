# PRA-PCCS · Handbook (Vault Obsidian)

Este vault contiene **procedimientos (SOPs)**, **runbooks**, **decisiones de arquitectura (ADR)** y notas operativas del proyecto.  
Está versionado en **Git** y visible en Visual Studio mediante el proyecto `docs/Docs.csproj`.

---

## 1) Cómo abrir este vault
- En **Obsidian**: *Open folder as vault* → selecciona la carpeta `docs/`.
- Confirma *Trust this vault* si te lo pide.
- (Opcional) Cambia a **Dark theme** en *Settings → Appearance*.

---

## 2) Estructura de carpetas

docs/  
00-MOC/ # Índices/portadas (Map of Content)  
01-SOPs/ # Procedimientos paso a paso (operación/despliegue)  
02-Runbooks/ # Guías de diagnóstico/incident response  
03-Plantillas/ # Templates (SOP, Runbook, ADR, Reunión)  
04-ADRs/ # Architecture Decision Records  
05-Decisiones/ # Línea temporal de decisiones  
10-Notas/ # Reuniones, ideas, miscelánea

---

## 3) Plantillas (Templates)

Activa **Templates**: *Settings → Core plugins → Templates*  
Configura *Settings → Templates*:
- **Template folder**: `03-Plantillas`
- **Date format**: `YYYY-MM-DD` (recomendado)

Plantillas disponibles:
- `SOP - plantilla`
- `Runbook - plantilla`
- `ADR - plantilla`
- `Reunión - plantilla`

**Usar una plantilla**
1. Crea una nota en la carpeta destino (p. ej., `01-SOPs/`).
2. Renómbrala (ej.: `SOP - Despliegue IIS`).
3. `Ctrl+P` → **Insert template** → elige la plantilla.

---

## 4) Convenciones

**Nomenclatura**
- SOPs: `SOP - <Tema>`
- Runbooks: `Runbook - <Tema>`
- ADRs: `ADR - <Decisión>`
- Reuniones: `YYYY-MM-DD - Reunión - <Tema>`

**Front-matter mínimo**
```yaml
# SOP
type: sop
owner: <responsable>
rev: 1
tags: [sop, <área>]

# Runbook
type: runbook
sev: P2
system: PRA-PCCS
tags: [runbook, <área>]

# ADR
type: adr
status: Accepted
date: YYYY-MM-DD
tags: [adr, arquitectura] ```
**Enlaces**

- Usa `[[Nombre de nota]]` para crear **backlinks**.
    
- Desde un SOP, enlaza ADRs relevantes y runbooks asociados.
    

**Bloques de código**

` ```powershell dotnet publish ./src/PRA-PCCS.Web -c Release -o ./publish ``` `

---

## 5) ÍNDICES y registro

- Portada: `00-MOC/Índice.md`
    
- Timeline: `05-Decisiones/Registro de decisiones.md` (añade cada ADR con fecha y resumen).
    

---

## 6) Versionado (Git)

- Cambios en `docs/` se **commitean** junto al código.
    
- `.gitignore` recomendado (ya incluido):
    
    `docs/.obsidian/workspace.json docs/bin/ docs/obj/`
    
- Mensajes de commit sugeridos:
    
    - `docs: add SOP despliegue IIS`
        
    - `adr: elegir arquitectura por capas`
        

_(Opcional)_ Plugin **Obsidian Git** para auto-commit/push desde Obsidian si te resulta cómodo.

---

## 7) Compartir / Exportar

- **PDF**: _File → Export to PDF_ para compartir un SOP/ADR con terceros.
    
- **Portal**: puedes subir PDFs a Google Drive o un Google Site si necesitas un “escaparate”.
    

---

## 8) Búsquedas y (opcional) Dataview

Si instalas el plugin **Dataview**, puedes crear listados automáticos:

**Últimos ADR aceptados**

`TABLE date AS Fecha FROM "04-ADRs" WHERE type = "adr" AND status = "Accepted" SORT date DESC LIMIT 10`

**SOPs por área**

`LIST FROM "01-SOPs" WHERE contains(tags, "sop") GROUP BY choice(length(filter(tags, (t) => t != "sop")) > 0, filter(tags, (t) => t != "sop")[0], "general")`

---

## 9) Mantenimiento y durabilidad (10+ años)

- Mantén **SOPs** y **Runbooks** al día tras cada cambio operativo.
    
- Registra decisiones en **ADRs** y enlázalas desde SOPs afectados.
    
- Haz **backup** periódico del repo (incluye `docs/`).
    
- Conserva instaladores offline del **SDK .NET** que uses y considera un **snapshot** de paquetes NuGet (ver ADR y SOPs de despliegue).
    

---

## 10) Primeros pasos

1. Lee `ADR - Arquitectura por capas (v2)`.
    
2. Revisa `SOP - Despliegue IIS con HTTPS`.
    
3. Crea `Runbook - Diagnóstico arranque IIS`.
    
4. Añade a `Registro de decisiones` cualquier ADR nuevo.
    

---

**Contacto / dudas:** añade comentarios directamente en la nota o crea una tarea en el backlog del proyecto.

``---  ### Añade un enlace desde tu índice Abre `docs/00-MOC/Índice.md` y agrega al principio:  ```markdown > 📌 Ver también: [[../README]]``




