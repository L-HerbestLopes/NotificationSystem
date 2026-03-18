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

echo -e

curl -X POST http://localhost:5258/api/users \
     -H "Content-Type: application/json" \
     -d '{"name": "Ciclano", "email": "Ciclano2003@gmail.com", "access": 0}'

echo -e

# should return a BadRequest (user was already registered)
# this is checked by the email, so two users can have the same name
curl -X POST http://localhost:5258/api/users \
     -H "Content-Type: application/json" \
     -d '{"name": "Beltrano", "email": "Bel.Marques@gmail.com", "access": 1}'

echo -e

# registers notifications

echo "POST (notifications)"

curl -X POST http://localhost:5258/api/notifications \
     -H "Content-Type: application/json" \
     -d '{"message": "this is a test message.", "senderId": 1, "recipients": [2, 3]}'

echo -e

# should return a BadRequest (sender is not a moderator/administrator)
curl -X POST http://localhost:5258/api/notifications \
     -H "Content-Type: application/json" \
     -d '{"message": "this message should fail!", "senderId": 3, "recipients": [1, 2]}'