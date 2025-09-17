---
type: adr
status: Accepted
date: 2025-09-13
tags: [adr, arquitectura, v2, clean-architecture]
decision-drivers:
  - Mantenibilidad 10+ años (entornos offline)
  - Despliegue mixto: Windows (IIS) y Linux (Apache/Nginx)
  - Separación clara de responsabilidades
  - Testeabilidad y refactor seguro
  - Complejidad operativa razonable (monolito modular)
---

# ADR: Arquitectura por capas (v2) de PRA-PCCS

## Contexto
La solución actual creció de forma orgánica y mezcla UI, acceso a datos y lógica de dominio. El producto debe:
- operar en redes corporativas sin Internet,
- desplegarse en **IIS** y **Apache/Nginx**,
- mantenerse durante **10+ años**,
- incorporar **i18n** (UI y datos),
- ejecutar **tareas de fondo** (Bosch/Modbus) con estabilidad.

## Decisión
Adoptar un **monolito modular por capas (Clean Architecture ligera)**:

src/  
PRA-PCCS.Web ← Blazor Server (UI + endpoints mínimos)  
PRA-PCCS.Application ← Casos de uso, puertos (interfaces)  
PRA-PCCS.Infrastructure ← EF Core + adaptadores (Npgsql, Bosch, Modbus)  
Domains/ ← Proyectos de dominio existentes (Users, Controller, etc.)  
docs/ ← Obsidian (SOPs, Runbooks, ADRs)

- **Web** referencia a **Application** e **Infrastructure**.
- **Application** referencia **Domains** y define interfaces (repos/puertos).
- **Infrastructure** implementa esas interfaces (EF Core, IO, integraciones).
- **Hosted Services** viven en Web pero **consumen casos de uso** de Application (no EF directo).

## Alcance
Afecta a estructura de proyectos, DI, estilo de servicios y login. No cambia reglas de negocio salvo dónde residen.

## Diseño seleccionado (puntos clave)
- **Auth**: cookies + claims; políticas (`CanConfigure`). CSRF/Antiforgery habilitado.
- **Persistencia**: PostgreSQL vía **EF Core**; `ControllerDataContext` movido a Infrastructure; migraciones allí.
- **i18n**: `AddLocalization` + `.resx` para UI; tablas de traducción (p.ej. `CSButtonTranslation`) para datos, con *fallback* a `es`.
- **Configuración proxy**: `ForwardedHeaders` activable por **config/env** (no por compilación).
- **Publicación**: SOPs para [[SOP - Despliegue IIS con HTTPS]] y Linux/Apache; opción **self-contained**.
- **Longevidad offline**: `global.json`, **lock files** (`packages.lock.json`), `NuGet.Config` con **feed local** opcional; snapshot de `.nupkg` cuando toque.
- **Documentación**: vault Obsidian en `docs/` versionado en Git (proyecto `Docs.csproj` para verlo en VS).

## Consecuencias
**Positivas**
- Mejor **mantenibilidad** y **testabilidad** (casos de uso puros).
- Separación clara UI / aplicación / infraestructura.
- Despliegue homogéneo en IIS/Apache (solo config).
- Preparado para i18n real en UI y datos.

**Negativas**
- Overhead inicial (mover contextos, crear repos/handlers).
- Más proyectos/DI que aprender para nuevos devs.

## Alternativas consideradas
1. **Monolito “todo en Web”**  
   + Simplicidad inicial; − deuda estructural y acoplamiento alto.
2. **Microservicios**  
   + Escala independiente; − complejidad operativa excesiva para el caso.
3. **DDD Modular Monolith completo**  
   + Aislamiento fuerte; − coste de modelado ahora.
4. **Cliente grueso (MAUI) + APIs mínimas**  
   + Offline UI; − no encaja con operadores múltiples y requisitos web.

## Plan de migración
1. Crear `Application` e `Infrastructure`; registrar DI básico.
2. Mover **`ControllerDataContext`** y **migraciones** a Infrastructure; crear `IUsersRepository` + `UsersRepositoryEf`.
3. Caso de uso **`LoginUserHandler`**; cambiar `/auth/login` para usarlo.
4. Adaptar **Hosted Services** a casos de uso (no EF directo).
5. Añadir i18n: Resources, selector de cultura y tablas de traducción.
6. Documentar SOPs y Runbooks; snapshot NuGet cuando estabilice.

## Riesgos y mitigaciones
- **Errores al mover EF/migraciones** → migrar por módulos y compilar a cada paso.
- **Tareas de fondo rompiendo al inicio** → DI por **constructor**, cancelación correcta y *retry* controlado.
- **Desalineación IIS/Apache** → `ForwardedHeaders` por config; SOPs separados por plataforma.
- **Dependencias futuras inaccesibles** → congelar SDK/paquetes y preparar **feed local** periódicamente.

## Preguntas abiertas
- ¿Centralizar versiones con `Directory.Packages.props` ahora o tras estabilizar?
- ¿Observabilidad estructurada (Serilog) en esta fase o después?
- ¿Necesitamos roles/permisos más finos en Claims?

## Referencias
- [[SOP - Despliegue IIS con HTTPS]]
- [[Runbook - Diagnóstico arranque IIS]] (pendiente)
- [[00-MOC/Índice]]
