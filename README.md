# Event-Driven Microservices with Saga Orchestrator 🚀

This project implements a **distributed microservices architecture** using **Event-Driven Design, Saga Orchestration, and MassTransit** with **RabbitMQ**.

## 📌 Key Features:
✅ **Microservices Architecture** with **.NET 8**  
✅ **Event-Driven Communication** with **RabbitMQ**  
✅ **Saga Orchestration** using **MassTransit State Machine**  
✅ **Three Independent Services:**
  - **OrderService**: Manages order lifecycle
  - **PaymentService**: Handles payment processing
  - **StockService**: Ensures stock availability

## ⚡ Technologies Used:
- **.NET 8 Web API**
- **MassTransit**
- **RabbitMQ**
- **Saga Pattern (State Machine)**
- **Docker**
- **Postman / cURL (for testing)**

## 🛠 Setup & Run
- **docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management**
- **cd OrderService && dotnet run**
- **cd PaymentService && dotnet run**
- **cd StockService && dotnet run**
