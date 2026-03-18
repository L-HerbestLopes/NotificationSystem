#!/usr/bin/env bash

# getting user info
echo "GET (users)"

curl http://localhost:5258/api/users/1

echo -e

curl http://localhost:5258/api/users/search?email=Ciclano2003@gmail.com

echo -e

# should give a NotFound response
curl http://localhost:5258/api/users/search?email=l.herbestlopes@gmail.com

echo -e

# getting notification info

curl http://localhost:5258/api/notifications/search?userid=2

echo -e

curl http://localhost:5258/api/notifications/search?email=Ciclano2003@gmail.com