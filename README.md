KeyManagementAPI

KeyManagementAPI is a work-in-progress REST API built in C# for managing cryptographic keys. The project focuses on symmetric and asymmetric key generation, secure storage, and encryption workflows within a structured backend architecture.

Overview

This API is designed to explore backend security patterns and applied cryptography in a service-oriented architecture. It provides endpoints for generating symmetric and asymmetric keys, securely storing them in SQL Server, and encrypting input data into ciphertext.

Features

- Cryptographic key generation

- Secure key storage and retrieval

- Symmetric key generation and encryption

- Asymmetric key generation and encryption

- Test endpoint for encrypting input strings to ciphertext

- RESTful API architecture


Tech Stack

- C# / .NET

- ASP.NET Core Web API
  
- SQL Server
  
- Razor Pages

- Built-in .NET cryptography libraries

Architecture

The API exposes endpoints for key lifecycle management and cryptographic operations.
Keys are generated server-side and stored securely within the SQL database.

Status

- Core key generation and signing logic implemented.
- Storage improvements and additional security hardening are in progress.
- AI prediction also in progress.

Running Locally

- Clone the repository

- Open the solution in Visual Studio

- Update the SQL Server connection string in appsettings.Development.json to match your local database configuration

- Ensure the required database exists (or run migrations if configured)

- Run the KeyManagementAPI project

- Test endpoints using Postman or via https://localhost:{port}
