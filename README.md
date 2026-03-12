# Systems Engineering Project Showcase

This repository contains demo materials for selected personal software engineering projects, focusing on backend systems, networking, and high-performance data processing.

The projects showcased here include representative screenshots, architecture notes, and selected demo code samples.  
Full source code for some systems is private; this repository provides representative demonstrations of the technical concepts and implementations.

---

# Project Preview

### High-Concurrency Multiplayer Game Backend System
![Architecture](assets/backend-system/server-log-2.png)

### Distribution Platform
![Login UI](assets/distribution-platform/login.png)

### Asset Tool Console Output
![Excel Export](assets/asset-tool/console-output.png)

---

# Featured Projects

## 1. High-Concurrency Multiplayer Game Backend System
C++, SQL Server

- 300K+ lines of code
- Supports 1000+ concurrent players
- Handles 10M+ database records
- Millisecond-level indexed queries

**Overview:**  
A high-concurrency backend system designed to simulate real-time multiplayer game environments.  
The system implements networking protocol handling, session management, server-side game state processing, and optimized database persistence.

➡ Project Details:  
[View Project](projects/multiplayer-backend-system/README.md)

---

## 2. Game Distribution Platform
C#, ASP.NET Core, SQL Server, Chromium Embedded Framework

- Desktop launcher application
- User authentication and account management
- Game library and region/server display
- REST APIs with JWT and OAuth authentication

**Overview:**  
A desktop game distribution platform inspired by modern digital storefronts.  
The system combines a Chromium-based UI with backend services built in ASP.NET Core.

➡ Project Details:  
[View Project](projects/game-distribution-platform/README.md)

---

## 3. Game Asset Analysis Tool
C#

- Converts proprietary binary resource formats
- Processes 10K+ records with 100+ columns
- Exports structured Excel datasets
- ~2× performance improvement through optimized parsing

**Overview:**  
A data analysis and conversion tool designed to parse custom binary resource formats and transform them into structured datasets for analysis.

➡ Project Details:  
[View Project](projects/game-asset-analysis-tool/README.md)

---

# Technical Focus Areas

- Backend Systems
- Networking & Protocol Handling
- High-Concurrency Systems
- Database Optimization
- Developer Tools & Data Processing

---

# Repository Structure
```
projects/
├─ multiplayer-backend-system/
├─ game-distribution-platform/
└─ game-asset-analysis-tool/

assets/
├─ backend-system/
├─ distribution-platform/
└─ asset-tool/
```

---

# Notes

This repository contains demonstration materials and selected code samples intended for portfolio and technical showcase purposes.

Some full project source code remains private due to size or licensing considerations. Representative examples are provided to illustrate system design and implementation techniques.