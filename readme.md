# Architecture
- System with tree layers: Front, Middle, Back. 
- Jaeger logs dashboard
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

1. Execute `Api/Success` and `Api/Failure` operations. You may use build in Swagger UI

1. Browse 'http://localhost:16686/' for Jaeger dashboard