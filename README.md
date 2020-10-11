# FB.TransactionalOutbox

As you know Event Sourcing and CQRS are important topics for microservices.
But have you ever thought, what happens if we do not throw any event(s) or lose some events during transaction?

## Installation 

To run a RabbitMq;
```
docker run -d --hostname my-rabbit --name myrabbit -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=123456 -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
and create a virtual host called as ``demand``.

## Articles

- What happens if we lose some event(s) during transaction? — [Part 1](https://bit.ly/33OOwHH "What happens if we lose some event(s) during transaction?") 
