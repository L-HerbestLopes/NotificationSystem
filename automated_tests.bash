#!/usr/bin/env bash

# registers users
curl -X POST http://localhost:5258/api/users \
     -H "Content-Type: application/json" \
     -d '{"name": "Fulano", "email": "FulanoSilva@gmail.com", "access": 2}'

echo -e

curl -X POST http://localhost:5258/api/users \
    -H "Content-Type: application/json" \
    -d '{"name": "Ciclano", "email": "Ciclano2003@gmail.com", "access": 0}'