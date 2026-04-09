docker build -t webhongan -f Dockerfile.WebHongAn ..

docker-compose -f docker-compose.yml down
docker-compose build
docker-compose -f docker-compose.yml up -d
# docker system prune -a -f