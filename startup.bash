docker build -t quetalsidera.me -f QuetzalSidera.Me/Dockerfile . 
docker build -t chat.quetalsidera.me -f Chat.QuetzalSidera.Me/Dockerfile .  
docker build -t api.quetalsidera.me -f Api.QuetzalSidera.Me/Dockerfile . 
docker build -t backend -f Backend/Dockerfile . 
docker-compose up --build
