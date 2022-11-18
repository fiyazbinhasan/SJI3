# Stage 1 - the build process
FROM node:latest AS builder
WORKDIR /app
COPY . .
RUN npm i && npm run build

# Stage 2 - the production environment
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=builder /app/dist/sji3.ui .
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
COPY ./nginx/mime.types /etc/nginx/mime.types
COPY ./certs/dev_localhost.key /etc/nginx/localhost.key
COPY ./certs/dev_localhost.pem /etc/nginx/localhost.pem

ENTRYPOINT ["nginx", "-g", "daemon off;"]


