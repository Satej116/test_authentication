# Authentication (hello2)

This is an updated version of the Authentication project, with additional features and improvements.

Overview
This is a simple authentication system that allows users to register, log in, and access protected resources. The system is built using C# (.NET Core) for the backend and Microsoft SQL Server as the database.
The project includes:
* User Registration & Login
* Database Integration
* Token-Based Authentication

Prerequisites
Before running this project, make sure you have the following installed on your computer:
1. Docker (for running containers)
2. Git (for cloning the repository)
3. Visual Studio (for running the .NET application)
4. Postman (for testing API requests)

How to Set Up the Project (Step-by-Step)
1. Clone the Repository
First, download the project from GitHub:
sh
CopyEdit
git clone https://github.com/yourusername/authentication-system.git
cd authentication-system

2. Set Up Docker Containers
This project uses Docker to run the backend application and SQL Server database easily.
Start the Containers:
sh
CopyEdit
docker-compose up --build -d
* This command will download the required dependencies and start the services.
* The application will be running on http://localhost:8080/

3. Check if Everything is Running
Run the following command to ensure the Docker containers are running:
sh
CopyEdit
docker ps
You should see:
* authentication-app (your backend service)
* sqlserver (your database)
If they are not running, restart Docker and try docker-compose up again.

4. Access the API
Now, you can test the authentication system using Postman or a web browser or click on run button.
Register a New User
* URL: http://localhost:8080/api/auth/register
* Method: POST






* Body (JSON format):
json
CopyEdit
{
  "username": "john_doe",
  "password": "SecurePassword123"
}
* Expected Response:
json
CopyEdit
{
  "message": "User registered successfully"
}
Login
* URL: http://localhost:8080/api/auth/login
* Method: POST
* Body:
json
CopyEdit
{
  "username": "john_doe",
  "password": "SecurePassword123"
}
* Expected Response (includes a token):
json
CopyEdit
{
  "token": "eyJhbGciOiJIUzI1..."
}
Access Protected Routes
* Use the token received from the login response in the request header:
json
CopyEdit
{
  "Authorization": "Bearer your_token_here"
}

How to Stop the Project
To stop all running containers, run:
sh
CopyEdit
docker-compose down

Troubleshooting
If you encounter any issues, try the following:
1. Check if Docker is running
2. Restart Docker and try again
3. Make sure ports 8080 and 1433 are not in use by another application
4. Check the logs for errors using:
sh
CopyEdit
docker-compose logs -f

Contributors
* Satej Kulkarni - Developer
* Anirudh Mangalvedhe – Mentor 

