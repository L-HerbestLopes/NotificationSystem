#!/usr/bin/env bash

# registers users
echo "POST (users)"

curl -X POST http://localhost:5258/api/users \
     -H "Content-Type: application/json" \
     -d '{"name": "Fulano", "email": "FulanoSilva@gmail.com", "access": 2}'

echo -e

curl -X POST http://localhost:5258/api/users \
     -H "Content-Type: application/json" \
     -d '{"name": "Beltrano", "email": "Bel.Marques@gmail.com", "access": 1}'

curl -X POST http://localhost:5258/api/users \
    -H "Content-Type: application/json" \
    -d '{"name": "Ciclano", "email": "Ciclano2003@gmail.com", "access": 0}'

echo -e
echo "GET (users)"

curl http://localhost:5258/api/users/3

echo -e

curl http://localhost:5258/api/users/Fulano

#registers notifications
echo "POST (notifications)"

curl -X POST http://localhost:5258/api/notifications \
     -H "Content-Type: application/json" \
     -d '{"message": "this is a test message", "senderId": 1,
     "recipients": [1, 2]}'