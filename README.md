# Microservices.Communication.Sample

Решение содержит микросервис (`Service1`) и (`Service2`) +  контрактный слой для коммуникации между сервисами.

## Запуск RabbitMQ

```bash
docker compose up -d
```

##  если хошь запустить кафку
```bash
docker compose --profile kafka up -d
```

RabbitMQ Management UI: [http://localhost:15672](http://localhost:15672)  
Логин/пароль: `guest` / `guest`

## Запуск через VS Code

1. Откройте папку проекта в VS Code.
2. Выполните задачу `build` (`Terminal -> Run Task`).
3. Запустите конфигурацию дебага `Service1 Api` из `Run and Debug`.

Либо через терминал:

```bash
dotnet run --project src/Services/Service1/Api/Microservices.Communication.Sample.Service1.Api.csproj

dotnet run --project src/Services/Service1/Api/Microservices.Communication.Sample.Service2.Api.csproj
```

## API

`POST /api/messages/service2/base`

Пример body:

```json
{
  "message": "hello from service1"
}
```
