# Architecture
System with tree layers: Front, Middle, Back

### Controllers
* Success
* Failrue

### Flow
* Success: Front > Middle (700 ms delay) > Back (1500 ms delay)
* Failure: Front > Midle > Back (exception)

# Instructions
1. Compose the environment. Run command in the root folder
    ```bash
    docker compose up
    ```

1. Go to `https://localhost:8088/swagger` in the browser

1. Execute `Api/Success` and `Api/Failure` operations