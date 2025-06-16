# ntech.logging

## Overview

**ntech.logging** is a .NET Core 3.1 solution for advanced, extensible logging using Serilog, with support for Seq, Elasticsearch (ELK), and file-based logging. It is designed for ASP.NET Core applications and provides flexible configuration via `appsettings.json`.

## Project Structure

- **Ntech.Logging**: Main ASP.NET Core web application.
- **Ntech.Logging.Serilog**: Logging library with Serilog integration, enrichers, and configuration options.
- **Seq**: Docker Compose setup for running a local Seq server.
- **Elastic**: Docker Compose setup for running Elasticsearch and Kibana.

## Features

- Structured logging with Serilog
- Console, File, Seq, and Elasticsearch (ELK) sinks
- OpenTracing context enrichment (TraceId, SpanId)
- Flexible configuration via `appsettings.json`

## Configuration

Edit `Ntech.Logging/Ntech.Logging/appsettings.json` to configure logging:

```json
"Logging": {
  "ConsoleEnabled": true,
  "Seq": {
    "Enabled": true,
    "Url": "http://localhost:5340/",
    "ApiKey": ""
  },
  "ELK": {
    "Enabled": true,
    "Url": "http://localhost:9200"
  },
  "File": {
    "Enabled": true,
    "PathFile": ".\\logs\\log.txt"
  },
  "MinimumLevel": {
    "Default": "Information",
    "Override": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

## Running Seq and Elasticsearch (ELK) with Docker Compose

### Seq
To start a local Seq server:

```sh
docker-compose -f Ntech.Logging/Seq/docker-compose/docker-compose.yml up -d
```
Access Seq at [http://localhost:5340](http://localhost:5340).

### Elasticsearch & Kibana
To start Elasticsearch and Kibana:

```sh
docker-compose -f Ntech.Logging/Elastic/docker-compose/docker-compose.yml up -d
```
Access Elasticsearch at [http://localhost:9200](http://localhost:9200) and Kibana at [http://localhost:5601](http://localhost:5601).

## Usage

- The main entry point is `Program.cs`, which configures logging via `.UseLogging()` extension.
- Logging options and sinks are configured in `appsettings.json`.
- The `Ntech.Logging.Serilog` library provides extension methods and enrichers for advanced logging scenarios.

## Extending Logging

- Add enrichers in `Ntech.Logging.Serilog/Enrichers` for custom log properties.
- Modify or extend `Extensions.cs` to add new sinks or enrichers.

## Dependencies

- Serilog.AspNetCore
- Serilog.Sinks.Seq
- Serilog.Sinks.Elasticsearch
- OpenTracing

## License

[MIT](LICENSE) (add your license file if needed)