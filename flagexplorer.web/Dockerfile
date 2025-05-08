FROM node:18 AS build

# Set working directory
WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the source code
COPY . .

# Run unit tests
RUN npm test -- --watchAll=false

# Build the app
RUN npm run build

FROM nginx:stable-alpine

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/build /usr/share/nginx/html

EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]