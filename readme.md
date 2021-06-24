# Architecture
- 4 separate container services
  - System with tree layers: Front, Middle, Back. 
  - Jaeger all-in-one: agent (network listener), collector (receives traces, validate, indexing, transformations), in-memory storage, Query (dashboard)

### Operations
* Api/Success
* Api/Failrue

### Flow
* Success: Front > Middle (700 ms delay) > Back (1500 ms delay)
* Failure: Front > Midle > Back (exception)

# Instructions
1. Compose the environment. Run command in the root folder
    ```bash
    docker compose up
    ```

1. Go to `http://localhost:8088/swagger` in the browser

1. Browse 'http://localhost:16686/' for Jaeger UI dashboard and recent requests

### Scenarios 
1. Execute operations operations in swagger tester UI. Browse Jaeger UI for logged spans.
2. Execute opeartions with `correlation-id` header (e.g. in Postman) - it should appear in http request spans.
3. Execute operations and look for `trace-id` response header. It may be used by frontend to track given requests. Lookup by Trace ID in Jagger UI. 

# Docs
- https://opentelemetry.io/
- [Open Tracing](https://github.com/opentracing/opentracing-csharp)
- [Open Tracing tutorial](https://github.com/yurishkuro/opentracing-tutorial/tree/master/csharp)
- Jaeger: [homepage](https://www.jaegertracing.io/), [architecture](https://www.jaegertracing.io/docs/1.22/architecture/)
- [Jaeger C# client](https://github.com/jaegertracing/jaeger-client-csharp)