# TODO 
- [ ] no logs on release build (still works on debug in Visual Studio)
- [x] insert OpenTracing / Jaeger documentation do Readme
- [x] insert jaegertracing/all-in-one documentation do Readme
- [ ] ? call Back twice from Middle
- [x] sending 'Baggage' (`correlation_id`) through layers
- [ ] starting 'Trace' on frontend
- [ ] Possibilities to swap UpdSender

# Architecture
- 4 separate container services
- System with tree layers: Front, Middle, Back. 
- Jaeger all-in-one: agent (network listener), collector (receives traces, validate, indexing, transformations), in-memory storage, Query (dashboard)
- Static IP addressess set in `docker-compose.yaml` - simplification for the purposes of PoC 

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

1. Browse 'http://localhost:16686/' for Jaeger dashboard and recent requests

### Scenarios 

1. Execute `Api/Success` and `Api/Failure` operations in swagger tester
2. Execute `Api/Success` / `Api/Failure` with `correlation-id` header
3. Execute `Api/Success` / `Api/Failure` and look for `trace-id` response header

# Docs
- https://opentelemetry.io/
- [Open Tracing](https://github.com/opentracing/opentracing-csharp)
- [Open Tracing tutorial](https://github.com/yurishkuro/opentracing-tutorial/tree/master/csharp)
- Jaeger: [homepage](https://www.jaegertracing.io/), [architecture](https://www.jaegertracing.io/docs/1.22/architecture/)
- [Jaeger C# client](https://github.com/jaegertracing/jaeger-client-csharp)