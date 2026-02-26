KeyManagementAPI

KeyManagementAPI is a work-in-progress REST API built in C# for managing cryptographic keys. The project focuses on secure key generation, storage, and digital signing and verification workflows.

Overview

This API is designed to explore backend security patterns and cryptographic operations in a structured service-oriented architecture. It provides endpoints for generating keys, securely storing them, and performing signing and verification operations.

The project emphasizes clean API design, proper key handling practices, and maintainable backend structure.

Features

- Cryptographic key generation

- Secure key storage and retrieval

- Digital signing of data

- Signature verification

- RESTful API architecture

- Structured request/response validation

Tech Stack

- C# / .NET

- ASP.NET Core Web API
  
- Razor Pages

- Built-in .NET cryptography libraries

Architecture

The API exposes endpoints for key lifecycle management and cryptographic operations.
Keys are generated server-side and stored securely within the application’s configured storage layer.
Signing and verification operations are handled through dedicated endpoints with structured input validation.

Status

- Core key generation and signing logic implemented.
- Storage improvements and additional security hardening are in progress.

Running Locally

- Clone the repository

- Open the solution in Visual Studio

- Configure application settings if required

- Run the API project

- Test endpoints using Postman or similar tools
